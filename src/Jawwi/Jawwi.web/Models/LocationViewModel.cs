using Jawwi.web.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jawwi.web.Models
{
    //public class LocationViewModel
    //{
    //    public string CountryName { get; set; }

    //    public string CountryCode { get; set; }

    //    public string City { get; set; }
    //    public string City { get; set; }
    //}
    public class LocationsViewModel
    {
        public Country Location{ get; set; }
        public CurrentCodition Condition { get; set; }
    }
}
