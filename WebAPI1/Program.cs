using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())//fabrika olarak Autofac'i kullan
            //yukarýdaki yazarak farklý bir IoC kullanacaðýmýzý belirtiyoruz, newleyip Install etmemiz gerekiyor Autofac'i

                .ConfigureContainer<ContainerBuilder>(builder => //ContainerBuilder using Autofac'den gelir
                {
                    builder.RegisterModule(new AutofacBusinessModule()); //kullanacaðýmýz yeri belirtiyoruz ..
                })

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
