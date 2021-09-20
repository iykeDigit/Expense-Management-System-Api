using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.DTOs.Mappings
{
    public class ExpenseMapper : Profile
    {
        public ExpenseMapper()
        {
            CreateMap<Expense, ExpenseDTO>().ReverseMap();
        }

        public static Expense ExpenseInfo(ExpenseDTO request)
        {
            return new Expense
            {
                ExpenseDetails = request.ExpenseDetails,
                Amount = request.Amount,
                

            };
        }


    }
}
