using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyRent.Models
{
    public class GeneralQuery
    {

        public string Keyword { get; set; }
        public string Country { get; set; }
        public int? PropertyType { get; set; }

        public int? PropertyStatus { get; set; }

        public int? NumberofBedrooms { get; set; }

        public int? NumberOfBathrooms { get; set; }

        public double MinPrice { get; set; }

        public double MaxPrice { get; set; }
    }
}
