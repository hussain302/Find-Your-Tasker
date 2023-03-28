using Interfaces.UserInterfaces;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;
using Models.WebModels;

namespace Middleware.Controllers
{

    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository userService;
        private readonly IRoleRepository roleService;
        private readonly ILogger<AdminController> logger;

        public AdminController(IUserRepository userService,
            IRoleRepository roleService, ILogger<AdminController> logger)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.logger = logger;
        }


        [HttpGet]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin()
        {
            logger.LogInformation($"Register Get Method of admin is called");
            return View();
        }


        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin(UserModel model)
        {
            logger.LogInformation($"Register method of admin is called");
            return View();
        }

        [HttpGet]
        [Route("login-admin")]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.None, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> LoginAdmin()
        {
            logger.LogInformation($"Login Get method of admin is called");
            return View();
        }

        [HttpPost]
        [Route("login-admin")]
        public async Task<IActionResult> LoginAdmin(UserModel model)
        {
            try
            {

                var response = await userService.LoginUserRequest(username: model.UserName, password: model.Password);


                if (response != null && response.Role.RoleName == "admin" || response.Role.RoleName == "super admin")
                {
                    if (Convert.ToBoolean(response.IsApproved) == true)
                    {
                        HttpContext.Session.SetString("admin name", response.UserName);
                        HttpContext.Session.SetString("admin role", response.Role.RoleName);
                        HttpContext.Session.SetString("username admin", response.UserName);

                        logger.LogInformation($"{response.Email} Logged in successfully");
                        TempData["message"] = $"{response.UserName} Logged in successfully";

                        return RedirectToAction("admin-dashboard", "Admin");
                    }
                    else
                    {
                        logger.LogError($"{response.Email} is not approved User");
                        TempData["message"] = "User is not approved to login";
                        return View();
                    }
                }
                else if (response.Role.RoleName == "tasker" || response.Role.RoleName == "poster")
                {
                    logger.LogError($"{model.FullName} isn't authorized");
                    TempData["message"] = "User isn't authorized";
                    return View();
                }
                else
                {
                    logger.LogInformation($"{model.FullName} doesn't exixts");
                    TempData["message"] = "User doesn't exist";
                    return View();
                }
            }
            catch
            {

                logger.LogInformation($"No user found");
                TempData["message"] = "User doesn't exist";
                return View();
            }
        }

        [HttpGet]
        [Route("admin-dashboard")]
        public async Task<IActionResult> Dashboard()
        {


            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");
            
            logger.LogInformation($"Dashboard method of admin is called");
            ViewData["PageName"] = "Dashboard";

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

        [HttpGet]
        [Route("manage-users")]
        public async Task<IActionResult> ManageUsers()
        {
            logger.LogInformation($"Manege Users of admin is called");
            ViewData["PageName"] = "Manage All Dashboard";

            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");


            if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
            {
                var users = userService.GetUsers().Result.Select(x => x.ToModel()).ToList();
                return View(users);
            }
            else
            {
                return RedirectToAction("login-admin", "Admin");
            }
            #endregion session
        }

        [HttpGet]
        [Route("/admin-logout")]
        public async Task<IActionResult> AdminLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("login-admin");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.DeleteUser(id);
            return RedirectToAction("manage-users");
        }

        [HttpGet]
        [Route("manage-account-info")]
        public async Task<IActionResult> ManageAccountInfo()
        {
            logger.LogInformation($"Manege Users of admin is called");
            ViewData["PageName"] = "Manage All Dashboard";
            
            #region Admin session 

            ViewBag.Name = HttpContext.Session.GetString("admin name");
            ViewBag.Role = HttpContext.Session.GetString("admin role");
            ViewBag.Username = HttpContext.Session.GetString("username admin");

            string username = ViewBag.Username;

            if (ViewBag.Role == WebUtils.ADMIN_ROLE || ViewBag.Role == WebUtils.SUPER_ADMIN_ROLE)
            {
                var user = userService.GetUser(username).Result.ToModel();                
                return View(user);
            }
            else
            {
                return RedirectToAction("login-admin", "Admin");
            }

            #endregion session
        }

    }
}
