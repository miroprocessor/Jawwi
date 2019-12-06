using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jawwi.web.Weather
{
    public class Api
    {
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
        
    }
}
