using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models.PlantVM
{
    public class UpdateDataPVM
    {
        public int PlantId { get; set; }
        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        public double? Price { get; set; }
        public string PhotoPath { get; set; }
        [Display(Name = "Type of plant")]
        public string TypeOfPlant { get; set; }
        public Shop Shop { get; set; }
        public int ShopId { get; set; }
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }

    }
}
