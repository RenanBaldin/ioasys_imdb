using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class MovieActor : IIdentityEntity
    {
        public int Id { get; set; }
         public string MovieId { get; set; }

        public Movie Movie { get; set; }

        public string ActorId { get; set; }

        public Actor Actor { get; set; }
    }
}
