﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jawwi.web.Controllers
{
    public class LocationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}