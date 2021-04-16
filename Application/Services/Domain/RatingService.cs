using Application.Interfaces.Services.Domain;
using Application.Services.Standard;
using Domain.Entities;
using Domain.ViewModels;
using Infrastructure.Interfaces.Repositories.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Domain
{
    public class RatingService : ServiceBase<Rating>,
                               IRatingService
    {
        private readonly IRatingRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IMovieRepository _movieRepository;

        public RatingService(IRatingRepository repository, IUserRepository userRepository, IMovieRepository movieRepository) : base(repository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public async Task<string> Vote(VoteModel v)
        {

            v.Rate = (byte)Math.Max(0, Math.Min(4, v.Rate));
            var user = this._userRepository.GetByIdAsync(v.UserId).Result;
            if (user == null)
                return "User not exists.";
            var movie = this._movieRepository.GetByIdAsync(v.MovieId).Result;
            if (movie == null)
                return "Movie not exists.";

            var vote = _repository.GetAllAsync().Result.FirstOrDefault(x => x.UserId == v.UserId && x.MovieId == v.MovieId);
            if (vote == null)
            {
                vote = new Rating();
                vote.MovieId = v.MovieId;
                vote.UserId = v.UserId;
                vote.Rate = v.Rate;
                await this._repository.AddAsync(vote);
            }
            else
            {
                vote.Rate = v.Rate;
                await this._repository.UpdateAsync(vote);
            }
            return "";
        }
    }
}
