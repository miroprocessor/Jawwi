using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jawwi.web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly Weather.Api _api;

        public CountriesController(Weather.Api api)
        {
            _api = api;
        }

        public async Task<JsonResult> Index(string region)
        {
            var result = await _api.GetCountries(region);
            return Json(result);
        }
    }
}