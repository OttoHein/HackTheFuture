using HackTheFuture.PL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace HackTheFuture.PL.Controllers
{
    public class Challenge1Controller : Controller
    {
        // GET: Challenge1
        public ActionResult GetChallenge()
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

            Challenge challenge = JsonConvert.DeserializeObject<Challenge>(resultString);

            return View(challenge);
        }

        [HttpPost]
        public ActionResult PostChallenge(string challengeid)
        {
            string json = "{\"challengeId\": \"" + challengeid + "\",\"values\": [{\"name\": \"name\",\"data\": \"Dackotton\"},{\"name\": \"secret\",\"data\": \"Dackotton\"}]}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://htf2018.azurewebsites.net/");

                //HTTP POST
                var postTask = client.PostAsJsonAsync("Challenges", json);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.test = json;
                ViewBag.responseMessage = result;

            }
            return View();
        }

    }
}