using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Jawwi.web.Controllers
{
    public class TestController : Controller
    {
        public async Task<string> Index()
        {

            var weatherApi = new Weather.Api();
            return await weatherApi.GetCountries("MEA");
        }
    }
}