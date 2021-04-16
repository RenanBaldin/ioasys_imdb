using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.IoC.ORMs
{
    public abstract class OrmTypes : IOrmTypes
    {
        internal abstract IServiceCollection AddOrm(IServiceCollection services, IConfiguration configuration = null);

        public IServiceCollection ResolveOrm(IServiceCollection services, IConfiguration configuration = null)
        {
            return AddOrm(services, configuration);
        }
    }
}
