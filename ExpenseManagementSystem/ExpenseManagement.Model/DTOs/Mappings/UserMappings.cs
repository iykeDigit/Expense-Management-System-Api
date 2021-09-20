using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.DTOs.Mappings
{
    public class UserMappings
    {
        public static UserDetailsDTO GetUserDetails(AppUser user) 
        {
            return new UserDetailsDTO
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FirstName + " " + user.LastName,
                PhoneNumber = user.PhoneNumber,
                Department = user.Department
            };
        }

        public static AppUser RegisterUser(RegistrationRequestDTO request) 
        {
            return new AppUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Department = request.Department,
                PhoneNumber = request.PhoneNumber,
                UserName = string.IsNullOrWhiteSpace(request.Username) ? request.Email : request.Username
            };
        }
    }
}
