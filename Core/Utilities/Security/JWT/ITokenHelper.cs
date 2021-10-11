using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user,List<OperationClaim> operationClaims);
        //buna neden ihtiyaç var, kullanıcı adı ve şifresini girdi apiye yolladı
        //apide EĞER DOĞRU İSE bizim yukarıdaki createtoken çalışacak
        //ilgili kullanıcı için db ye gidecek db de bu kullanıcının claimlerini oluşturacak
        //orada bir JWT üretip onu cliente geri yollayacak.
    }
}
