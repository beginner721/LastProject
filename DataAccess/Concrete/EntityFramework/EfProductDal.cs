using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    //IProductDal neden gerekli? Çünkü içerisinde sadece Product'ları ilgilendirecek spesifik metodlar yazılabilir... genel kodların haricinde ekstra metodlar gerekebilir.
    {
        public List<ProductDetailDto> GetProductDetails()
        {
            using (NorthwindContext context= new NorthwindContext())
            {
                var result = from p in context.Products //buraların yazılabilmesi için yukarıda using linq olması gerekir
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId //p deti catId ile c deki catId eşit ise onlar join edilir
                             //sonucu da alt kısma göre verir
                             select new ProductDetailDto {ProductId=p.ProductId,CategoryName=c.CategoryName,ProductName=p.ProductName,UnitsInStock=p.UnitsInStock };
                return result.ToList(); //listeye çevirmemiz gerekir çünkü IQueryable bir nesnedir result
            }
        }
    }
}
