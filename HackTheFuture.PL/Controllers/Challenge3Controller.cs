using HackTheFuture.PL.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
namespace HackTheFuture.PL.Controllers
{
    public class Challenge3Controller : Controller
    {
        // GET: Challenge3
        public ActionResult GetChallenge(string url)
        {
            Uri uri = new Uri("http://htf2018.azurewebsites.net/");
            url = "Challenges/5be0b78cc8dc68e6009a8b6fd347a819";

            var client = new RestClient(uri);

            var request = new RestRequest(url, Method.GET);
            request.AddHeader("htf-identification", "M2NkMzQyZTQtMDI2MC00OWM0LTgxNzctYjM1Nzc1MzQxY2Ji");

            var response = client.Execute(request);

            Challenge challenge = JsonConvert.DeserializeObject<Challenge>(response.Content);

            return View(challenge);
        }
    }
}
