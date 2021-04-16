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
    public class DirectorRepository : DomainRepository<Director>,
                                  IDirectorRepository
    {
        public DirectorRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Director>> GetAllIncludingMoviesAsync()
        {
            IQueryable<Director> query = await Task.FromResult(GenerateQuery(null, null, nameof(Director.Movie)));
            return query.AsEnumerable();
        }


        public async Task<Director> GetByIdIncludingMoviesAsync(int id)
        {
            IQueryable<Director> query = await Task.FromResult(GenerateQuery((x => x.Id == id), null, nameof(Director.Movie)));
            return query.SingleOrDefault();
        }


    }
}
