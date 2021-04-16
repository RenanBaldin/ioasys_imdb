using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC
{
    public static class ResolveOrmIoC
    {
        public static void InfrastructureORM<T>(this IServiceCollection services, IConfiguration configuration = null) where T : IOrmTypes, new()
        {
            var ormType = new T();
            ormType.ResolveOrm(services, configuration);
        }
    }
}
