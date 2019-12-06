using System.Threading.Tasks;
using Jawwi.web.Weather;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jawwi.web.Controllers
{
    public class TestController : Controller
    {
        private readonly Api _api;

        public TestController(Api api)
        {
            _api = api;
        }

        public async Task<Location> Index()
        {

            
            return await _api.GetCurrentLocationDetails();
        }
    }
}