using Business.Abstract;
using Business.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            //authentication servisi için JwtBearer kullanacaðýmýzý belirtiyoruz.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            ServiceTool.Create(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //burasý middleware: yaþam döngüsünde hangi yapýlarýn sýrasýyla devreye gireceðini belirtiyorduk
            //önceden klasik asp.net de burasý tanýmlýydý ihtiyaç olsa da olmasa da devreye girerdi
            //artýk neye ihtiyaç varsa onu araya sokuyoruz bu sebeple middleware deniyor.
            //middleware: ara katman yazýlýmý
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
