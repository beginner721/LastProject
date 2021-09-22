using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics ) //params ile IResult türünde istediğimiz kadar parametre ekleyebiliriz 
        {
            foreach (var logic in logics)
            {
                if (!logic.Success) //başarılı değil ise business ı haberdar edeceğiz
                {
                    return logic; //uymayan kuralı döndürüyoruz
                }
            }
            return null; //başarılıysa birşeye gerek yok
        }
    }
}
