using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class UserFaqViewModel : UserGeneralViewModel
    {
        public IEnumerable<Faq> Faqs { get; set; }
    }
}
