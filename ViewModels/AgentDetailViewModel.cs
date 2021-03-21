using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class AgentDetailViewModel : UserGalleryViewModel
    {
        public IEnumerable<Property> Recent { get; set; }

        public User Agent { get; set; }

        public ContactAgent ContactAgent { get; set; }
    }
}
