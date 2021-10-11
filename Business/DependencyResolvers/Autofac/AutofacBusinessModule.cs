using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module //buradaki Module, using Autofac; den gelir...
        //arkaplanda newleri oluşturuyor, reflection ile yapıyor bunu
    {
        //startup yerine yazdıklarımız buraya geliyor.
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            //IProductService istenirse ona ProductManager instance'ı ver demektir. Single Instance tek bir instance oluşturur herkese onu verir.
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            //alttaki kod Interceptor görevi veriyor 
            var assembly = System.Reflection.Assembly.GetExecutingAssembly(); //çalışan uygulama içerisinde 

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces() //implemente edilmiş interface'leri bul 
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector() //Onlar için AspectInterceptorSelector 'u çağır, Aspect var mı diye bakıyor [ASPECT] 
                }).SingleInstance();
        }
    }
}
