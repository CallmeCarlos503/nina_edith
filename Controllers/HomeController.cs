using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MVC_tarea.Models;
using static System.Net.WebRequestMethods;

namespace MVC_tarea.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Estadisticas()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Acerca()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}