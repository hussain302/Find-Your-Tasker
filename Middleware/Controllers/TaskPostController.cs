using Interfaces.TaskerInterfaces;
using Interfaces.UserInterfaces;
using Mappers.TaskMappers;
using Mappers.UserMappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Middleware.Common;
using Models.DbModels;
using Models.WebModels;
using System;

namespace Middleware.Controllers
{
    
    public class TaskPostController : Controller
    {
        private readonly ITaskPostRepository taskPostService;
        private readonly ILogger<TaskPostController> logger;
        private readonly IUserRepository userService;
        private readonly ITaskStatusRepository taskStatusService;
        private readonly ISubCategoryRepository subCatService;

        public TaskPostController(ITaskPostRepository TaskPostService,
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

        //[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        //[Route("get-all-tasks")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //HttpContext.Session.Clear();
            try
            {

                TimeSpan ts = new TimeSpan();
                ts = ts.Subtract(TimeSpan.FromDays(10));

               ViewBag.Name = HttpContext.Session.GetString("Name");
               ViewBag.Role = HttpContext.Session.GetString("Role");

                    var models = taskPostService.GetAll().Result
                    .Where(x => x.TaskStatus.TaskStatusName == WebUtils.POST_APPROVED_STATUS)
                    .Select(x => x.ToModel()).ToList();
                models = models.Where(x=>x.CreatedOn > DateTime.Now + ts).ToList();
                if (models.Count == 0) TempData["message"] = "No approved task added yet";

                logger.LogInformation($" Get All Tasks called. Count = {models.Count}");
                return View(models);
            }
            catch (Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }



        

        //[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        //[Route("get-all-tasks")]
        //public IActionResult GetAllByCategory()
        //{
        //    try
        //    {
        //        var models = taskPostService.GetAll().Result
        //            .Where(x => x.TaskStatus.TaskStatusName == WebUtils.APPROVED_STATUS)
        //            .Select(x => x.ToModel()).ToList();

        //        if (models.Count == 0) TempData["message"] = "No approved task added yet";

        //        logger.LogInformation($" Get All Tasks called. Count = {models.Count}");
        //        return View(models);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError($"{ex.Message}");
        //        TempData["message"] = $"{ex.Message}";
        //        return View();
        //    }
        //}
        //[ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Client, VaryByHeader = "User-Agent")]
        //[Route("/Post-task")]

        [HttpGet]
        public IActionResult PostTask()
        {
            #region session 

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Role = HttpContext.Session.GetString("Role");
            if (ViewBag.Name == null && ViewBag.Role == null) return RedirectToAction("login", "User");

            #endregion session

            ViewBag.subCategory = subCatService.GetAll().Result.Select(x => x.ToModel());
            
            logger.LogInformation($"PostTask Get method called.");
            return View();
        }

        [ResponseCache(Duration = 3600, Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]        
        [HttpPost]
        public async Task<IActionResult> PostTask(TasksModel model)
        {
            
            logger.LogInformation($"PostTask Post method called.");

            try
            {
                ViewBag.subCategory = subCatService.GetAll().Result.Select(x => x.ToModel());

                #region assigning default values
                string username = HttpContext.Session.GetString("username");

                var poster = userService.GetUser(username).Result.ToModel();
                var taskStatus= taskStatusService.Get(WebUtils.POST_UNASSIGNED_STATUS).Result.ToModel();

                model.DateOfAssiging = Convert.ToDateTime(DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                model.PostedById = poster.UserId;

                model.TaskStatusId = taskStatus.TaskStatusId;
                model.IsValid = false;
                model.CreatedBy = username;
                model.CreatedOn = Convert.ToDateTime(DateTime.Now.ToString("dddd, dd MMMM yyyy"));
                #endregion


                var response = await taskPostService.Add(model.ToDb());
                if (response == true)
                {
                    TempData["message"] = $"Posted Task successfully";
                    return RedirectToAction("GetAll", "TaskPost");
                }
                else
                {
                    TempData["message"] = $"Error while adding your post";
                    return View();
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"{ex.Message}");
                TempData["message"] = $"{ex.Message}";
                return View();
            }
        }
    }
}
