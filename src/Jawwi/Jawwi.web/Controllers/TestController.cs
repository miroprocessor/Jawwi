using System.Threading.Tasks;
using Jawwi.web.WeatherApi;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Jawwi.web.Controllers
{
    public class TestController : Controller
    {
        private readonly IHttpContextAccessor _accessor;

        public TestController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public async Task<Location> Index()
        {

            var weatherApi = new WeatherApi.WeatherApi(_accessor.HttpContext);
            //return await weatherApi.GetCountries("MEA");
            
            return await weatherApi.GetCurrentLocationDetails();
        }
    }
}