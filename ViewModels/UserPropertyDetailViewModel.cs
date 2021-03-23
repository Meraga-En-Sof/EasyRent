using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class UserPropertyDetailViewModel : UserGeneralViewModel
    {
        public IEnumerable<Property> PopularProperties { get; set; }


        public Property Property { get; set; }

        public User User { get; set; }

        public IEnumerable<PropertyAmenities> PropertyAmenities { get; set; }

        public IEnumerable<Property> RecentProperties { get; set; }

        public IEnumerable<PropertySlider> PropertySliders { get; set; }


        public string MessageForm { get; set; }
    }
}
