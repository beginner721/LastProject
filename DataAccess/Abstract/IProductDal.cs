using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //IEntityRepository den formüllerimizi almış olduk, buraya özel bir method gerekliyse onu ekleyebiliriz...
        List<ProductDetailDto> GetProductDetails();//işte buraya özel bir method
    }
}
