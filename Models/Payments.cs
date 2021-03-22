using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class Payments
    {
        [Key]
        public int Id { get; set; }

        public string RecieptNumber { get; set; }

        public DateTime PaidOn { get; set; }

        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Proof of Payment")]
        public string ImageName { get; set; }




        [NotMapped, Display(Name = "Upload File")]
        public IFormFile UploadedFile { get; set; }
        //Relationships
        public Property Property { get; set; }
        public int PropertyId { get; set; }



        public User Tenant { get; set; }
        [Display(Name = "Tenant")]
        public string TenantId { get; set; }



        public User LandLord { get; set; }
        [Display(Name = "Land Lord")]
        public string LandLordId { get; set; }
    }
}
