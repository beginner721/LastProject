using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessResult:Result //inheritance
        //bu classı yaparak true,'mesaj' kısmını buradan hallediyoruz product managerda ekstra yazmamız gerekmiyor.
    {
        public SuccessResult(string message) : base(true, message) //base'e true gönderdik yani Result kısmına..
        { 

        }
        public SuccessResult():base(true) //base'in tek parametreli olanını çalıştırdık, mesaj vermek istemediğimiz kısım ...
        {

        }
    }
}
