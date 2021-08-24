using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //buradaki using IDisposable pattern implementation of c# 'tır. en üsteki using ile alakası yok
            using (NorthwindContext context= new NorthwindContext()) //using bitince garbagecollector gelir performans artırır ! belleği hızlıca temizler..
            {
                var addedEntity = context.Entry(entity); //eşleştirmeyi yazıyoruz, yani referansı yakalama... ancak yeni ekleneceği için eşleşmiyor direkt ekleniyor
                addedEntity.State = EntityState.Added; // eklenecek bir nesne
                context.SaveChanges(); // ekle
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context= new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context= new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter); 
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context= new NorthwindContext())
            {
                //ternary op. yazım şekli
                //set ile products tablosuna yerleştik oradaki bütün tabloyu listeye çevirir bize verir EĞER FİLTER NULL İSE!
                //te
                return filter == null 
                    ? context.Set<Product>().ToList() //filter null ise burası çalışır
                    : context.Set<Product>().Where(filter).ToList(); //filter varsa burası çalışır
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
