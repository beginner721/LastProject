using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Autofac, Ninject, CastleWindsor, StructureMap, LightInject, DryInject --> IoC Container
            //AOP bir methodun önünde veya sonunda / hata verdiðinde çalýþan kod parçacýklarýdýr.
            //AOP altyapýsýný kullanacaðýmýz için en üsteki IoC Container'ler kullanýlabilir. Autofac Bize AOP imkaný sunuyor (diðerleri de ..)

            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>(); // DATA YOKSA SINGLETON KULLANILIR!, data varsa AddScoped vs.
            ////biri ctor'da IProductService kullanýrsa arkaplanda ona ProductManager new ' i verir
            //services.AddSingleton<IProductDal, EfProductDal>();
            //yukarýdakiler artýk Business içerisinde ....
            //program.cs kýsmýna da artýk burayý deðil diðerini kullanacaðýmýzý belirtiyoruz

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
