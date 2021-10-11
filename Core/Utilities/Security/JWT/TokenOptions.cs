using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class TokenOptions
    {//bu class bir Ientity Idto değil
        //altaki bütün özelliklerin her biri birer option'dır bu sebeple class options adını alır
        //classlarımız normalde tekil olur.
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public string SecurityKey { get; set; }
    }
}
