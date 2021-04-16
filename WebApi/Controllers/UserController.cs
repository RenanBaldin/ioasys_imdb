
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
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Create a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///       "name": "Usuário Teste 1",
        ///       "lastName": "Fulano",
        ///       "email": "fulano@ioasys.com",
        ///       "password": "ioasystest",
        ///       "flagAdmin": false,
        ///       "flagActive": true
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>User created.</returns>
        /// <response code="200">A collection of users.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] User command)
        {
            try
            {
                if (command == null)
                    return NotFound();

                var result = await _userService.AddAsync(command);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }
        }

        /// <summary>
        /// Update a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /User
        ///     {
        ///       "name": "Usuário Teste 1",
        ///       "lastName": "Fulano",
        ///       "email": "fulano@ioasys.com",
        ///       "password": "ioasystest",
        ///       "flagAdmin": false,
        ///       "flagActive": true
        ///     }
        ///
        /// </remarks>
        /// <param name="command"></param>
        /// <returns>Ok</returns>
        /// <response code="200">A collection of users.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] User command)
        {
            try
            {
                if (command == null)
                    return NotFound();

                await _userService.UpdateAsync(command);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }
        }


        /// <summary>
        /// Inativate a User.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     Delete /User/1
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Ok</returns>
        /// <response code="200">A collection of users.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                await _userService.UpdateActiveState(id, false);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }

        }

        /// <summary>
        /// Get a list of users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /User
        ///
        /// </remarks>
        /// <returns>Collection of user.</returns>
        /// <response code="200">A collection of users.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>
        [HttpGet]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var users = await _userService.GetAllAsync();

                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }

        }
        /// <summary>
        /// Get a user by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /User/1
        ///
        /// </remarks>
        /// <returns>Collection of user.</returns>
        /// <response code="200">A collection of users.</response>
        /// <response code="500">Internal server error.</response>
        /// <response code="404">Not Found.</response>

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return NotFound();
            try
            {
                var users = await _userService.GetByIdAsync(id);

                if (users == null)
                {
                    return NotFound();
                }
                return Ok(users);
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

    }
}