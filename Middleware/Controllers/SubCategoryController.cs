using Interfaces.TaskerInterfaces;
using Mappers.TaskMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryRepository service;
        private readonly ILogger<SubCategoryController> logger;
        private readonly ICategoryRepository categoryService;

        public SubCategoryController(ISubCategoryRepository service, ILogger<SubCategoryController> logger, ICategoryRepository categoryService)
        {
            this.service = service;
            this.logger = logger;
            this.categoryService = categoryService;
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
                    logger.LogInformation($"Manage of SubCategory is called");
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

            try
            {
                ViewBag.category = categoryService.GetAll().Result.Select(xx => xx.ToModel()).ToList();

                if (id > 0)
                {
                    //Edit Record
                    ViewData["Title"] = "Edit SubCategory";
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
                else
                {
                    //Create new record
                    //Edit Record
                    ViewData["Title"] = "Create SubCategory";
                    #region Admin session 

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

                    #endregion session
                }
            }
            finally
            {
                //finally
            }

        }
        [HttpPost]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> CreateOrEdit(SubCategoryModel model)
        {
            try
            {


                if (model.SubCategoryId > 0)
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





        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    ViewData["Title"] = "Delete SubCategory";
                    ViewBag.category = categoryService.GetAll().Result.Select(xx => xx.ToModel()).ToList();
                    return View(service.Get(Convert.ToInt32(id)).Result.ToModel());
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }

                #endregion session
                //Edit Record
               
            }
            finally { }
        } 
    
        [HttpPost]
        [ResponseCache(Duration = 2000, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Delete(SubCategoryModel model)
        {
            var response = await service.Delete(model.SubCategoryId);
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
            try
            {

                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    ViewBag.category = categoryService.GetAll().Result.Select(xx => xx.ToModel()).ToList();
                    ViewData["Title"] = "SubCategory Details";
                    return View(service.Get(Convert.ToInt32(id)).Result.ToModel());
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }

                #endregion session
            }
            finally { };
        }

    }
}
