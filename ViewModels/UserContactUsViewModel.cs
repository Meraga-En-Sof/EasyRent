using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class UserContactUsViewModel : UserGeneralViewModel
    {
        public ContactInformation   ContactInformation { get; set; }

        public ContactUs ContactUsForm { get; set; }
    }
}
