
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.DBConfiguration.EFCore;
using Application.Services.Domain;
using Application.Interfaces.Services.Domain;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net;
using Domain.Dto;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IRatingService _rateService;

        public MovieController(IMovieService movieService, IRatingService ratingService)
        {
            _movieService = movieService;
            _rateService = ratingService;
        }

        /// <summary>
        /// Get a list of movies.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Movie
        ///
        /// </remarks>
        /// <returns>Collection of movie.</returns>
        /// <response code="200">A collection of movies.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var movies = await _movieService.GetAllWithIncludeAllsync();

                if (movies == null)
                {
                    return NotFound();
                }
                return Ok(movies);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }

        }
       
        /// <summary>
        /// Get a list of movies with filter and pagination.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Post /Movie/getWithPage
        ///     {
        /// 
        ///         "diretor": "Anthony",
        ///         "genre": "",
        ///         "name": "",
        ///         "actor": "",
        ///         "page": 1,
        ///         "itemsPerPage":1   
        ///     }
        /// </remarks>
        /// <returns>Collection of movie.</returns>
        /// <params>filters</params>
        /// <response code="200">A collection of movies.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost("getWithPage")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] MovieFilters filters)
        {
            try
            {
                var movies = _movieService.GetAllWithPaginationAsync(filters);

                if (movies == null)
                {
                    return NotFound();
                }
                return Ok(movies);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }

        }
      
        /// <summary>
        /// Get a movie by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Movie/1
        ///
        /// </remarks>
        /// <returns>Collection of movie.</returns>
        /// <response code="200">A collection of movies.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return NotFound();
            try
            {
                var movies = await _movieService.GetByIdAsync(id);

                if (movies == null)
                {
                    return NotFound();
                }
                return Ok(movies);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        /// <summary>
        /// Create a Movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movie
        ///     {
        ///       "name": "Vingadores Ultimato"
        ///       "description":"Em Vingadores: Ultimato, após Thanos eliminar metade das criaturas vivas em Vingadores: Guerra Infinita, os heróis precisam lidar com a dor da perda de ...",
        ///       "directorId": 1,
        ///       "genreId": 1
        ///     }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Movie created.</returns>
        /// <response code="200">A collection of movies.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] Movie command)
        {
            try
            {
                if (command == null)
                    return NotFound();

                var result = await _movieService.AddAsync(command);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }
        }

        /// <summary>
        /// post a Vote.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movie/Vote
        ///     {
        ///       "userId": 1,
        ///       "movieId": 7,
        ///       "rate": 4
        ///     }
        /// </remarks>
        /// <param name="vote"></param>
        /// <returns>Ok</returns>
        /// <response code="200">Vote is created.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost("Vote")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] VoteModel vote)
        {
            try
            {
                if (vote.UserId == 0 || vote.MovieId == 0)
                    return NotFound();

                var result = await _rateService.Vote(vote);
                if (!string.IsNullOrEmpty(result))
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }
        }
        /// <summary>
        /// Update a Movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Movie
        ///     {
        ///        
        ///        "id": 1,
        ///        "name": "Update Name",
        ///        "description": "string",
        ///        "directorId": 1,
        ///        "genreId": 2
        ///        
        ///     }
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Ok</returns>
        /// <response code="200">A collection of movies.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] Movie command)
        {
            try
            {
                if (command == null)
                    return NotFound();

                await _movieService.UpdateAsync(command);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }
        }


        /// <summary>
        /// Remove a Movie.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /Movie/1
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Ok</returns>
        /// <response code="200">A collection of movies.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                await _movieService.RemoveAsync(id);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }

        }

       



    }
}