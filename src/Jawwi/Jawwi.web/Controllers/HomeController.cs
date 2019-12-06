﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Jawwi.web.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Jawwi.web.ViewModel;

namespace Jawwi.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Weather.Api _api;

        public HomeController(ILogger<HomeController> logger, Weather.Api weatherApi, IHttpContextAccessor accessor)
        {
            _logger = logger;
            _accessor = accessor;
            _api = weatherApi;
        }

        public async Task<IActionResult> Index()
        {
            //var weatherApi = new Weather.Api(_accessor);
            //return await weatherApi.GetCountries("MEA");
            var model = new IndexVM();
            model.Location =  await _api.GetCurrentLocationDetails();
            model.Dailyforecast = await _api.Forcast5Days(model.Location.Key);
            model.HourlyForecast = await _api.GetHourlyForecast(model.Location.Key);

            return View(model);
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
            if (locations.Count > 0)
            {
                var model = new List<LocationsViewModel>();
                foreach (var location in locations)
                {
                    var item = new LocationsViewModel()
                    {
                        Location = location
                    };
                    var result = (await _api.Search(location.City)).FirstOrDefault();
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
