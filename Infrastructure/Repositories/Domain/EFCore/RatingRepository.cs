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
    public class RatingRepository : DomainRepository<Rating>,
                                  IRatingRepository
    {
        public RatingRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Rating>> GetAllIncludingUserAsync()
        {
            IQueryable<Rating> query = await Task.FromResult(GenerateQuery(null, null, nameof(Rating.User)));
            return query.AsEnumerable();
        }

        public async Task<Rating> GetByIdIncludingUserAsync(int id)
        {
            IQueryable<Rating> query = await Task.FromResult(GenerateQuery((x => x.Id == id), null, nameof(Rating.User)));
            return query.SingleOrDefault();
        }
    }
}
