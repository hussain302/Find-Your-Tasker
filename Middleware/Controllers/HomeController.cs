using Interfaces.UserInterfaces;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Mvc;
using Models.WebModels;
using System.Diagnostics;

namespace Middleware.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository user;

        public HomeController(ILogger<HomeController> logger, IUserRepository user)
        {
            _logger = logger;
            this.user = user;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //ViewData["PageName"] = "Homepage";
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            return View();
        }

        [HttpGet]
        public IActionResult About()
        {
            //ViewData["PageName"] = "About";
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            return View();
        }

        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public IActionResult Contact()
        {
            //ViewData["PageName"] = "Contact";
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            return View();
        }


        [HttpPost]
        public IActionResult Query(ContactModel model)
        {
            try
            {
                user.AddQuery(model.ToDb());
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Contact");
            }
        }
    }
}