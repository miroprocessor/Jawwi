using Jawwi.web.Weather;
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

        public string City { get; set; }
    }
    public class LocationsViewModel
    {
        public LocationViewModel Location{ get; set; }
        public CurrentCodition Condition { get; set; }
    }
}
