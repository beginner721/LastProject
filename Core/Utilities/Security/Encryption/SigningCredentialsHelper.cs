using Microsoft.IdentityModel.Tokens;//signing credentials burdan gelir 
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        //burası bizim için web apinin kullanabileceği JWT larının oluşturulabilmesi için anahtara ihtiyacımız var
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            return new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512Signature);
            //hangi anahtar ve hangi algoritma kullanılacak onu veriyoruz.
        }
    }
}
