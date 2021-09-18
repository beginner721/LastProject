using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType) //kullanırken parantez içinde Type belirtiyoruz, ProductManager Add kısmından görebiliriz.
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType)) //gönderilen Validator type bir IValidator değilse UYAR!
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil!");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);//burası reflection, çalışma anında birşeyleri çalıştırabilmemizi sağlıyor, productvalidator'ın instance'ını oluşturduk newledik yani...
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // productvalidator'ın çalışma tipini bul diyor //base tipi, generic argümanlarından ilkini bul .. <Product> vs..
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // onun parametrelerini bul
            foreach (var entity in entities) //parametreleri tek tek gez 
            {
                ValidationTool.Validate(validator, entity); //validation tool kullanarak bunları validate et!
            }
        }
    }
}
