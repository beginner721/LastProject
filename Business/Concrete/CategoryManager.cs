using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal; //bağımlılığı minimize ediyoruz, ctor injection ile bağımlılık yaratıp interface'e bağımlı oluyoruz

        //ben categorymanager olarak veri erişim katmanına bağlıyım ama bağımlılığım zayıf, referans/interface üzerinden bağımlıyım
        //o sebeple dataaccess katmanında kurallara uyulduğu sürece istendiği gibi at koşturulabilir
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetAll()
        {
            return _categoryDal.GetAll();
        }

        public Category GetById(int categoryId)
        {
            return _categoryDal.Get(c=> c.CategoryId==categoryId);

            //yukarıdaki kod ' Select * from categories where categoryId=3 ' çalıştıracaktır
        }
    }
}
