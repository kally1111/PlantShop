using Microsoft.AspNetCore.Http;
using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models.PlantVM
{
    public class GetPlantViewModel
    {
        public IQueryable<Plant> Query { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string OrderBy { get; set; }
        public string SortByTypeOfPlant { get; set; }
        public string SortByPlaceToPlant { get; set; }
        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        public double? Price { get; set; }

    }
}
