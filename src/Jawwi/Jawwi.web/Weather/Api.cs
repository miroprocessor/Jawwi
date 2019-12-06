using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Jawwi.web.Weather
{
    public class Api
    {
        private readonly HttpContext _context;

        public Api(HttpContext context)
        {
            _context = context;
        }

        public readonly string BaseUrl = "http://dataservice.accuweather.com/";
        public readonly string apikey = "Bl3XjUzA1w7VraOff37cEfixuOoRPQnZ";//"C4rQfwrxnBypH9NSFUMpdfyg9z28sFNV";

        public async Task<string> GetCountries(string regionCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"locations/v1/countries/{regionCode}?apikey={apikey}");

            //return JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
            return (await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// get admin areas
        /// </summary>
        /// <param name="countryCode">KW for kuwait</param>
        /// <returns></returns>
        public async Task<string> GetAreas(string countryCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"locations/v1/adminareas/{countryCode}?apikey={apikey}");

            //return JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
            return (await result.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// get forcasting
        /// </summary>
        /// <param name="Come">3-222056_1_AL</param>
        /// <returns></returns>
        public async Task<List<Dailyforecast>> Forcast5Days(string locationCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"forecasts/v1/daily/5day/{locationCode}?apikey={apikey}");

            var json = JsonConvert.DeserializeObject<dynamic>(await result.Content.ReadAsStringAsync());

            var days = new List<Dailyforecast>();
            foreach (var dayna in json.Dailyforecast)
            {
                days.Add(new Dailyforecast()
                {
                    Date = dayna.Date,
                    MinTemperature = dayna.Temperature.Minimum.Value,
                    MaxTemperature = dayna.Temperature.Maximum.Value,
                    Day = dayna.Day,
                    Night = dayna.Night
                });

            }
            return days;
        }

        /// <summary>
        /// get location
        /// </summary>
        /// <returns></returns>
        public async Task<List<Location>> Search(string query)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"locations/v1/cities/search/?apikey={apikey}&q={query}&offset=25");

            return JsonConvert.DeserializeObject<List<Location>>(await result.Content.ReadAsStringAsync());
            //return (await result.Content.ReadAsStringAsync());
        }


        public async Task<Location> GetCurrentLocationDetails()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            var ip = "37.37.166.79";
            //var ip = _context.Connection.RemoteIpAddress.ToString();

            var result = await client.GetAsync($"locations/v1/cities/ipaddress?apikey={apikey}&q={ip}&lang=en-us&details=false");
            var stringResult = await result.Content.ReadAsStringAsync();
            var json= (dynamic)JsonConvert.DeserializeObject(stringResult);

            var location = new Location()
            {
                Name = json.Country.EnglishName,
                Area = json.AdministrativeArea.EnglishName,
                Key = json.Key
            };


            var ccresult = await client.GetAsync($"currentconditions/v1/{location.Key}?apikey={apikey}&lang=en-us&details=true");
            var ccstringResult = await ccresult.Content.ReadAsStringAsync();
            var ccjson = (dynamic)JsonConvert.DeserializeObject(ccstringResult);

            location.WeatherText = ccjson[0].WeatherText;
            location.IsDayTime = ccjson[0].IsDayTime;
            location.Temperature = ccjson[0].Temperature.Metric.Value;
            location.RelativeHumidity = ccjson[0].RelativeHumidity;
            location.WeatherIcon = ccjson[0].WeatherIcon;
            location.Wind = ccjson[0].Wind.Speed.Metric.Value + " " + ccjson[0].Wind.Speed.Metric.Unit;

            return location;
        }

        public async Task<CurrentCodition> GetLocationDetails(string locationKey)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            var ip = "37.37.166.79";
            //var ip = _context.Connection.RemoteIpAddress.ToString();

            var result = await client.GetAsync($"currentconditions/v1/{locationKey}?apikey={apikey}");
            var stringResult = await result.Content.ReadAsStringAsync();
            var json = (dynamic)JsonConvert.DeserializeObject(stringResult);

            var currentCodition = new CurrentCodition()
            {
                WeatherText = json.WeatherText,
                WeatherIcon = json.WeatherIcon,
                HasPrecipitation = json.HasPrecipitation,
                IsDayTime = json.IsDayTime,
                Temperature = json.Temperature.Metric.Value
            };
            return currentCodition;
        }
    }


    //[
    //{
    //    "LocalObservationDateTime": "2019-12-06T22:50:00+03:00",
    //    "EpochTime": 1575661800,
    //    "WeatherText": "Rain",
    //    "WeatherIcon": 18,
    //    "HasPrecipitation": true,
    //    "PrecipitationType": "Rain",
    //    "IsDayTime": false,
    //    "Temperature": {
    //        "Metric": {
    //            "Value": 22.2,
    //            "Unit": "C",
    //            "UnitType": 17
    //        },
    //        "Imperial": {
    //            "Value": 72,
    //            "Unit": "F",
    //            "UnitType": 18
    //        }
    //    },
    //    "MobileLink": "http://m.accuweather.com/en/kw/kuwait/222056/current-weather/222056?lang=en-us",
    //    "Link": "http://www.accuweather.com/en/kw/kuwait/222056/current-weather/222056?lang=en-us"
    //}
    //]

}
