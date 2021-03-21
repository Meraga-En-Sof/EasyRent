using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class UserHomeViewModel : UserGeneralViewModel
    {
        public IEnumerable<Property> SliderProperties { get; set; }

        public IEnumerable<OurService> OurServices { get; set; }

        public IEnumerable<Property> PopularProperties { get; set; }

        public IEnumerable<Property> RecentProperties { get; set; }
        public IEnumerable<Property> AllProperties { get; set; }

        public IEnumerable<Testimonials> Testimonials { get; set; }

        public IEnumerable<PropertyType> PropertyTypes { get; set; }
        public IEnumerable<PropertyMode> PropertyModes { get; set; }


        public string Keyword { get; set; }
        public string Country { get; set; }
        public int? PropertyType { get; set; }

        public int PropertyStatus { get; set; }

        public int NumberofBedrooms { get; set; }

        public int NumberOfBathrooms { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }




    }
}
