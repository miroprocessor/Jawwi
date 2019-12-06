namespace Jawwi.web.Weather
{
    //public class Location
    //{
    //    public int Version { get; set; }
    //    public string Key { get; set; }
    //    public string Type { get; set; }
    //    public int Rank { get; set; }
    //    public string LocalizedName { get; set; }
    //    public string EnglishName { get; set; }
    //    public string PrimaryPostalCode { get; set; }
    //    public Region Region { get; set; }
    //    public Country Country { get; set; }
    //    public Administrativearea AdministrativeArea { get; set; }
    //    public Timezone TimeZone { get; set; }
    //    public Geoposition GeoPosition { get; set; }
    //    public bool IsAlias { get; set; }
    //    public Parentcity ParentCity { get; set; }
    //    public object[] SupplementalAdminAreas { get; set; }
    //    public string[] DataSets { get; set; }
    //}
    public class CurrentCodition 
    {
        public string WeatherText { get; set; }

        public int WeatherIcon { get; set;}

        public bool HasPrecipitation { get; set; }
        public bool IsDayTime { get; set; }

        public decimal Temperature { get; set; }


    }
    public class Location
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Key { get; set; }
        public string WeatherText { get; set; }
        public bool IsDayTime { get; set; }
        public decimal Temperature { get; set; }

        public string RelativeHumidity { get; set; }
        public string WeatherIcon { get; set; }
        public string Wind { get; set; }
    }
    public class Region
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Country
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }

    public class Administrativearea
    {
        public string ID { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
        public int Level { get; set; }
        public string LocalizedType { get; set; }
        public string EnglishType { get; set; }
        public string CountryID { get; set; }
    }

    public class Timezone
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int GmtOffset { get; set; }
        public bool IsDaylightSaving { get; set; }
        public object NextOffsetChange { get; set; }
    }

    public class Geoposition
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public Elevation Elevation { get; set; }
    }

    public class Elevation
    {
        public Metric Metric { get; set; }
        public Imperial Imperial { get; set; }
    }

    public class Metric
    {
        public int Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Imperial
    {
        public int Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }

    public class Parentcity
    {
        public string Key { get; set; }
        public string LocalizedName { get; set; }
        public string EnglishName { get; set; }
    }
}