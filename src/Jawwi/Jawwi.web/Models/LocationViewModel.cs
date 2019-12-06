using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jawwi.web.Models
{
    public class LocationViewModel
    {
        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public string RegionCode { get; set; }
    }
    public class LocationsViewModel
    {
        public string CountryName { get; set; }

        public string CountryCode { get; set; }

        public string RegionCode { get; set; }

        public string City { get; set; }

        public decimal Temp { get; set; }

        public decimal MinTemp { get; set; }

        public decimal MaxTemp { get; set; }

        public string State { get; set; }

        public string Humidity { get; set; }
    }
}
