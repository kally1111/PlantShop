using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Services
{
         public  interface IEmployeeService
        {
        List<EmployeeViewModel> Index();
        void Create(EmployeeViewModel employee);
        EmployeeViewModel Details(int id);
        List<EmployeeViewModel> UpdateData(string search);
        EmployeeViewModel DetailedInfo(int id);
        void Change(EmployeeViewModel employee);
        EmployeeViewModel Delete(int id);
        void ConfermedDelete(int id);
        List<EmployeeViewModel> GetByShop(int id);
         }
}
