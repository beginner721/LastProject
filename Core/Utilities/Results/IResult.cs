using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç
    public interface IResult
    {
        bool Success { get; } //sadece get yaptık, sadece okunabilir demektir.
        //bool true veya false başarılı olup olmaması yeterli
        string Message { get; } //başarılı veya başarısız mesajlarımız da burada bulunacak
    }
}
