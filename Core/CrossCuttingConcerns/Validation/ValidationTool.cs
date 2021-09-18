using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Validation
{
    public static class ValidationTool
    {

        public static void Validate(IValidator validator, object entity) //entity dto herşeyi ekleyebiliriz o yüzden object yazılır hepsinin base'idir
        {
            //business içine taşınan kuralları burada kullanıyoruz.
            var context = new ValidationContext<object>(entity); //validation yapılacak zaman standart kod, ilgili thread'i anlatır, <Product> için doğrulama yapacağız çalışacağımız tip de bu (product)
            var result = validator.Validate(context); //yazdığımız kurallar için ilgili context'i Validate et/doğrula! context= en üstte belirttiğimiz (product)
            if (!result.IsValid)//eğer sonuç geçerli değilse hata fırlat diyoruz (IsValid= eğer sonuç   & !result= geçerli değil   ... ise)
            {
                throw new ValidationException(result.Errors);
            }

        }


    //    //ÜSTTEKİ KODUN ESKİ VERSİYONU

    //    var context = new ValidationContext<Product>(product); //validation yapılacak zaman standart kod, ilgili thread'i anlatır, <Product> için doğrulama yapacağız çalışacağımız tip de bu (product)
    //    ProductValidator productValidator = new ProductValidator(); // Product'ı productvalidator kullanarak doğrulayacağım (yani business içine yazdığımız kuralları kullanarak)
    //    var result = productValidator.Validate(context); //yazdığımız kurallar için ilgili context'i Validate et/doğrula! context= en üstte belirttiğimiz (product)
    //        if (!result.IsValid)//eğer sonuç geçerli değilse hata fırlat diyoruz (IsValid= eğer sonuç   & !result= geçerli değil   ... ise)
    //        {
    //            throw new ValidationException(result.Errors);
    //}
    }
}

