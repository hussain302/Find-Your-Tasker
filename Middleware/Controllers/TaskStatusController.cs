using Interfaces.TaskerInterfaces;
using Mappers.TaskMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class TaskStatusController : Controller
    {
        private readonly ITaskStatusRepository service;
        private readonly ILogger<TaskStatusController> logger;

        public TaskStatusController(ITaskStatusRepository service, ILogger<TaskStatusController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Manage()
        {
            try
            {
                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    logger.LogInformation($"Manage of Task status is called");
                    var models = service.GetAll().Result.Select(x => x.ToModel()).ToList();
                    return View(models);
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }

                #endregion session
                
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }






        [HttpGet]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> CreateOrEdit(int? id)
        {
            
            if (id > 0)
            {
                
                //Edit Record
                ViewData["Title"] = "Edit Task Status";
                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    return View(service.Get(Convert.ToInt32(id)).Result.ToModel());
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }
                
            }
            else
            {
                //Create new record
                ViewData["Title"] = "Create Task Status";
                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }
            }
        }
        [HttpPost]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> CreateOrEdit(TaskStatusModel model)
        {
            try
            {


                if (model.TaskStatusId > 0)
                {
                    //Edit Record
                    var response = await service.Update(model.ToDb());
                    if (response)
                    {
                        return RedirectToAction(nameof(Manage));
                    }
                    else
                    {
                        TempData["message"] = $"Record didn't Updated!";
                        return View();
                    }
                }
                else
                {
                    //Create new record
                    var response = await service.Add(model.ToDb());
                    if (response)
                    {
                        return RedirectToAction(nameof(Manage));
                    }
                    else
                    {
                        TempData["message"] = "Record didn't Created!";
                        return View();
                    }
                }
            }
            catch(Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View();
            }
        }
        




        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            //Delete Record
            ViewData["Title"] = "Delete Task Status";
            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");


            if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
            {
                return View(service.Get(Convert.ToInt32(id)).Result.ToModel());
            }
            else
            {
                return RedirectToAction("login-admin", "Admin");
            }

            #endregion session
        }

        [HttpPost]
        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(TaskStatusModel model)
        {
            var response = await service.Delete(model.TaskStatusId);
            if (response)
            {
                return RedirectToAction(nameof(Manage));
            }
            else
            {
                ViewBag.Message = "Record didn't Deleted!";
                return RedirectToAction(nameof(Manage));
            }
        }
        



        
        
        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Title"] = "Task Status Details";
            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");


            if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
            {
                return View(service.Get(Convert.ToInt32(id)).Result.ToModel());
            }
            else
            {
                return RedirectToAction("login-admin", "Admin");
            }

            #endregion session
        }
    }
}
