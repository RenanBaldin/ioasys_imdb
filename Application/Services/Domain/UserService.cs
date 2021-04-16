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
    public class UserService : ServiceBase<User>,
                           IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllWithPaginationAsync(MovieFilters filters)
        {
            return _repository.GetAllAsync().Result.OrderBy(u => u.Name)
                         .Skip((filters.Page - 1) * filters.ItemsPerPage)
                         .Take(filters.ItemsPerPage)
                         .ToList();
        }

        public async Task<User> GetByIdIncludingTasksAsync(int id)
        {
            return await _repository.GetByIdIncludingTasksAsync(id);
        }

        public async Task<User> UpdateActiveState(int id, bool AcTiveState)
        {
            var user = await _repository.GetByIdIncludingTasksAsync(id);

            if (user != null)
            {
                user.FlagActive = AcTiveState;
                await _repository.UpdateAsync(user);
            }
            return user;
        }


        public User getUserAuth(AuthFilter filter)
        {
            var user = _repository.GetAllAsync().Result.AsEnumerable();

            return user.Where(x => x.Email.ToLower().Contains(filter.Email.ToLower()) && x.Password.ToLower().Contains(filter.Password.ToLower())).FirstOrDefault();
        }
    }
}
