using Application.Interfaces.Services.Standard;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IAdminstratorService : IServiceBase<User>
    {
        IEnumerable<User> GetAllIncludingTasksAsync();
    }
}
