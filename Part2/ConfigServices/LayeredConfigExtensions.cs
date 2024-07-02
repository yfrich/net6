using ConfigServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LayeredConfigExtensions
    {
        public static void AddLayerConfig(this IServiceCollection services)
        {
            services.AddScoped<IConfigReader, LayeredConfigReader>();
        }
    }
}
