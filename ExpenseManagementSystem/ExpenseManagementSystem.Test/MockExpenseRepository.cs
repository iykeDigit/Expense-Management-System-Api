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
    public class MockExpenseRepository : IExpenseRepository
    {
        private readonly List<Expense> _expenses;
        public MockExpenseRepository()
        {
            _expenses = new List<Expense>()
            {
                //    new ShoppingItem() { Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                //        Name = "Orange Juice", Manufacturer="Orange Tree", Price = 5.00M },
                //}
                new Expense()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200").ToString(),
                    UserId = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c211").ToString(),
                    ExpenseDetails = "Plane Ticket",
                    Amount = 500.00,
                    AttachedFileUrl = "",
                    Status = "Pending"
                }

            };
        }

        
            



       
                
                
           
        

        public Task<ExpenseDTO> AddExpense(string userId, ExpenseDTO request)
        {
            throw new NotImplementedException();
        }

        public Task<BudgetDTO> AllocateInitialTotalBudget(BudgetDTO budget)
        {
            throw new NotImplementedException();
        }

        public bool ApproveRejectExpense(string expenseId, string decision)
        {
            throw new NotImplementedException();
        }

        public bool SufficientBudget(double expenseAmount)
        {
            throw new NotImplementedException();
        }

        public bool UploadImage(string expenseId, string url)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExpenseDTO> ViewAllExpenses()
        {
            throw new NotImplementedException();
        }

        public ViewExpenseDTO ViewExpenseStatus(string expenseId)
        {
            throw new NotImplementedException();
        }

        public double ViewRemainingBudget()
        {
            throw new NotImplementedException();
        }
    }
}
