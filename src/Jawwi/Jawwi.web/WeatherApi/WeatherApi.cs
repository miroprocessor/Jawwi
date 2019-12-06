using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Jawwi.web.WeatherApi
{
    public class WeatherApi
    {
        private readonly HttpContext _context;

        public WeatherApi(HttpContext context)
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
        public async Task<string> ForCast5Days(string locationCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"forecasts/v1/daily/5day/{locationCode}?apikey={apikey}");

            //return JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
            return (await result.Content.ReadAsStringAsync());
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


            var ccresult = await client.GetAsync($"currentconditions/v1/{location.Key}?apikey={apikey}&lang=en-us&details=false");
            var ccstringResult = await ccresult.Content.ReadAsStringAsync();
            var ccjson = (dynamic)JsonConvert.DeserializeObject(ccstringResult);

            location.WeatherText = ccjson[0].WeatherText;
            location.IsDayTime = ccjson[0].IsDayTime;
            location.Temperature = ccjson[0].Temperature.Metric.Value;

            return location;
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

    public class Location
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Key { get; set; }
        public string WeatherText { get; set; }
        public bool IsDayTime { get; set; }
        public decimal Temperature { get; set; }
    }
}
