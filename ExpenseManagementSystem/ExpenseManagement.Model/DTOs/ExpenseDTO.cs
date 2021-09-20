using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.DTOs
{
    public class ExpenseDTO
    {
        [Required]
        public string ExpenseDetails { get; set; }
        [Required]
        public Double Amount { get; set; }
        
        //public string AttachFile { get; set; }

    }
}
