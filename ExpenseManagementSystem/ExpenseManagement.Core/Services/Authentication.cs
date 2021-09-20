using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Model;
using ExpenseManagement.Model.DTOs;
using ExpenseManagement.Model.DTOs.Mappings;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Services
{
    public class Authentication : IAuthentication
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        public Authentication(UserManager<AppUser> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserDetailsDTO> Login(UserRequestDTO request) 
        {
            AppUser user = await _userManager.FindByEmailAsync(request.Email);
            if(user != null) 
            {
                if(await _userManager.CheckPasswordAsync(user, request.Password)) 
                {
                    var response = UserMappings.GetUserDetails(user);
                    response.Token = await _tokenGenerator.GenerateToken(user);

                    return response;
                }

                throw new AccessViolationException("Invalid credentials");
            }
            throw new AccessViolationException("Invalid Credentials");
        }

        public async Task<UserDetailsDTO> Register(RegistrationRequestDTO registrationRequest) 
        {
            AppUser user = UserMappings.RegisterUser(registrationRequest);

            IdentityResult result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded) 
            {
                return UserMappings.GetUserDetails(user);
            }

            string errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);

        }
    }
}
