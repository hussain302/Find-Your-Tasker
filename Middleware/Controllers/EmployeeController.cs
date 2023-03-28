using Interfaces.UserInterfaces;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Mvc;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository service;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeRepository service, ILogger<EmployeeController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
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
                    logger.LogInformation($"Manage of Employee is called");
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
                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    ViewData["Title"] = "Edit Employee";
                    return View(service.Get(Convert.ToInt32(id)).Result.ToModel());
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }

                #endregion session
                
            }
            else
            {
                //Create new record
                ViewData["Title"] = "Create Employee";
                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    ViewData["Title"] = "Edit Employee";
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
        public async Task<IActionResult> CreateOrEdit(EmployeeModel model)
        {
            try
            {

                if (model.Id > 0)
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
            catch (Exception ex)
            {
                TempData["message"] = $"Error: {ex.Message}";
                return View();
            }
        }


        [HttpGet]
        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            //Edit Record
            ViewData["Title"] = "Delete Employee";
            
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
        public async Task<IActionResult> Delete(EmployeeModel model)
        {
            var response = await service.Remove(model.Id);
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

            ViewData["Title"] = "Employee Details";
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
