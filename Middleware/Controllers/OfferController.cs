using Interfaces.TaskerInterfaces;
using Interfaces.UserInterfaces;
using Mappers.TaskMappers;
using Microsoft.AspNetCore.Mvc;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class OfferController : Controller
    {
        private readonly ITaskPostRepository taskPostService;
        private readonly ILogger<TaskPostController> logger;
        private readonly IUserRepository userService;
        private readonly ITaskStatusRepository taskStatusService;
        private readonly ISubCategoryRepository subCatService;

        public OfferController(ITaskPostRepository TaskPostService,
            ILogger<TaskPostController> logger, IUserRepository userService,
            ITaskStatusRepository taskStatusService,
            ISubCategoryRepository subCatService)
        {
            taskPostService = TaskPostService;
            this.logger = logger;
            this.userService = userService;
            this.taskStatusService = taskStatusService;
            this.subCatService = subCatService;
        }

        [HttpGet]
        public IActionResult AllOffers(int id)
        {
            logger.LogInformation($" Get Tasks called");
            try
            {
                var models = taskPostService.GetOffers(id).Result.Select(x=>x.ToModel()).ToList();
                return View(models);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }

        public IActionResult TaskDetails(int id)
        {
            logger.LogInformation($" Get Tasks called");
            try
            {
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");

                var model = taskPostService.Get(id).Result.ToModel();

                if (model == null)
                {
                    TempData["message"] = "No task details found";
                    return RedirectToAction("GetAll", "TaskPost");
                }
                if (ViewBag.Role == WebUtils.TASKER_ROLE || ViewBag.Role == WebUtils.POSTER_ROLE)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }
        [HttpGet]
        public IActionResult MakeOffer(int taskid)
        {
            try
            {
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                
                if (ViewBag.Role == WebUtils.TASKER_ROLE || ViewBag.Role == WebUtils.POSTER_ROLE)
                {
                     TempData["taskId"] = taskid;
                     return View();
                }
                else
                {
                    return RedirectToAction("Login", "User");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> MakeOffer(OfferModel model)
        {
            try
            {
                string username = HttpContext.Session.GetString("Name");
                var user = await userService.GetUser(username);
                model.UserId = user.UserId;

                bool response = await taskPostService.AddOffer(model.ToDb());

                if (response)
                {
                    TempData["message"] = $"Posted your offer successfully";
                    return RedirectToAction("GetAll", "TaskPost");
                }
                else
                {
                    TempData["message"] = $"Error while adding your post";
                    return RedirectToAction("GetAll", "TaskPost");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }
    }
}