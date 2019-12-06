using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Jawwi.web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Jawwi.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Weather.Api _api;

        public HomeController(ILogger<HomeController> logger, Weather.Api weatherApi)
        {
            _logger = logger;
            _api = weatherApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddLocation()
        {
            return View(new LocationViewModel());
        }

        public async Task<IActionResult> Locations()
        {
            var locations = new List<LocationViewModel>();
            if (Request.Cookies.ContainsKey("locations"))
            {
                locations = JsonConvert.DeserializeObject<List<LocationViewModel>>(Request.Cookies["locations"]);
            }
            var model = new List<LocationsViewModel>();
            foreach (var location in locations)
            {
                var result = (await _api.Search(location.City)).FirstOrDefault();
                if (result != null)
                {
                    var weather = await _api.Forcast5Days(result.Key);
                    if (weather != null)
                    {
                        model.Add(new LocationsViewModel()
                        {
                            Location = location,
                            Dailyforecasts = weather
                        });
                    }
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddLocation(LocationViewModel model)
        {
            var options = new CookieOptions();
            options.Expires = DateTime.Now.AddMonths(1);

            var locations = new List<LocationViewModel>();
            if (Request.Cookies.ContainsKey("locations"))
            {
                locations = JsonConvert.DeserializeObject<List<LocationViewModel>>(Request.Cookies["locations"]);
            }
            locations.Add(model);
            Response.Cookies.Append("locations", JsonConvert.SerializeObject(locations), options);
            ViewBag.Message = "added";
            return RedirectToAction("locations");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
