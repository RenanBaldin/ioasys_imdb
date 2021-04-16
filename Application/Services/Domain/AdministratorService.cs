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
    public class AdministratorService : ServiceBase<User>,
                           IAdminstratorService
    {
        private readonly IUserRepository _repository;

        public AdministratorService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetAllIncludingTasksAsync()
        {
            return _repository.GetAllIncludingTasksAsync().Result.Where(x => x.FlagAdmin == true);
        }

        public User GetByIdIncludingTasksAsync(int id)
        {
            var result = _repository.GetByIdIncludingTasksAsync(id).Result;
            if (result.FlagAdmin)
                return result;
            else return null;
        }
    }
}