using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class Testimonials
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(10)]
        public string Testimony { get; set; }

        public string ImageName { get; set; }

        [NotMapped, Display(Name = "Upload File")]
        public IFormFile UploadedFile { get; set; }


        //Relationship

        public User User { get; set; }


        [Required, Display(Name = "User")]
        public string UserId { get; set; }

        [Required]
        public bool isApproved { get; set; }


    }
}
