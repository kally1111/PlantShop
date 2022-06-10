using PlantShop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-Z][a-z]{1,20}$")]
        [Display(Name ="Last name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression(@"^[A-Z][a-z]{1,20}$")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(10)]
        [RegularExpression(@"^0[7-9][0-9]{8}$", ErrorMessage = "Invalid phone number")]
        [Display(Name ="Phone number")]
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        public string Email { get; set; }
        [Required]
        public int ShopId { get; set; }
        public string Password { get; set; }
        public Shop Shop { get; set; }
        [Display(Name ="Shop name")]
        public string ShopName { get; set; }


    }
}
