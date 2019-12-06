
using System;

public class Rootobject
{
    public DateTime LocalObservationDateTime { get; set; }
    public int EpochTime { get; set; }
    public string WeatherText { get; set; }
    public int WeatherIcon { get; set; }
    public bool HasPrecipitation { get; set; }
    public string PrecipitationType { get; set; }
    public bool IsDayTime { get; set; }
    public Temperature Temperature { get; set; }
    public string MobileLink { get; set; }
    public string Link { get; set; }
}

public class Temperature
{
    public Metric Metric { get; set; }
    public Imperial Imperial { get; set; }
}
