using ExpenseManagement.Model;
using ExpenseManagement.Model.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Services
{
    public interface IExpenseRepository
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