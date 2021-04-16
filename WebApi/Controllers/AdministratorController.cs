
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
    public class AdministratorController : ControllerBase
    {
        private readonly IAdminstratorService _admService;

        public AdministratorController(IAdminstratorService admService)
        {
            _admService = admService;
        }

        /// <summary>
        /// Get a list of administrator users.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Administrator
        ///
        /// </remarks>
        /// <returns>Collection of admins.</returns>
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
                var users = _admService.GetAllIncludingTasksAsync();

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
        /// Get a admin by id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Administrator/1
        ///
        /// </remarks>
        /// <returns>Collection of admin.</returns>
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
                var users = await _admService.GetByIdAsync(id);
                if (users == null)
                {
                    return NotFound();
                }
                if (users.FlagAdmin == false)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "This user not is of type admin");
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