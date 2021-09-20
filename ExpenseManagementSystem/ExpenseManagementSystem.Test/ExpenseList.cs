using ExpenseManagement.Core.Services;
using ExpenseManagement.Model;
using ExpenseManagement.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagementSystem.Test
{
    public class ExpenseList
    {
        public static IEnumerable<ExpenseDTO> ViewExpenseDTO()
        {
            return new List<ExpenseDTO>
                {
                new ExpenseDTO{
                    ExpenseDetails = "Plane Ticket",
                    Amount = 500.00,
                   
                },

                new ExpenseDTO
                {
                    
                    ExpenseDetails = "Rent Receipt",
                    Amount = 1000.00,
                   
                },

                new ExpenseDTO
                {
                    
                    ExpenseDetails = "Car Receipt",
                    Amount = 2000.00
                    
                }

        };
        }
    }
}



        
    

