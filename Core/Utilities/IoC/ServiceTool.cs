using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        //
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)//.NET'in service collectionunu kullanarak
        {
            ServiceProvider = services.BuildServiceProvider();//servisleri al ve onları build et
            return services;
        }
    } //bu kod web api de veya autofac'de oluşturduğumuz injectionları oluşturabilmemize yarıyor, interface'in servisteki karşılığını bu kod vasıtasıyla alabiliyoruz
}
