using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain.Standard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories.Domain
{
    public interface IMovieRepository : IDomainRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetAllIncludeActorsAsync();
        Task<Movie> GetByIdIncludeActorsAsync(int id);
    }
}
