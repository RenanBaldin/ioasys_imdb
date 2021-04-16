using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Rating : IIdentityEntity
    {
        public int Id { get; set; }

        public int Rate { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
