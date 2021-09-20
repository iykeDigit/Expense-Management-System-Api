using ExpenseManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Interfaces
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(AppUser user);
    }
}
