using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlantShop.DataAccess;
using PlantShop.Models;
using PlantShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly PlantShopDbContext db;
        public EmployeeController(IEmployeeService employeeService, PlantShopDbContext db)
        {
            this.employeeService = employeeService;
            this.db = db;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(this.employeeService.Index());
        }
        [Authorize]
        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(db.Shopes, "Id", "ShopName");

            return View();
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(EmployeeViewModel employee)
        {
            this.employeeService.Create(employee);

            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        public IActionResult Details(int id)
        {
            return View(this.employeeService.Details(id));
        }
        [Authorize]
        public IActionResult UpdateData(string searchString)
        {
            ViewData["FilterEmployee"] = searchString.ToLower();
            return View(this.employeeService.UpdateData(searchString));
        }
        [Authorize]
        public IActionResult DetailedInfo(int id)
        {
            return View(this.employeeService.DetailedInfo(id));
        }
        [Authorize]
        public IActionResult Change(EmployeeViewModel employee)
        {
            this.employeeService.Change(employee);
            return RedirectToAction(nameof(Index));

        }
        [Authorize]
        public IActionResult Delete(int id)
        {
            return View(this.employeeService.Delete(id));
        }
        [Authorize]

        public IActionResult ConfermedDelete(int id)
        {
            this.employeeService.ConfermedDelete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GetByShop(int id)
        {
            return View(this.employeeService.GetByShop(id));
        }
    }
}
