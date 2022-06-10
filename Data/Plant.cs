using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
   
    public  class Plant
    {
        
        public int Id { get; set; }


        [Required]
       
        [Column(TypeName = "nvarchar(20)")]

        public string PlantName { get; set; }
        public double? Price { get; set; }

        public string Description { get; set; }
     

        public string PhotoPath { get; set; }
        [Column(TypeName = "nvarchar(10)")]

        public string TypeOfPlant { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string PlaceToPlant { get; set; }
        [Required]
        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public Shop Shop{ get; set; }
    }
}
