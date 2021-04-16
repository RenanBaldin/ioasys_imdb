using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories.Standard.EFCore
{
    public class DomainRepository<TEntity> : RepositoryAsync<TEntity>,
                                         IDomainRepository<TEntity> where TEntity : class, IIdentityEntity
    {
        protected DomainRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
