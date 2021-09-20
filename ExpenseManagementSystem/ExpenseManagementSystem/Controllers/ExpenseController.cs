using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Core.Services;
using ExpenseManagement.Data;
using ExpenseManagement.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class ExpenseController : ControllerBase
    {
        public readonly IExpenseRepository _expenseRepository;
        private readonly IConfiguration _config;
        private readonly IImageService _imageService;
        public ExpenseController(IExpenseRepository expenseRepository, IConfiguration config, IImageService imageService)
        {
            _expenseRepository = expenseRepository;
            _config = config;
            _imageService = imageService;
        }

        [HttpPost("Allocate-Budget")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllocateBudget([FromBody] BudgetDTO request) 
        {
            try 
            {
                var result = await _expenseRepository.AllocateInitialTotalBudget(request);
                return Created("", result);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            
        }

        [HttpPost("Add-Expense")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseDTO request)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
                var result = await _expenseRepository.AddExpense(userId, request);
                return Created("", result);
            }

            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }



        }

        [HttpGet("ExpenseId")]
        [Authorize(Roles = "Admin, Regular")]
        public IActionResult ViewExpenseStatus(string expenseId) 
        {
            try
            {
                return Ok(_expenseRepository.ViewExpenseStatus(expenseId));
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

        [HttpPatch("ApproveOrRejectExpense")]
        [Authorize(Roles = "Admin")]
        public IActionResult ApproveRejectExpense(string expenseId, string decision) 
        {
            try
            {
                if (!string.IsNullOrEmpty(expenseId) && !string.IsNullOrEmpty(decision)) 
                {
                    var result = _expenseRepository.ApproveRejectExpense(expenseId, decision);
                    return Ok($"The expense with expenseId {expenseId} has been {decision}");
                    
                }
                
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            catch (Exception)
            {
                return StatusCode(500);
            }

            throw new ArgumentException("The expenseId and decision parameters should not be empty");

        }

        [HttpGet("BudgetBalance")]
        [Authorize(Roles = "Admin, Regular")]
        public IActionResult ViewBudgetBalance() 
        {
            try
            {
                var balance = _expenseRepository.ViewRemainingBudget();
                return Ok($"The budget's balance is {balance}");
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

        [HttpGet("GetAllExpenses")]
        [Authorize(Roles = "Admin, Regular")]
        public IActionResult ViewAllExpenses() 
        {
            try
            {
                var expenses = _expenseRepository.ViewAllExpenses();
                return Ok(expenses);
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

        [HttpPatch("expense - fileUpload/id")]
        [Authorize(Roles = "Admin, Regular")]
        public async Task<IActionResult> UploadImage([FromForm] AddImageDto imageDto, string expenseId)
        {
            try
            {
                var response = "";
                var upload = await _imageService.UploadAsync(imageDto.Image);
                var imageProperties = new ImageAddedDTO()
                {
                    PublicId = upload.PublicId,
                    Url = upload.Url.ToString()
                };

                string url = imageProperties.Url.ToString();
                //var result = await _userService.UploadImage(userId, url);
                var result = _expenseRepository.UploadImage(expenseId, url);


                if (result)
                {
                    response = "Photo updated successfully";
                }
                return Ok(response);

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
