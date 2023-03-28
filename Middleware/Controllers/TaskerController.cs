using Microsoft.AspNetCore.Mvc;

namespace Middleware.Controllers
{
    public class TaskerController : Controller
    {
        public IActionResult Index()
        {
            ViewData["PageName"] = "Dashboard";
            
            #region session 

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");

            #endregion session


            return View();
        }




    }
}
