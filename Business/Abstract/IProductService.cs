using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
        //IResult = voidler için, yani döndürülmeyen methodlar! dataya sahip değiller...
        //IDataResult = List'ler ve diğer döndürülecek şeyler ; hem diğer verileri(mesajlar vs.) hem de datayı içinde barındıracak
      
    {
        //bu methodları servis ediyoruz
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product); //void değil IResult döndürsün istiyoruz, dönüş mesajı verebilelim diye ...
        IResult Update(Product product);
        IResult AddTransactionalTest(Product product);
        //transaction yönetimi uygulamalarda tutarlılığı korumak için kullanılan bir yöntem,


    }
}
