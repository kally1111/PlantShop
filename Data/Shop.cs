using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
    public class Shop
    {
        
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string ShopName { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string City { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Address { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        [Display(Name ="Phone number")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Email { get; set; }
        public string PhotoPath { get; set; }
        [InverseProperty("Shop")]
        public List<Plant> Plants { get; set; }
        [InverseProperty("Shop")]
        public List<Employee> Employees { get; set; }
     
    
    }
}
