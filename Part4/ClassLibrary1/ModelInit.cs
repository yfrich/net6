using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zack.Commons;

namespace ClassLibrary1
{
    public class ModelInit : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddScoped<Class1>();
            services.AddScoped<Class2>();
        }
    }
}
