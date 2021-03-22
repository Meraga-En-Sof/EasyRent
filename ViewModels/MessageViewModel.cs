using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class MessageViewModel
    {
        public IEnumerable<Messages> AllMessages { get; set; }

        public IEnumerable<Messages> ActiveMessage { get; set; }

        [Required]
        public string Content { get; set; }

        public string Subject { get; set; }

        public string AttachmentName { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        public bool IsImportant { get; set; }

        //Relationship




        [Display(Name = "Reciever"), Required(ErrorMessage = "Reciever not Selected")]
        public string RecieverId { get; set; }

        public string ActiveUserId { get; set; }
    }
}
