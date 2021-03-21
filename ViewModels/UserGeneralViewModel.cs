using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class UserGeneralViewModel
    {
        public IEnumerable<Property> MenuProperties { get; set; }

        public SocialMedia SocialMedia { get; set; }
    }
}
