using ConfigServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EnVarConfigExtensions
    {
        public static void AddEnVarConfig(this IServiceCollection services)
        {
            services.AddScoped<IConfigService, EnVarConfigService>();
        }
    }
}
