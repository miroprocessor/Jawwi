using Jawwi.web.Weather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jawwi.web.ViewModel
{
    public class IndexVM
    {
        public Location Location { get; set; }
        public List<Dailyforecast> Dailyforecast { get; set; }
    }
}
