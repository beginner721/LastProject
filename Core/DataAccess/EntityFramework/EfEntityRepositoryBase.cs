using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity> //Solda hangi TEntity'i verirsek sağda aynı şekilde onun IEntityRepository'si çalışacak... ikisi de TEntity
        where TEntity : class, IEntity, new() //TEntity referans tip (class) olmalı, IEntity i kullanan olmalı , newlenebilir olmalı. IEntity yazamayalım diye.
        where TContext : DbContext, new() //TContext'imiz DbContext olmalı, newlenebilir olmalı
    {
        public void Add(TEntity entity)
        {
            //buradaki using IDisposable pattern implementation of c# 'tır. en üsteki using ile alakası yok
            using (TContext context = new TContext()) //using bitince garbagecollector gelir performans artırır ! belleği hızlıca temizler..
            {
                var addedEntity = context.Entry(entity); //eşleştirmeyi yazıyoruz, yani referansı yakalama... ancak yeni ekleneceği için eşleşmiyor direkt ekleniyor
                addedEntity.State = EntityState.Added; // eklenecek bir nesne
                context.SaveChanges(); // ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                //ternary op. yazım şekli
                //set ile products tablosuna yerleştik oradaki bütün tabloyu listeye çevirir bize verir EĞER FİLTER NULL İSE!
                //te
                return filter == null
                    ? context.Set<TEntity>().ToList() //filter null ise burası çalışır
                    : context.Set<TEntity>().Where(filter).ToList(); //filter varsa burası çalışır
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
