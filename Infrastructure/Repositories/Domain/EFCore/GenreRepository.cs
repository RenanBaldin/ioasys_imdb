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
    public class GenreRepository : DomainRepository<Genre>,
                                  IGenreRepository
    {
        public GenreRepository(ApplicationContext dbContext) : base(dbContext)
        {

        }
    }
}
