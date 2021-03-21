using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class SocialMedia
    {
        [Key]
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Google Link")]
        public string GoogleAddress { get; set; }


        [Url, Display(Name = "Facebook Link")]
        public string FacebookLink { get; set; }



        [Url, Display(Name = "Twitter Link")]
        public string TwitterLink { get; set; }



        [Url, Display(Name = "Instagram Link")]
        public string InstagramLink { get; set; }



        [Url, Display(Name = "LinkedIn Link")]
        public string LinkedInLink { get; set; }


    }
}
