using Microsoft.AspNetCore.Mvc;

namespace Azure.NETCoreAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult SampleContent()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
