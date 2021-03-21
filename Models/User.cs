using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class User :IdentityUser
    {
        [Display(Name = "Last Name")]
        public string LastName { get; set; }



        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }



        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Display(Name = "About Agent"), MinLength(15)]
        public string About { get; set; }


        public string Address { get; set; }


        public string Skills { get; set; }

        public string Quote { get; set; }


        [Display(Name = "Phone Number")]
        public string MobileNumber { get; set; }



        public string Skype { get; set; }



        [Display(Name = "Language spoken")]
        public string Language { get; set; }



        [Url, Display(Name = "Facebook Link")]
        public string FacebookLink { get; set; }



        [Url, Display(Name = "Twitter Link")]
        public string TwitterLink { get; set; }



        [Url, Display(Name = "Instagram Link")]
        public string InstagramLink { get; set; }



        [Url, Display(Name = "LinkedIn Link")]
        public string LinkedInLink { get; set; }

        [Display(Name = "Image Name")]
        public string ImageName { get; set; }

        [NotMapped, Display(Name = "Upload File")]
        public IFormFile UploadedFile { get; set; }
    }
}
