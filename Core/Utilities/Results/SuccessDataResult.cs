using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data, string message):base(data,true,message) //base e datayı  işlem sonucunu ve mesajını verdik default true...
        {

        }
        public SuccessDataResult(T data):base(data,true) //mesaj istenmiyorsa data ve true döndürülür direkt base e
        {

        }

        //alttaki ikisi pek kullanılmaz
        public SuccessDataResult(string message):base(default,true, message) //data döndürülmek istenmediği durumda sadece mesaj verildi .. direkt default yani T' ne ise o / pek kullanılmaz 
        {

        }
        public SuccessDataResult():base(default,true) //data default, message yok sadece true ... hiçbişey verilmedi
        {

        }
    }
}
