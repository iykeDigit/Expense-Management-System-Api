using ExpenseManagement.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Interfaces
{
    public interface IUserService
    {
        Task<bool> Update(string userId, UpdateUserRequest updateUser);
        Task<bool> DeleteUser(string userId);
        Task<UserDetailsDTO> GetUser(string userId);
    }
}
