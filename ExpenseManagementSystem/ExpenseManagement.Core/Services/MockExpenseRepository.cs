using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Services
{
    public class MockExpenseRepository : IMockExpenseRepository
    {
        private readonly IExpenseRepository _query;
        public MockExpenseRepository(IExpenseRepository query)
        {
            _query = query;
        }
        public Task<ExpenseDTO> AddExpense(string userId, ExpenseDTO request)
        {
            return _query.AddExpense(userId, request);
        }

        public Task<BudgetDTO> AllocateInitialTotalBudget(BudgetDTO budget)
        {
            return _query.AllocateInitialTotalBudget(budget);
        }

        public bool ApproveRejectExpense(string expenseId, string decision)
        {
            return _query.ApproveRejectExpense(expenseId, decision);
        }

        public bool SufficientBudget(double expenseAmount)
        {
            return _query.SufficientBudget(expenseAmount);
        }

        public bool UploadImage(string expenseId, string url)
        {
            return _query.UploadImage(expenseId, url);
        }

        public IEnumerable<ExpenseDTO> ViewAllExpenses()
        {
            return _query.ViewAllExpenses();
        }

        public ViewExpenseDTO ViewExpenseStatus(string expenseId)
        {
            return _query.ViewExpenseStatus(expenseId);
        }

        public double ViewRemainingBudget()
        {
            return _query.ViewRemainingBudget();
        }
    }
}
