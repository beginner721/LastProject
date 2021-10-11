using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    //erişim anahtarı
    public class AccessToken
    {
        //postmandan kullanıcı adı ve şifre verilecek biz de ona bir token vereceğiz
        public string Token { get; set; } //json web token değerinin kendisi
        public DateTime Expiration { get; set; } //tokenin sonlanma zamanı
    }
}
