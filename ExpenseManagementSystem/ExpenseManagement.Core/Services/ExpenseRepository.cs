using AutoMapper;
using ExpenseManagement.Data;
using ExpenseManagement.Model;
using ExpenseManagement.Model.DTOs;
using ExpenseManagement.Model.DTOs.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagement.Core.Services
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public ExpenseRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<BudgetDTO> AllocateInitialTotalBudget(BudgetDTO budget)
        {
            var budgetCount = _db.Budget.ToList();
            if (budgetCount.Count < 1)
            {
                var budgetObj = _mapper.Map<Budget>(budget);
                await _db.Budget.AddAsync(budgetObj);
                _db.SaveChanges();
                return budget;
            }

            else
            {
                throw new ArgumentException("The budget has already been created");
            }


        }

        public double ViewRemainingBudget()
        {
            var budget = _db.Budget.FirstOrDefault();
            var balance = budget.Balance;
            return balance;
        }

        public async Task<ExpenseDTO> AddExpense(string userId, ExpenseDTO request)
        {
            if (request == null)
            {
                throw new ArgumentException("The request is empty");
            }


            Expense expenseObj = ExpenseMapper.ExpenseInfo(request);
            expenseObj.UserId = userId;
            expenseObj.Id = Guid.NewGuid().ToString();
            expenseObj.Status = "Pending";

            await _db.Expenses.AddAsync(expenseObj);
            _db.SaveChanges();
            return request;
        }

        public IEnumerable<ExpenseDTO> ViewAllExpenses()
        {
            var expenseList = _db.Expenses.ToList();
            var myList = new List<ExpenseDTO>();
            foreach (var item in expenseList)
            {
                myList.Add(_mapper.Map<ExpenseDTO>(item));
            }
            return myList;
        }

        public ViewExpenseDTO ViewExpenseStatus(string expenseId)
        {
            var expense = _db.Expenses.Where(x => x.Id == expenseId).FirstOrDefault();
            if (expense == null)
            {
                throw new ArgumentException("This expense does not exist");
            }

            var expenseObj = _mapper.Map<ViewExpenseDTO>(expense);
            return expenseObj;
        }

        public bool ApproveRejectExpense(string expenseId, string decision)
        {
            var expense = _db.Expenses.Where(x => x.Id == expenseId).FirstOrDefault();
            if (expense == null)
            {
                throw new ArgumentException("This expense does not exist");
            }

            if (decision == "Approved")
            {
                var sufficientBudget = SufficientBudget(expense.Amount);
                if (sufficientBudget == true)
                {
                    var budget = _db.Budget.FirstOrDefault();
                    budget.Balance = budget.Balance - expense.Amount;

                    expense.Status = "Approved";
                    _db.Expenses.Update(expense);
                    _db.SaveChanges();
                    return true;

                }
            }

            if (decision == "Rejected")
            {
                expense.Status = "Rejected";
                _db.Expenses.Update(expense);
                _db.SaveChanges();
                return false;
            }

            if (decision != "Approved" && decision != "Rejected")
            {
                throw new ArgumentException("You did not enter the correct info in the decision box. The decision " +
                "can either be Approved or Rejected");
            }

            throw new ArgumentException("The balance in the budget is not sufficient for this transaction");
        }

        public bool SufficientBudget(double expenseAmount)
        {
            var budget = _db.Budget.FirstOrDefault();
            var budgetBalance = budget.Balance;
            var balanceAfterExpenseDeduction = budgetBalance - expenseAmount;
            if (balanceAfterExpenseDeduction > 0)
            {
                return true;
            }
            return false;
        }

        public bool UploadImage(string expenseId, string url)
        {
            Expense expenseObj = _db.Expenses.Where(x => x.Id == expenseId).FirstOrDefault();

            if (expenseObj != null)
            {
                expenseObj.AttachedFileUrl = url;
                _db.Expenses.Update(expenseObj);

                return _db.SaveChanges() >= 0 ? true : false;
            }

            throw new ArgumentException("Resource not found");
        }
    }
}
