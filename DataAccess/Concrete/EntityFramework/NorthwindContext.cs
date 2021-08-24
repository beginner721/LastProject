using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //context nesnesi DB tabloları ile proje classlarını bağlamak
    public class NorthwindContext:DbContext //entity framework kurunca DbContext base sınıfı gelir...
    {
        //altaki method; projemizin  hangi veritabanıyla ilişkili olduğunu belirttiğimiz yer
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //sql servera nasıl bağlanacağını belirteceğiz
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=true"); //@ işareti ters slash'ın normal kullanılmasını sağlar
            //büyük küçük harf kuralları önemsizdir burada
        }
        //dbset ile nesnelerin db deki karşılıklarını ayarlıyoruz
        public DbSet<Product> Products{ get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
