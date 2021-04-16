using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IActorRepository : IDomainRepository<Actor>
    {
        Task<IEnumerable<Actor>> GetAllIncludingAllAsync();
        Task<Actor> GetByIdIncludingAllAsync(int id);
    }
}
