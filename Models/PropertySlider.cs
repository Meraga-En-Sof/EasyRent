using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class PropertySlider
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }

        [NotMapped, Display(Name = "Upload File")]
        public IFormFile UploadedFile { get; set; }

        //Relationship
        public Property Properties { get; set; }

        [Display(Name = "Property")]
        public int PropertiesId { get; set; }
    }
}
