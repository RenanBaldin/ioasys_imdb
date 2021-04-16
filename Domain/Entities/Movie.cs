using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
namespace Domain.Entities
{
    public class Movie : IIdentityEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        
        public int DirectorId { get; set; }
        public virtual Director Director { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        private ICollection<Rating> _rating { get; set; }
        public virtual IReadOnlyCollection<Rating> Rating { get { return _rating as Collection<Rating>; } }
        private ICollection<MovieActor> _movieActor { get; set; }
        public virtual IReadOnlyCollection<MovieActor> MovieActor { get { return _movieActor as Collection<MovieActor>; } }        

        public Movie()
        {
            this._rating = new Collection<Rating>();
            this._movieActor = new Collection<MovieActor>();
        }

        public void AddItemRating(Rating rating)
        {
            _rating.Add(rating);
        }

        public void AddItemMovieActor(MovieActor movieActor)
        {
            _movieActor.Add(movieActor);
        }        

    }
}