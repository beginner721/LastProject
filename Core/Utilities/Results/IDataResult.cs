using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult //hangi tipi döndürecek belirtilmeli  ? <T>
    {
        //burası mesajı/işlem sonucunu içerecek aynı zamanda datayı da içerecek
        //o yüzden mesajı işlem sonucunu IResult zaten içeriyor, tekrar etmeye gerek yok IResult implementasyonu yapıyoruz
        //ek olarak data yazılacak

        T Data { get; }
    }
}
