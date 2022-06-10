using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
  
    public class Employee:People
    {

        [Column(TypeName = "nvarchar(10)")]
        public string Password { get; set; }
        [Required]
        public int ShopId { get; set; }
        [ForeignKey("ShopId")]
        public Shop Shop { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string FullName
        {
            get
            {
                return this.FirstName + this.LastName;
            }
        }
    }
}
