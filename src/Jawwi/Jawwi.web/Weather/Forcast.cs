
using System;

public class HourlyForecast
{
    public DateTime Date { get; set; }
    public decimal MinTemperature { get; set; }
    public decimal MaxTemperature { get; set; }
    public Day Day { get; set; }
    public Night Night { get; set; }
}

public class Dailyforecast
{
    public DateTime Date { get; set; }
    public decimal MinTemperature { get; set; }
    public decimal MaxTemperature { get; set; }
    public Day Day { get; set; }
    public Night Night { get; set; }
}

public class Day
{
    public int Icon { get; set; }
    public string IconPhrase { get; set; }
    public bool HasPrecipitation { get; set; }
    public string PrecipitationType { get; set; }
    public string PrecipitationIntensity { get; set; }
}

public class Night
{
    public int Icon { get; set; }
    public string IconPhrase { get; set; }
    public bool HasPrecipitation { get; set; }
    public string PrecipitationType { get; set; }
    public string PrecipitationIntensity { get; set; }
}
