using Application.Interfaces.Services.Domain;
using Application.Interfaces.Services.Standard;
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
    public class ActorrService : ServiceBase<Actor>,
                           IActorService
    {
        private readonly IActorRepository _repository;

        public ActorrService(IActorRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<Actor> GetAllIncludingTasksAsync()
        {
            throw new NotImplementedException();
        }
    }
}