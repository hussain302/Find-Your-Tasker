using Middleware.Common;
using Interfaces.UserInterfaces;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.WebModels;
using Newtonsoft.Json;

namespace Middleware.Controllers
{
    [Route("User/")]
    public class UserController : Controller
    {
        private readonly IUserRepository service;
        private readonly ILogger<UserController> logger;
        private readonly IRoleRepository roleService;

        public UserController(IUserRepository service, ILogger<UserController> logger, IRoleRepository roleService)
        {
            this.service = service;
            this.logger = logger;
            this.roleService = roleService;
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        public async Task<IActionResult> Login()
        {
            logger.LogInformation("Login Get method called");
            return View();
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserModel model)
        {
            try
            {
                var response = await service.LoginUserRequest(username: model.UserName, password: model.Password);
                if (response.Role.RoleName == WebUtils.ADMIN_ROLE || response.Role.RoleName == WebUtils.SUPER_ADMIN_ROLE)
                {
                    TempData["message"] = "Something went wrong. Contact US!";
                    logger.LogInformation($"Role is unauthorized Contact US!");
                    return View();
                }
                //if (response.Role.RoleName == "admin" || response.Role.RoleName == "super admin") RedirectToAction("login-admin", "Admin");

                if (response != null)
                {
                    if (Convert.ToBoolean(response.IsApproved) == true)
                    {
                        HttpContext.Session.SetString("username", response.UserName);
                        HttpContext.Session.SetString("Name", response.UserName);
                        HttpContext.Session.SetString("Role", response.Role.RoleName);

                        logger.LogInformation($"{response.Email} Logged in successfully");
                        TempData["message"] = $"{response.UserName} Logged in successfully";

                        if (response.IsTasker == true) return RedirectToAction("Index", "Tasker");
                        else return RedirectToAction("Index", "Poster");
                    }
                    else
                    {
                        logger.LogInformation($"{response.Email} is not approved User");
                        TempData["message"] = "User is not approved to login";
                        return View();
                    }
                }
                else
                {
                    logger.LogInformation($"{model.FullName} doesn't exixts");
                    TempData["message"] = "User doesn't exist";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "User doesn't exist";
                logger.LogError($"Error: {ex.Message}");
                return View();
            }
        }

        [HttpGet]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserModel model)
        {

            var verifyPasswords = WebUtils.PassAndConfirmPassMathing(model.Password, model.ConfirmPassword);

            if (verifyPasswords != true)
            {
                TempData["message"] = "Password and confirm password doesn't match";
                model.Password = model.ConfirmPassword = "";
                return View(model);
            }

            var response = await service.GetUser(model.UserName);

            if (response != null)
            {
                TempData["message"] = "User already exist try a different Username";
                return View();
            }
            else
            {

                model.CreatedBy = model.FullName;
                model.CreatedOn = DateTime.Now;
                try
                {
                    dynamic res;
                    if (model.IsTasker == true)
                    {
                        res = await service.AddUser(model.ToDb(), WebUtils.TASKER_ROLE);
                    }
                    else
                    {
                        res = await service.AddUser(model.ToDb(), WebUtils.POSTER_ROLE);
                    }

                    if (res)
                    {
                        TempData["message"] = "User registed successfully";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        TempData["message"] = "Something went wrong";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    TempData["message"] = ex.Message.ToString();
                    return View();
                }

            }
        }

        [HttpGet]
        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("account-info")]
        public async Task<IActionResult> AccountInfo()
        {
            try
            {
                #region session
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                ViewBag.UserName = HttpContext.Session.GetString("username");
                if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");
                #endregion session

                string username = ViewBag.UserName;
                logger.LogInformation($"Manege Users info");

                var user = service.GetUser(username).Result.ToModel();
                return View(user);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string? name)
        {


            try
            {
                #region session
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                //if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");
                #endregion session

                logger.LogInformation($"Manege Users info");
                if (string.IsNullOrWhiteSpace(name))
                {
                    name = HttpContext.Session.GetString("username");
                    var user = service.GetUser(name).Result.ToModel();
                    return View(user);

                }
                else
                {
                    var user = service.GetUser(name).Result.ToModel();
                    return View(user);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        [Route("/ChangePassword")]
        public async Task<IActionResult> ChangePassword()
        {
            try
            {
                #region session
                ViewBag.Name = HttpContext.Session.GetString("Name");
                ViewBag.Role = HttpContext.Session.GetString("Role");
                ViewBag.UserName = HttpContext.Session.GetString("username");
                if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");
                #endregion session

                string username = ViewBag.UserName;
                logger.LogInformation($"{username} Password Change");

                var user = service.GetUser(username).Result.ToModel();
                user.Password = string.Empty;
                return View(user);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Route("/ChangePassword")]
        public async Task<IActionResult> ChangePassword(UserModel user)
        {
            try
            {
                TempData["message"] = "Password Changed Successfully";

                if (WebUtils.PassAndConfirmPassMathing(user.Password, user.ConfirmPassword) == false) TempData["message"] = "Password and Confirm Password doesn't match";

                ViewBag.UserName = HttpContext.Session.GetString("username");
                string username = ViewBag.UserName;
                logger.LogInformation($"{username} Password Change");

                string oldPassword = user.OldPassword;
                string newPassword = user.ConfirmPassword;

                var response = await service.ChangePassword(newPassword, oldPassword, username);
                return View(user);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = ex.Message.ToString();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}