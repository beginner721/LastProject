using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        
        IProductDal _productDal; //injection
        //interface kullandık çünkü inmemory de çalışıyoruz yarın entityframework de olabilir değişimlere açık bir yapıdayız.
        ICategoryService _categoryService; //BAŞKA DAL ENJEKTE EDEMEYİZ ANCAK servisini enjekte edebiliriz.
        //çünkü dal içinde işkodu yok ancak serviste işkodu yazıldıysa her yer etkiler direkt olarak

        public ProductManager(IProductDal productDal, ICategoryService categoryService) //ctor 
        {
            _productDal = productDal;
            _categoryService = categoryService;
            
        }

        //Claim
        [SecuredOperation("product.add")]
        [ValidationAspect(typeof(ProductValidator))] //Altaki metodu doğrula, ProductValidator kullanarak !!! demektir
        public IResult Add(Product product)
        {
            
            

            IResult result= BusinessRules.Run(CheckIfProductNameExists(product.ProductName), 
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());

            if (result!=null) //kurala uymayanları döndürdüğümüz için burasının boş olması gerek, EĞER RESULT BOŞ DEĞİLSE!
            {
                return result; //hatayı göster
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);

            
        }

        public IDataResult<List<Product>> GetAll()
        {
            //işkodları vs vs. 
            //yetkisi varsa vs. ardından en alttaki kod çalışır.
            if (DateTime.Now.Hour == 23)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result > 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //any var mı demek
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
