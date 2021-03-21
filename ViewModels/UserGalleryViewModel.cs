using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class UserGalleryViewModel : UserGeneralViewModel
    {
        public IEnumerable<Property> Properties { get; set; }

        public IEnumerable<PropertyType> PropertyTypes { get; set; }
    }
}
