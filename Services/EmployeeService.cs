using PlantShop.Data;
using PlantShop.DataAccess;
using PlantShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PlantShop.Services
{
    public class EmployeeService:IEmployeeService
    {

        private readonly PlantShopDbContext db;
        public EmployeeService(PlantShopDbContext db)
        {
            this.db = db;
            
        }
        public List<EmployeeViewModel> Index()
        {
              return db.Employees.Include(e=>e.Shop).Where(e=>e.Id!=1).ToList().Select(e => MapEmployee(e)).ToList();
           
        }
        public void Create(EmployeeViewModel employee)
        {
            db.Add(MapEmployee(employee));
            db.SaveChanges();
        }
        public EmployeeViewModel Details(int id)
        {
            return MapEmployee(this.db.Employees.Include(e=>e.Shop).FirstOrDefault(e => e.Id == id));
        }
        public List<EmployeeViewModel> UpdateData(string searchString)
        {
            var q = this.db.Employees.Include(e=>e.Shop).ToList()
               .Where(e => e.FullName.Contains(searchString)).Select(s => MapEmployee(s))
               .OrderBy(e => e.Id).ToList();
            return q;
        }
        public EmployeeViewModel DetailedInfo(int id)
        {
            return MapEmployee(this.db.Employees.Include(e => e.Shop).FirstOrDefault(pl => pl.Id == id));
        }
        public void Change(EmployeeViewModel employee)
        { 
            var EmployeeToChange = db.Employees.FirstOrDefault(x => x.Id == employee.Id);
            EmployeeToChange.FirstName = employee.FirstName;
            EmployeeToChange.LastName = employee.LastName;
            EmployeeToChange.PhoneNumber = employee.PhoneNumber;
            EmployeeToChange.Email = employee.Email;
            EmployeeToChange.Password = employee.Password;
            EmployeeToChange.ShopId = employee.ShopId;
       
            db.SaveChanges();
        }
        public EmployeeViewModel Delete(int id)
        {
            return MapEmployee(this.db.Employees.Include(x => x.Shop)
                .FirstOrDefault(p => p.Id == id));
        }
        public void ConfermedDelete(int id)
        {
            var EmployeeToDelete = this.db.Employees.Include(x => x.Shop)
                .FirstOrDefault(p => p.Id == id);
           
            db.Remove(EmployeeToDelete);
            db.SaveChanges();
        }
        public List<EmployeeViewModel> GetByShop(int id)
        {
            return db.Employees.Include(e=>e.Shop).Where(e => e.ShopId == id).ToList().Select(e => MapEmployee(e)).ToList();
        }
        private EmployeeViewModel MapEmployee(Employee employee)
        {
            if (employee == null)
            {
                return new EmployeeViewModel();
            }
            return new EmployeeViewModel
            {
            Id=employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            Password = employee.Password,
            ShopId = employee.ShopId,
           ShopName=employee.Shop.ShopName
            };
        }
        private Employee MapEmployee(EmployeeViewModel employee)
        {
            return new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email,
                Password = employee.Password,
                ShopId = employee.ShopId
            };
        }
    }
}
