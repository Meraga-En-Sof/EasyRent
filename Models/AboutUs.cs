using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class AboutUs
    {

        [Key]
        public int Id { get; set; }

        [MaxLength(30),Display(Name = "Header Title")]
        public string HeadTitle { get; set; }

        [MinLength(40)]
        public string Content { get; set; }



    }
}
