using Interfaces.UserInterfaces;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> logger;
        private readonly IRoleRepository service;

        public RoleController(ILogger<RoleController> logger, IRoleRepository service)
        {
            this.logger = logger;
            this.service = service;
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
                    logger.LogInformation($"Manage of Roles is called");
                    var models = service.GetRoles().Result.Select(x => x.ToModel()).ToList();
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
                ViewData["Title"] = "Edit Role";
                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    return View(service.GetRole(Convert.ToInt32(id)).Result.ToModel());
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
                ViewData["Title"] = "Edit Role";
                
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
        public async Task<IActionResult> CreateOrEdit(RoleModel model)
        {
            try
            {
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");

                if (model.RoleId > 0)
                {
                    model.UpdatedBy = Convert.ToString(ViewBag.Name);
                    model.UpdatedOn = Convert.ToDateTime(DateTime.Now.ToString("dddd, dd MMMM yyyy"));

                    //Edit Record
                    var response = await service.UpdateRole(model.ToDb());
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
                    model.CreatedBy = Convert.ToString(ViewBag.Name);
                    model.CreatedOn = Convert.ToDateTime(DateTime.Now.ToString("dddd, dd MMMM yyyy"));

                    //Create new record
                    var response = await service.AddRole(model.ToDb());
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





        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            //Edit Record
            ViewData["Title"] = "Delete Role";
            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");


            if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
            {
                return View(service.GetRole(Convert.ToInt32(id)).Result.ToModel());
            }
            else
            {
                return RedirectToAction("login-admin", "Admin");
            }

            #endregion session
            
        }

        [HttpPost]
        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(RoleModel model)
        {
            var response = await service.RemoveRole(model.RoleId);
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
            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");


            if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
            {
                ViewData["Title"] = "Role Details";
                return View(service.GetRole(Convert.ToInt32(id)).Result.ToModel());
            }
            else
            {
                return RedirectToAction("login-admin", "Admin");
            }

            #endregion session
            
        }


    }
}
