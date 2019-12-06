using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Jawwi.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Jawwi.web.Weather
{
    public class Api
    {
        private readonly HttpContext _context;

        public Api(IHttpContextAccessor accessor)
        {
            _context = accessor.HttpContext;
        }

        public readonly string BaseUrl = "http://dataservice.accuweather.com/";
        public readonly string apikey = "ju8lA0Heax0Q1lAol0AoVo97x8Aw2Pcb";//"C4rQfwrxnBypH9NSFUMpdfyg9z28sFNV";

        public async Task<IEnumerable<Country>> GetCountries(string regionCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"locations/v1/countries/{regionCode}?apikey={apikey}");
            var countries = new List<Country>();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = (dynamic)JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());

                foreach (var item in json)
                {
                    countries.Add(new Country()
                    {
                        Code = item.ID,
                        Name = item.EnglishName
                    });
                }
            }
            return countries;
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

            var result = await client.GetAsync($"forecasts/v1/daily/5day/{locationCode}?apikey={apikey}&metric=true");

            var json = JsonConvert.DeserializeObject<ForcastDays>(await result.Content.ReadAsStringAsync());
            return json.DailyForecasts.ToList();
        }

        /// <summary>
        /// get location
        /// </summary>
        /// <returns></returns>
        public async Task<List<LocationResult>> Search(string query)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"locations/v1/cities/search?apikey={apikey}&q={query}&offset=25");
            var locations = new List<LocationResult>();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = (dynamic)JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());

                foreach (var item in json)
                {
                    locations.Add(new LocationResult()
                    {
                        Key = item.Key
                    });
                }
            }
            return locations;
        }


        public async Task<Location> GetCurrentLocationDetails()
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            var ip = "37.37.166.79";
            //var ip = _context.Connection.RemoteIpAddress.ToString();

            var result = await client.GetAsync($"locations/v1/cities/ipaddress?apikey={apikey}&q={ip}&lang=en-us&details=false");
            var stringResult = await result.Content.ReadAsStringAsync();
            var json = (dynamic)JsonConvert.DeserializeObject(stringResult);

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

        public async Task<CurrentCodition> GetCurrentCondition(string locationKey)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            var result = await client.GetAsync($"currentconditions/v1/{locationKey}?apikey={apikey}");
            var currentCodition = new CurrentCodition();
            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var stringResult = await result.Content.ReadAsStringAsync();
                var json = (dynamic)JsonConvert.DeserializeObject(stringResult);

                foreach (var item in json)
                {
                    currentCodition = new CurrentCodition()
                    {
                        WeatherText = item.WeatherText,
                        WeatherIcon = item.WeatherIcon,
                        HasPrecipitation = item.HasPrecipitation,
                        IsDayTime = item.IsDayTime,
                        Temperature = item.Temperature.Metric.Value
                    };
                    break;
                }

            }

            return currentCodition;
        }

        public async Task<List<HourlyForecast>> GetHourlyForecast(string locationCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"forecasts/v1/hourly/12hour/{locationCode}?apikey={apikey}");

            var json = (dynamic)JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());

            var hours = new List<HourlyForecast>();
            foreach (var hourly in json)
            {
                hours.Add(new HourlyForecast()
                {
                    Date = hourly.DateTime,
                    MinTemperature = hourly.Temperature.Value,
                    WeatherIcon = hourly.WeatherIcon
                });

            }
            return hours;


        }
    }
}
