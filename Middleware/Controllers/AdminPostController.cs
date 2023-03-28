using Interfaces.TaskerInterfaces;
using Mappers.TaskMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class AdminPostController : Controller
    {
        private readonly ITaskPostRepository taskService;
        private readonly ITaskStatusRepository statusService;
        private readonly ILogger<AdminPostController> logger;

        public AdminPostController(ITaskPostRepository taskService, 
            ITaskStatusRepository statusService, ILogger<AdminPostController> logger)
        {
            this.taskService = taskService;
            this.statusService = statusService;
            this.logger = logger;
        }

        public async Task<IActionResult> Manage()
        {
            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");

            try
            {
                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    logger.LogInformation($"Manage of category is called");
                    var models = taskService.GetAllForAdmin().Result.Select(x => x.ToModel()).ToList();
                    return View(models);
                }
                else
                {
                    TempData["message"] = $"Admin not found";
                    return RedirectToAction("login-admin", "Admin");
                }
            }
            catch(Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                logger.LogError($"Error: {ex.Message}");
                return View(Enumerable.Empty<TasksModel>());
            }
            #endregion session
        }

        public async Task<IActionResult> TaskDetails(int id)
        {
            try
            {
                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");

                var model = taskService.Get(id).Result.ToModel();

                if (model == null)
                {
                    TempData["message"] = "No task details found";
                    return RedirectToAction(nameof(Manage));
                }
                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }


                logger.LogInformation($" Get Tasks called. Task title = {model.TaskTitle}");
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }

        public async Task<IActionResult> ApprovelTask(int id, string filter)
        {
            try
            {
                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");

                var model = taskService.Get(id).Result.ToModel();

                if (model == null)
                {
                    TempData["message"] = "No task details found";
                    return RedirectToAction(nameof(Manage));
                }
                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    logger.LogInformation($"Get Tasks called. Task title = {model.TaskTitle}");
                    if (filter == "a") 
                    {
                        model.TaskStatusId = statusService.Get(WebUtils.POST_APPROVED_STATUS).Result.TaskStatusId;
                    }
                    else if (filter == "r") 
                    {
                        model.TaskStatusId = statusService.Get(WebUtils.POST_REJECT_STATUS).Result.TaskStatusId;
                    }
                    else
                    {
                        TempData["message"] = "Wrong filter input";
                        return RedirectToAction(nameof(Manage));
                    }
                    var res = await taskService.Update(model.ToDb());
                    return RedirectToAction(nameof(Manage));
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
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