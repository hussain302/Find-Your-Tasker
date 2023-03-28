using Interfaces.TaskerInterfaces;
using Mappers.TaskMappers;
using Microsoft.AspNetCore.Mvc;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class PosterController : Controller
    {
        private readonly ITaskPostRepository taskService;
        private readonly ILogger<PosterController> logger;

        public PosterController(ITaskPostRepository taskService,
            ILogger<PosterController> logger)
        {
            this.taskService = taskService;
            this.logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            #region session 

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");
            //if (ViewBag.Role == WebUtils.TASKER_ROLE)
            //{
            //    //TempData["Message"] = "Login as Poster";
            //    return RedirectToAction("login", "User");
            //}
            #endregion session

            ViewData["PageName"] = "Poster Dashboard";
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> FindRecommendedTasker(Skills skill)
        {
            return RedirectToAction("Index", "Recommended", skill);
        }

        public async Task<IActionResult> AllActivePosts()
        {
            #region session 

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");

            #endregion session


            string name = ViewBag.Name;

            var taskList = taskService.GetAll(name).Result.Select(x => x.ToModel()).ToList();
            logger.LogInformation($" All Posts find count: {taskList.Count}");

            if (taskList.Count == 0)
            {
                TempData["message"] = "No Post added yet";
                return View();
            }

            return View(taskList);

        }
    }
}