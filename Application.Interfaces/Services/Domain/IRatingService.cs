using Application.Interfaces.Services.Standard;
using Domain.Entities;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Domain
{
    public interface IRatingService : IServiceBase<Rating>
    {
         Task<string> Vote(VoteModel vote);
    }
}
