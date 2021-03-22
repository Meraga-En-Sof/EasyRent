using EasyRent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.ViewModels
{
    public class DashboardViewModel
    {

        public double PropertyNumber { get; set; }

        public double MemberNumber { get; set; }

        public double PurchaseNumber { get; set; }

        public double RentNumber { get; set; }

        public double MessageNumbers { get; set; }

        public double AveragePrice { get; set; }
        public double LowestPrice { get; set; }

        public double HigestPrice { get; set; }

        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Property> Properties { get; set; }


    }
}
