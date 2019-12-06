using System;
using System.Collections.Generic;
using System.Text;

namespace Jawwi.Models
{

    public class AutoCompleteResponse
    {
        public AutoComplete[] AutoCompletes { get; set; }
    }

    public class AutoComplete
    {
        public int Version { get; set; }
        public string Key { get; set; }
        public string Type { get; set; }
        public int Rank { get; set; }
        public string LocalizedName { get; set; }
        public Country Country { get; set; }
        public Administrativearea AdministrativeArea { get; set; }
    }

    public class Country
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
    }

    public class Administrativearea
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
    }

}
