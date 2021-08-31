using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        //CTOR kullanım örneği aşağıda...

        public Result(bool success, string message):this(success) //this classın kendisi demektir, Result'ın tek parametreli ctor'una success'i yolla demiş olduk, yani dolayısıyla alt kısım da çalışacaktır
            //success in yanına message de yazılsaydı yine kendini çalıştırırdı, iki parametreli ctoru çalıştır demek olacaktı bu da kendisi demektir
        {
            //Message get'ini set ettik
            Message = message; //getler CTOR İÇİNDE SET EDİLEBİLİR !!!!!!!!!!!!!!!!!!!!!!!
            //getter set edilemiyor diye bilinse de ctor içinde set edilebilecektir
            //BÖYLECE programcı kafasına göre kodlar yazmaz düzene uymuş olur kodlar okunaklı olur ve yapıyı standardize etmiş oluruz 

            //Success = success; buraya yazılmasına gerek yok alt kısım onu karşılayacaktır.  
        }
        public Result(bool success)
        {
            //ctor overloading yaptık, farklı iki imzaya ihtiyacımız var
            //yukarıdaki ctor'da message veriliyor, mesajı tercih etmeyebiliriz o sebeple bir ctor daha yazdık mesaj olmadan kullanım için...
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; } //set vermeyerek programcıyı sınırlandırıyoruz, olayı standartlaştırıyoruz
    }
}
