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
    public class Challenge2Controller : Controller
    {
        // GET: Challenge2
        public ActionResult GetChallenge(string url)
        {
            Uri uri = new Uri("http://htf2018.azurewebsites.net/");

            var client = new RestClient(uri);

            var request = new RestRequest(url, Method.GET);
            request.AddHeader("htf-identification", "Hackotton");

            var response = client.Execute(request);

            ChallengeResponse challenge = JsonConvert.DeserializeObject<ChallengeResponse>(response.Content);

            return View(challenge);
        }
    }
}