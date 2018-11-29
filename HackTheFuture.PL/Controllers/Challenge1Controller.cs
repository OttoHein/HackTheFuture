using HackTheFuture.PL.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            string responseMessage = "";
            //string json = "{\"challengeId\": \"" + challengeid + "\",\"values\": [{\"name\": \"name\",\"data\": \"Dackotton\"},{\"name\": \"secret\",\"data\": \"Dackotton\"}]}";
            Uri uri = new Uri("http://htf2018.azurewebsites.net/");

            var client = new RestClient(uri);

            var request = new RestRequest("/Challenges/", Method.POST);

            //request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            request.AddJsonBody(new Answer { ChallengeId = challengeid, Values = new List<Value> { new Value { Name = "name", Data = "Dackotton" }, new Value { Name = "secret", Data = "Dackotton" } } });

            var response = client.Execute(request);

            ChallengeResponse challenge = JsonConvert.DeserializeObject<ChallengeResponse>(response.Content);

            return View(challenge);
        }

    }
}