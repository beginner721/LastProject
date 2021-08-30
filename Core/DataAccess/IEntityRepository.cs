using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T:class,IEntity , new()

        //T'yi sınırlandırmak istiyoruz herkes istediği T'yi yazamasın.
        //veritabanı nesnelerini ekler siler güncelleriz,T yi buna göre ayarlayalım...  GENERIC CONSTRAINT demek generic kısıt demektir
        //int verseydik  sorun olmazdı ancak  where class yaparak sadece referans tip alacak hale getiriyoruz (class olabilir demek değil, referans tip olabilir demek)
        //herhangi bir class değil belirli bir yerdeki classları vermesini istiyoruz onun için de referansını gösteriyoruz(IEntity)
        // en son new yazarak newlenebilir birşey istiyoruz, IEntity yazılmasını da engellemiş oluyoruz
        //özetle T bir referans tip olmalı, IEntity olabilir veya onu implemente olan birşey ve newlenebilen birşey olabilir diyor.
    {
        List<T> GetAll(Expression<Func<T,bool>> filter =null); //parantez içindeki expression, linq ile beraber gelir, bununla istediğimiz filtreyi verebiliriz. ayrı methodlar yazmamıza gerek yok
        //filter=null ; filtre vermezse bütün datayı getirsin demektir filtre yazmak mecburi değildir
        T Get(Expression<Func<T, bool>> filter);//tek bir datayı getirmek için, genelde birşeyin detayını görmek için kullanılır, filter vermek zorundadır! 
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
