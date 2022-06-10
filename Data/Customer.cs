using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PlantShop.Data
{
    public class Customer:People
    {
        [Column(TypeName = "nvarchar(20)")]

        //[StringLength((50), ErrorMessage = "City cannot be longer than 50 characters")]
        //[RegularExpression(@"^[A-Z][a-z]{1,50}$")]

        public string City { get; set; }
        [Column(TypeName = "nvarchar(50)")]

        public string Address { get; set; }
    }
}
