using Microsoft.AspNetCore.Mvc;
using Models.DbModels;
using Models.WebModels;
using System;
using System.Linq.Expressions;

namespace Middleware.Controllers
{
    public class RecommendedController : Controller
    {
        public IActionResult Index(string? skill)
        {
            IEnumerable<R_User> model = null;
            try
            {
                
                ViewData["SKILL"] = (string.IsNullOrWhiteSpace(skill))? null : skill.ToUpper();
                string url = string.Empty;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://127.0.0.1:8000/");
                    if(skill != null) 
                    {
                        url = $"recommendedUsers/{skill}/";
                    }
                    else
                    {
                        url = "users";
                    }

                    //HTTP GET
                    var responseTask = client.GetAsync(url);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadFromJsonAsync<IList<R_User>>();
                        readTask.Wait();

                        model = readTask.Result;
                    }
                    else //web api sent error response 
                    {
                        //log response status here..

                        model = Enumerable.Empty<R_User>();

                        //ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    }


                    foreach (var item in model)
                    {
                        if(item.PhoneOne == 0)
                        {
                            Random random = new Random();
                           item.PhoneOne = 3080000000 + random.Next(400000000);
                        }
                    }

                return View(model); 
                }
            }
            catch(Exception ex)
            {
                model = Enumerable.Empty<R_User>();
                return View(model);
            }
        }
    }
}
