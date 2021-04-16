using Application.Interfaces.Services.Standard;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IUserService : IServiceBase<User>
    {
        Task<User> GetByIdIncludingTasksAsync(int id);
        Task<User> UpdateActiveState(int id, bool AcTiveState);
        Task<IEnumerable<User>> GetAllWithPaginationAsync(MovieFilters filters);
        User getUserAuth(AuthFilter filter);
    }
}
