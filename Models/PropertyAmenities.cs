using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class PropertyAmenities
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        //Relationships

        public Property Property { get; set; }

        [Display(Name = "Property")]
        public int PropertyId { get; set; }
    }
}
