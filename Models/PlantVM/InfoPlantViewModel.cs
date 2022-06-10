using Microsoft.AspNetCore.Http;
using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models.PlantVM
{
    public class InfoPlantViewModel
    {
        public int PlantId { get; set; }
        [Required]
        [StringLength((50), ErrorMessage = "Name cannot be longer than 50 characters")]
        [Display(Name = "Plant Name")]
        public string PlantName { get; set; }
        public double? Price { get; set; }
        [StringLength((500), ErrorMessage = "Description cannot be longer than 500 characters")]
        public string Description { get; set; }
        public string PhotoPath { get; set; }
        [Display(Name = "Type of plant")]
        public string TypeOfPlant { get; set; }
        [Display(Name = "Place to plant")]
        public string PlaceToPlant { get; set; }
        public Shop Shop { get; set; }
        [Display(Name = "Shop Name")]
        public String ShopName { get; set; }
        public int ShopId { get; set; }
    }
}
