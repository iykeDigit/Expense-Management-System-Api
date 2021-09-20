using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Model;
using ExpenseManagement.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        //private readonly IImageService _imageService;
        private readonly UserManager<AppUser> _userManager;


        public UserController(IUserService userService, IConfiguration config, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _config = config;
            _userManager = userManager;
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UpdateUserRequest updateUserRequest)
        {
            try 
            {
                var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _userService.Update(userId, updateUserRequest);
                return NoContent();
            }
            catch(MissingMemberException ex) 
            {
                return BadRequest(ex.Message);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("userId")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                return Ok(await _userService.GetUser(userId));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string userId) 
        {
            try
            {
                await _userService.DeleteUser(userId);
                return NoContent();
            }
            catch (MissingMemberException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
