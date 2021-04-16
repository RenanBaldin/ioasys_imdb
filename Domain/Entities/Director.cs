using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class Director : IIdentityEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        private ICollection<Movie> _movie { get; set; }
        public virtual IReadOnlyCollection<Movie> Movie { get { return _movie as Collection<Movie>; } }

         public Director()
        {
            this._movie = new Collection<Movie>();
        }

        public void AddItemMovie(Movie movie)
        {
            _movie.Add(movie);
        }

    }
}