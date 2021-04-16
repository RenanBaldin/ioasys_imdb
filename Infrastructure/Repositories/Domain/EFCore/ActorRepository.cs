using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Infrastructure.Interfaces.Repositories.Domain;
using Infrastructure.Repositories.Standard.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Domain.EFCore
{
    public class ActorRepository : DomainRepository<Actor>,
                                  IActorRepository
    {
        public ActorRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Actor>> GetAllIncludingAllAsync()
        {
            IQueryable<Actor> query = await Task.FromResult(GenerateQuery(null, null, nameof(Actor.MovieActor)));
            return query.AsEnumerable();
        }

        public async Task<Actor> GetByIdIncludingAllAsync(int id)
        {
            IQueryable<Actor> query = await Task.FromResult(GenerateQuery((x => x.Id == id), null, nameof(Actor.MovieActor)));
            return query.SingleOrDefault();
        }
    }
}
