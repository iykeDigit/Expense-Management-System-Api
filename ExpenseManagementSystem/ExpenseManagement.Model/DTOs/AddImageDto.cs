using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.DTOs
{
    public class AddImageDto
    {
        public IFormFile Image { get; set; }
    }
}
