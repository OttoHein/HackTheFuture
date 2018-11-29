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

            return View();
        }
    }
}