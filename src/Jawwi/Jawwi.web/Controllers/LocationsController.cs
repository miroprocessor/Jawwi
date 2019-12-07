using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jawwi.web.Models;
using Jawwi.web.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jawwi.web.Controllers
{
    public class LocationsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Weather.Api _api;

        public LocationsController(ILogger<HomeController> logger, Weather.Api weatherApi, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
            _api = weatherApi;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var locations = new List<Country>();
            if (Request.Cookies.ContainsKey("locations"))
            {
                locations = JsonConvert.DeserializeObject<List<Country>>(Request.Cookies["locations"]);
            }
            if (locations.Count > 0)
            {
                var model = new List<LocationsViewModel>();
                foreach (var location in locations)
                {
                    var item = new LocationsViewModel()
                    {
                        Location = location
                    };

                    var weather = await _api.GetCurrentCondition(location.Key);
                    if (weather != null)
                    {
                        item.Condition = weather;
                    }
                    model.Add(item);
                }
                return View(model);
            }
            else
            {
                return View(new List<LocationsViewModel>());
            }
        }

        public async Task<JsonResult> Search(string query)
        {
            var countries = await _api.GetCountries(query);
            return Json(countries);
        }

        public IActionResult Add(string city, string code, string name, string key)
        {
            var options = new CookieOptions();
            options.Expires = DateTime.Now.AddMonths(1);

            var locations = new List<Country>();
            if (Request.Cookies.ContainsKey("locations"))
            {
                locations = JsonConvert.DeserializeObject<List<Country>>(Request.Cookies["locations"]);
            }
            locations.Add(new Country()
            {
                City = city,
                Code = code,
                Name = name,
                Key = key
            });
            Response.Cookies.Append("locations", JsonConvert.SerializeObject(locations), options);
            ViewBag.Message = "added";
            return RedirectToAction("index");
        }
    }
}