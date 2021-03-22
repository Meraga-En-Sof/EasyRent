using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public string Subject { get; set; }

        [Display(Name = "Attachment Name")]
        public string AttachmentName { get; set; }

        [Display(Name = "Date Sent")]
        public DateTime DateSent { get; set; }

        [Display(Name = "Is Important")]
        public bool IsImportant { get; set; }

        //Relationship

        public User Sender { get; set; }

        [Display(Name = "Sender")]
        public string SenderId { get; set; }

        public User Reciever { get; set; }

        [Display(Name = "Reciever")]
        public string RecieverId { get; set; }
    }
}
