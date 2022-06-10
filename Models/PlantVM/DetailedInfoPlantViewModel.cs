using Microsoft.AspNetCore.Http;
using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models.PlantVM
{
    public class DetailedInfoPlantViewModel
    {
        public int PlantId { get; set; }
        [Required]
        [StringLength((20), ErrorMessage = "Name cannot be longer than 20 characters")]
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
        public List<Shop> Shop { get; set; }
        public int ShopId { get; set; }
        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        public IFormFile Photo { get; set; }


    }
}
