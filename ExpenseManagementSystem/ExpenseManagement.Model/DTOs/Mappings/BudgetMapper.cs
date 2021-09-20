using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManagement.Model.DTOs.Mappings
{
    public class BudgetMapper : Profile
    {
        public BudgetMapper()
        {
            CreateMap<Budget, BudgetDTO>().ReverseMap();
        }
    }
}
