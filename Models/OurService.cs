using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class OurService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Icon Name")]
        [Required]
        public string IconName { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
