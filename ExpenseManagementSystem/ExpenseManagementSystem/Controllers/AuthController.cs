using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Model.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                return Ok(await _authentication.Login(userRequest));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpPost("Register")]
        //public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequest)
        //{
        //    try
        //    {
        //        var result = await _authentication.Register(registrationRequest);
        //        //return CreatedAtAction(nameof(Login), new { Id = result.Id }, result);
        //        return Created("", result);
        //    }
        //    catch (MissingFieldException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }
}
