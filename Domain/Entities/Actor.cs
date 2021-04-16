using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Domain.Entities
{
    public class Actor : IIdentityEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        private ICollection<MovieActor> _movieActor { get; set; }
        public virtual IReadOnlyCollection<MovieActor> MovieActor { get { return _movieActor as Collection<MovieActor>; } }    

        public Actor()
        {
            this._movieActor = new Collection<MovieActor>();
        }
        public void AddItemMovieActor(MovieActor movieActor)
        {
            _movieActor.Add(movieActor);
        }           
    }
}