using Microsoft.IdentityModel.Tokens;//security key burdan gelir, install edilebilir NuGet içinden
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    //işin içinde şifreleme olan sistemlerde bizim herşeyi bir byte array formatında veriyor olmamız gerekiyor
    //basit bir string ile key oluşturamıyoruz
    //bunları asp.net in json web token servislerinin anlayacağı hale getirmemiz gerekiyor...
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            //simetrik bir security key kullanacağız.
        }
    }
}
