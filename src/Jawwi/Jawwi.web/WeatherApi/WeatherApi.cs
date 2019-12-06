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
        public readonly string apikey = "C4rQfwrxnBypH9NSFUMpdfyg9z28sFNV";

        public async Task<string> GetCountries(string regionCode)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri(BaseUrl);
            
            var result = await client.GetAsync($"locations/v1/countries/{regionCode}?apikey={apikey}");
            
            //return JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
            return (await result.Content.ReadAsStringAsync());
        }
    }
}
