using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.DTOs
{
    public class ViewExpenseDTO
    {
        public string ExpenseDetails { get; set; }
        public Double Amount { get; set; }
        public string AttachedFileUrl { get; set; }

        public string Status { get; set; }

    }
}
