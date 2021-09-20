using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model
{
    public class Expense
    {
        public string Id { get; set; }  
        public string UserId { get; set; }
        [Required]
        public string ExpenseDetails { get; set; }
        [Required]
        public Double Amount { get; set; }
        
        public string AttachedFileUrl { get; set; }
        public string Status { get; set; }
    }
}
