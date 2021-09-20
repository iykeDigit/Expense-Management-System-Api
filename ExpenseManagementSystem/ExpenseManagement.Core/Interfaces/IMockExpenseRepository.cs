using ExpenseManagement.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Interfaces
{
    public interface IMockExpenseRepository
    {
        Task<BudgetDTO> AllocateInitialTotalBudget(BudgetDTO budget);
        double ViewRemainingBudget();
        Task<ExpenseDTO> AddExpense(string userId, ExpenseDTO request);
        IEnumerable<ExpenseDTO> ViewAllExpenses();
        bool ApproveRejectExpense(string expenseId, string decision);
        bool SufficientBudget(double expenseAmount);
        ViewExpenseDTO ViewExpenseStatus(string expenseId);
        bool UploadImage(string expenseId, string url);
    }
}
