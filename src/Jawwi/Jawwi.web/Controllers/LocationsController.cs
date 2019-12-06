using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jawwi.web.Models;
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
            var locations = new List<LocationViewModel>();
            if (Request.Cookies.ContainsKey("locations"))
            {
                locations = JsonConvert.DeserializeObject<List<LocationViewModel>>(Request.Cookies["locations"]);
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
                    var result = (await _api.Search(location.CountryCode)).FirstOrDefault();
                    if (result != null)
                    {
                        var weather = await _api.GetCurrentCondition(result.Key);
                        if (weather != null)
                        {
                            item.Condition = weather;
                        }
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
    }
}