using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Jawwi.web.WeatherApi
{
    public class WeatherApi
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
        public async Task<string> ForCast5Days(string locationCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);

            var result = await client.GetAsync($"forecasts/v1/daily/5day/{locationCode}?apikey={apikey}");

            //return JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
            return (await result.Content.ReadAsStringAsync());
        }
    }
}
