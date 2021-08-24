using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; //injection
        //interface kullandık çünkü inmemory de çalışıyoruz yarın entityframework de olabilir değişimlere açık bir yapıdayız.

        public ProductManager(IProductDal productDal) //ctor 
        {
            _productDal = productDal;
        }

        public List<Product> GetAll()
        {
            //işkodları vs vs. 
            //yetkisi varsa vs. ardından en alttaki kod çalışır.
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategoryId(int id)
        {
            return _productDal.GetAll(p=> p.CategoryId==id); 
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max);
        }
    }
}
