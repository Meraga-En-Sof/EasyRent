using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class Property
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Google Map Address")]
        public string GoogleMapAddress { get; set; }

        [Display(Name = "Main Image Name")]
        public string ImageName { get; set; }

        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required, Display(Name = "Area in M²")]
        public double Area { get; set; }
        [Display(Name = "Bed Room")]
        public int BedRooms { get; set; }

        [Display(Name = "Bath Room")]
        public int bathrooms { get; set; }

        public int Garage { get; set; }

        public int Stairs { get; set; }

        public string BuildingConditon { get; set; }

        [Display(Name="Floor Plan Link")]
        public string FloorPlanImage { get; set; }


        public bool isDealClosed { get; set; }

        public bool isDisplayed { get; set; }

        [NotMapped, Display(Name = "Upload File")]
        public IFormFile UploadedFile { get; set; }


        public string Country { get; set; }







        //Relationship


        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }



        public PropertyType PropertyType { get; set; }

        [Display(Name = "Property Type")]
        public int PropertyTypeId { get; set; }


        public PropertyMode PropertyMode { get; set; }

        [Display(Name = "Property Mode")]
        public int PropertyModeId { get; set; }



        public User User { get; set; }

        [Display(Name="Agent")]
        public string UserId { get; set; }

        [Display(Name = "Video Link")]
        public string VideoLink { get; set; }


        public User ClosedTo { get; set; }

        [Display(Name = "Tenant Email")]
        public string ClosedToId { get; set; }


    }
}
