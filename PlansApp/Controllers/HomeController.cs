using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlansApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult accountInformation()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Have you ever wished that there was a really easy and convenient way to keep track of friends and family when they are away from you?";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}