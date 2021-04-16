using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FluentValidation;

namespace Domain.Entities
{
    public class User : IIdentityEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public bool FlagAdmin { get; set; }

        public bool FlagActive { get; set; }

        private ICollection<Rating> _rating { get; set; }
        public virtual IReadOnlyCollection<Rating> Rating { get { return _rating as Collection<Rating>; } }

        public User()
        {
            this._rating = new Collection<Rating>();
        }

        public void AddItemRating(Rating rating)
        {
            _rating.Add(rating);
        }

       
    }
}
