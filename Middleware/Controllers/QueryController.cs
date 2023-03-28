using Interfaces.UserInterfaces;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;

namespace Middleware.Controllers
{
    public class QueryController : Controller
    {
        private readonly IUserRepository user;

        public QueryController(IUserRepository user)
        {
            this.user = user;
        }

        public IActionResult Manage()
        {
            try
            {
                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    var models = user.GetAllQueries().Result.Select(x => x.ToModel()).ToList();
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
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }
        
        
        public IActionResult Delete(int id)
        {
            try
            {
                #region Admin session 

                ViewBag.Name = HttpContext.Session.GetString("admin name");
                ViewBag.Role = HttpContext.Session.GetString("admin role");


                if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
                {
                    user.RemoveQuery(new Models.DbModels.Contact { Id = id});
                    return RedirectToAction("Manage");
                }
                else
                {
                    return RedirectToAction("login-admin", "Admin");
                }

                #endregion session

            }
            catch (Exception ex)
            {
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }
    }
}
