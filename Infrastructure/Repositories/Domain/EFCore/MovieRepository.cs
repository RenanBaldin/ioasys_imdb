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
    public class MovieRepository : DomainRepository<Movie>,
                              IMovieRepository
    {
        public MovieRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }


        public async Task<IEnumerable<Movie>> GetAllIncludeActorsAsync()
        {


            string[] includeProperties = new string[] {nameof(Movie.Rating),
                                                      nameof(Movie.Director),
                                                      nameof(Movie.Genre),
                                                      nameof(Movie.MovieActor)};

            IQueryable<Movie> query = await Task.FromResult(GenerateQuery(filter: null,
                                                                         orderBy: null,
                                                                         includeProperties));
            return query.AsEnumerable();
        }

        public async Task<Movie> GetByIdIncludeActorsAsync(int id)
        {
            string[] includeProperties = new string[] {nameof(Movie.Rating),
                                                      nameof(Movie.Director),
                                                      nameof(Movie.Genre),
                                                      nameof(Movie.MovieActor)};
            IQueryable<Movie> query = await Task.FromResult(GenerateQuery(filter: (user => user.Id == id),
                                                                         orderBy: null,
                                                                         includeProperties));
            return query.SingleOrDefault();
        }
    }
}
