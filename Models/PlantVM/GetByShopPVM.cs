using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models.PlantVM
{
    public class GetByShopPVM
    {
        public int Id { get; set; }

        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        public double? Price { get; set; }
        public string PhotoPath { get; set; }
        public IQueryable<Plant> Query { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int ShopId { get; set; }
    }
}
