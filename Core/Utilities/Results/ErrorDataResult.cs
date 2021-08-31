using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, false, message) //base e datayı  işlem sonucunu ve mesajını verdik default true...
        {

        }
        public ErrorDataResult(T data) : base(data, false) //mesaj istenmiyorsa data ve true döndürülür direkt base e
        {

        }

        //alttaki ikisi pek kullanılmaz
        public ErrorDataResult(string message) : base(default, false, message) //data döndürülmek istenmediği durumda sadece mesaj verildi .. direkt default yani T' ne ise o / pek kullanılmaz 
        {

        }
        public ErrorDataResult() : base(default, false) //data default, message yok sadece true ... hiçbişey verilmedi
        {

        }
    }
}
