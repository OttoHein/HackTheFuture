using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HackTheFuture.PL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string resultString = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://htf2018.azurewebsites.net/");
                //HTTP GET
                var responseTask = client.GetAsync("Challenges");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStreamAsync();
                    readTask.Wait();

                    StreamReader reader = new StreamReader(readTask.Result);
                    resultString = reader.ReadToEnd();
                }
                else //web api sent error response 
                {
                    //log response status here.
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }

            ViewBag.resultString = resultString;

            return View();
        }
    }
}