using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace Azure.NETCoreAPI.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IConfiguration _configuration;

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Read all key/value pairs from configuration and pass to the view
            var items = _configuration.AsEnumerable()
                .Where(k => k.Value != null)
                .OrderBy(k => k.Key)
                .ToList();

            return View(items);
        }
    }
}
