using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Dto;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Domain
{
    public class MovieService : ServiceBase<Movie>,
                           IMovieService
    {
        private readonly IMovieRepository _repository;

        public MovieService(IMovieRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Movie>> GetAllWithIncludeAllsync()
        {
            return await _repository.GetAllAsync();
        }

        public IEnumerable<dynamic> GetAllWithPaginationAsync(MovieFilters filters)
        {
            var movies = _repository.GetAllIncludeActorsAsync().Result.AsEnumerable();
            List<dynamic> result = new List<dynamic>();
            if (!string.IsNullOrEmpty(filters.Name))
            {
                movies = movies.Where(x => x.Name.ToLower().Contains(filters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filters.Genre))
            {
                movies = movies.Where(x => x.Genre.Name.ToLower().Contains(filters.Genre.ToLower()));
            }

            if (!string.IsNullOrEmpty(filters.Diretor))
            {
                movies = movies.Where(x => (x.Director.FirstName.ToLower().Contains(filters.Diretor.ToLower()) || (x.Director.LastName.ToLower().Contains(filters.Diretor.ToLower()))));
            }

            if (!string.IsNullOrEmpty(filters.Actor))
            {
                movies = movies.Where(x => x.MovieActor.Any(y => y.Actor.FirstName.ToLower().Contains(filters.Actor.ToLower())));
            }

            if (filters.ItemsPerPage != 0 && filters.Page != 0)
            {
                movies = movies.Skip((filters.Page - 1) * filters.ItemsPerPage).Take(filters.ItemsPerPage);
            }

            movies = movies.OrderByDescending(x => x?.Rating?.Average(y => y?.Rate));

            foreach (var m in movies)
            {
                result.Add(new
                {
                    id = m.Id,
                    name = m.Name,
                    description = m.Description,
                    director = m.Director,
                    actors = m.MovieActor,
                    genre = m.Genre,
                    rating = m?.Rating?.Average(y => y?.Rate)??0
                });
            }

            return result;
        }


    }
}
