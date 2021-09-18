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
            //AOP bir methodun �n�nde veya sonunda / hata verdi�inde �al��an kod par�ac�klar�d�r.
            //AOP altyap�s�n� kullanaca��m�z i�in en �steki IoC Container'ler kullan�labilir. Autofac Bize AOP imkan� sunuyor (di�erleri de ..)

            services.AddControllers();
            //services.AddSingleton<IProductService,ProductManager>(); // DATA YOKSA SINGLETON KULLANILIR!, data varsa AddScoped vs.
            ////biri ctor'da IProductService kullan�rsa arkaplanda ona ProductManager new ' i verir
            //services.AddSingleton<IProductDal, EfProductDal>();
            //yukar�dakiler art�k Business i�erisinde ....
            //program.cs k�sm�na da art�k buray� de�il di�erini kullanaca��m�z� belirtiyoruz

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
