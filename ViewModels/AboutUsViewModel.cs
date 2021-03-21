using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class AboutUsViewModel :UserGeneralViewModel
    {
        public IEnumerable<User> Agents { get; set; }

        public AboutUs AboutUs { get; set; }

        public IEnumerable<Testimonials> Testimonials { get; set; }
    }
}
