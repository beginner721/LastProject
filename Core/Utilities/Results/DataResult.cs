using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T> //çalışılacak tip çalışırken söylenecek <T>
    {
        //DataResult bir Result'tır. inheritasyon var. Result içindeki bool ve message'leri içeriyor şu an!
        //Result'ın Ctor'larını implemente etmemiz gerekiyor

        public DataResult(T data, bool success, string message):base(success,message) //base'e succes ve message yolluyoruz, böylece succes ve message kodunu yazmamış oluyoruz
        {
            Data = data;
        }

        public DataResult(T data, bool success):base(success) //message gönderilmeyen durum 
        {
            Data = data;
        }
        public T Data { get; }
    }
}
