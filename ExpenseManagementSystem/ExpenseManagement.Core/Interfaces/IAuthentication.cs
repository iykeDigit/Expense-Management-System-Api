using ExpenseManagement.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Interfaces
{
    public interface IAuthentication
        {
            Task<UserDetailsDTO> Login(UserRequestDTO request);
            Task<UserDetailsDTO> Register(RegistrationRequestDTO registrationRequest);
        }
    }
