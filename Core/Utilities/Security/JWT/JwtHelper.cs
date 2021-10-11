using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration; //IConfiguration burdan gelir
using Microsoft.IdentityModel.Tokens; //SigningCredentials burdan gelir
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt; //JwtSecurityToken burdan gelir
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class JwtHelper : ITokenHelper
    {
        //IConfiguration API de bulunan AppSettings.json dosyasını okumaya yarar
        //orda okunan değerler bir nesneye atılacak o nesne de tokenoptions
        public IConfiguration Configuration { get; }
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //get kırmızı ise "Microsoft.Extensions.Configuration.Binder" kurulmalı
            //getsection: section u bul, yani appsettingsdeki her bir süslü parantez dizisi 
            //hangi section: token options,bunu al ve onu TokenOptions sınıfının değerlerini kullanarak map'le, yani json to class
            //yani appsettingsdeki audience ' ı  tokenoptionsdaki audienca a eşitle vs. vs. diğerleri de..

        }

        //bu kullanıcı için bir tane token üreteceğiz
        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            //user bilgisi ve claim bilgierine göre bir token oluşturacak bu method
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration); //token ne zaman expired olacak, appsettingsden tokenoptionsa aldık bu bilgiyi ve burada kullanabildik
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey); //yazdığımız SecurityKeyHelperin içindeki create ile key oluşturuyoruz
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey); //hangi algoritma ve hangi anahtar kısmı da burada, buradaki helperda da bunlar var
            var jwt = CreateJwtSecurityToken(_tokenOptions, user, signingCredentials, operationClaims);
            //yukarıda token optionsları kullanarak ilgili user için ilgili credentials'ları kullanarak bu kişiye atanacak claimleri yani yetkileri içeren bir methodla yapıyoruz, method altta
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };

        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, User user,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken( //token oluşturmaya yarıyor
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, operationClaims),
                signingCredentials: signingCredentials
                //yukarıdaki bilgileri oluşturuyoruz
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user, List<OperationClaim> operationClaims)
        {//claimler önemli onun için de böyle bir methodumuz var, claim yetkiden daha fazlasıdır.
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");//başına dolar eklersen çift tırnak içine kod yazabilirsin, iki stringi yan yana göstermek için yazılmış, artı ile de yapılabilir
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());//kullanıcının db deki çektiğimiz claimlerini/rollerinin name lerini çekip array e basıp rollerini ekliyoruz.

            return claims;
        }
    }
}
