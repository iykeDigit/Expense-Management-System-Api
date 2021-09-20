using ExpenseManagement.Core.Interfaces;
using ExpenseManagement.Core.Services;
using ExpenseManagementSystem.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExpenseManagementSystem.Test
{
    public class ViewExpenseShould
    {
        private readonly IExpenseRepository query;
        public ViewExpenseShould()
        {
            //var expenseId = "";
            //IQueries mockquery = Mock.Of<IQueries>(
            //    x => x.DisplayCity() == MyLists.OrderDetails()
            //    );
            //query = mockquery;
            IExpenseRepository mockquery = Mock.Of<IExpenseRepository>(
                x => x.ViewAllExpenses() == ExpenseList.ViewExpenseDTO()
                );
            query = mockquery;
        }

        [Fact]
        public void GetAllExpenses()
        {
            //Arrange
            // EReport report = new EReport(query);
            MockExpenseRepository expenseRepo = new MockExpenseRepository(query);

            //ACT
            //var actual = report.DisplayCity().ToList();
            var actual = expenseRepo.ViewAllExpenses();
            var result = expenseRepo.ViewAllExpenses();

            //Assert
            Assert.NotEmpty(actual);
            Assert.True(actual == null);
        }
    }
}
