using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product> //AbstractValidator fluentvalidation'dan gelir 
    {//bu kurallar ctor içine yazılır
        public ProductValidator()
        {
            //KURALLAR ARDI ARDINA DA YAZILABILIR AMA !! HEPSİ AYRI YAZILMALIDIR, ILERIDE ORN: WHEN KULLANMAK GEREKIRSE AYIRMAK ZORUNDA KALIRIZ

            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryId==1);
            //p'nin categoryId'si =1  olduğu zaman p.UnitPrice büyük eşittir 10 olmalıdır
            
            //olmayan bir kural nasıl yazılır ? 
            //örnek olarak saçma olsa da bütün ürün adlarının ilk hafi A ile başlamalı ..
            RuleFor(P=>P.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı");
            //fluent 19 dilde destek veriyor .WithMessage ile kendi uyarımızı yazabiliriz, özel durum olmadıkça tavsiye edilmez

        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
