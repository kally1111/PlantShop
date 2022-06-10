using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlantShop.DataAccess;
using PlantShop.Models;
using PlantShop.Models.PlantVM;
using PlantShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantService plantService;
        private readonly PlantShopDbContext db;

       
        public PlantController(IPlantService plantService, PlantShopDbContext db)
        {
            this.plantService = plantService;
            this.db = db;
        }
       
        public IActionResult Get(GetPlantViewModel getPlantViewModel,int page = 1)
        {
            return View(this.plantService.Get( getPlantViewModel,page));

        }
        [HttpGet]
        [Authorize]

        public IActionResult Create()
        {
            ViewData["ShopId"] = new SelectList(db.Shopes, "Id", "ShopName");

            return View();
        }
        [HttpPost]
        [Authorize]

        public IActionResult Create(CreatePlantViewModel plant)
        {
            this.plantService.Create(plant);
           
            return RedirectToAction(nameof(Get));
        }
       
        public IActionResult InfoPlant(int id)
        {
            return View(this.plantService.InfoPlant(id));
        }
        [HttpPost]
        [Authorize]

        public IActionResult Change(DetailedInfoPlantViewModel plant)
        {
            ViewData["ShopId"] = new SelectList(db.Shopes, "Id", "ShopName");
            this.plantService.Change(plant);
            return RedirectToAction(nameof(Get));
        }
        [Authorize]

        public IActionResult UpdateData(string searchString)
        {
            ViewData["FilterPlant"] = searchString.ToLower();
            return View(this.plantService.UpdateData(searchString));
        }
        [Authorize]

        public IActionResult DetailedInfo(int id)
        {
            ViewData["ShopId"] = new SelectList(db.Shopes, "Id", "ShopName");
            return View(this.plantService.DetailedInfo(id));
        }

        [HttpGet]
        [Authorize]

        public IActionResult Delete(int id)
        {
            return View(this.plantService.Delete(id));
        }
        [Authorize]

        public IActionResult ConfermedDelete(int id)
        {
            this.plantService.ConfermedDelete(id);
            return RedirectToAction(nameof(Get));
        }
        public IActionResult GetByShop(int id, int page=1)
        {
            return View(this.plantService.GetByShop(id,page));
        }
    }
}
