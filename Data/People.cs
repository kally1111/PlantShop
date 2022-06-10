using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
    public  abstract class People
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string FirstName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string Email { get; set; }
    }
}
