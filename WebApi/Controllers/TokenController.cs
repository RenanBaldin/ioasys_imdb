
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
using Domain.Dto;
using WebApi.Auth;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IUserService _userService;

        public TokenController(IUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// generater token
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/v1/Home/login
        ///     {
        ///         "email": "fulano@ioasys.com",
        ///         "password": "ioasystest"
        ///     }
        ///
        /// </remarks>
        /// <param name="filter"></param>
        /// <returns>return a token.</returns>
        /// <response code="200">return a token.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] AuthFilter filter)
        {
            try
            {
                var user = _userService.getUserAuth(filter);
                if (user == null)
                    return StatusCode(StatusCodes.Status401Unauthorized, "User not found.");


                var token = TokenService.GeradorDeToken(user);
                user.Password = string.Empty;
                return Ok(new
                {
                    data = new
                    {
                        userAuth = user,
                        token = token
                    }
                });
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Request Error");
            }
        }




    }

}