using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SupportWheelOfFate.Core;
using SupportWheelOfFate.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace SupportWheelOfFate.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string inquireDateValue = null)
        {
            if (string.IsNullOrEmpty(inquireDateValue))
            {
                inquireDateValue = DateTime.Today.Date.ToString("yyyy-MM-dd");
            }

            var url = "https://fj7w0figk9.execute-api.us-east-1.amazonaws.com/prod/engineers/" + inquireDateValue; // Obtain list of all Engineers

            var model = new SupportViewModel
            {
                SupportDate = Convert.ToDateTime(inquireDateValue)
            };

            using (var client = new HttpClient())
            {
                var apiResponse = client.GetAsync(url).Result;

                if (apiResponse.IsSuccessStatusCode)
                {
                    var responseContent = apiResponse.Content;

                    string json = responseContent.ReadAsStringAsync().Result;

                    try
                    {
                        model.AssignedEngineers = JsonConvert.DeserializeObject<List<Engineer>>(json);
                        return View(model);
                    }
                    catch
                    {
                        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = "Failed to retrieve a List of Engineers from the API." });
                    }
                }
                else
                {
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, ErrorMessage = "Failed to retrieve a List of Engineers from the API." });
                }
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
