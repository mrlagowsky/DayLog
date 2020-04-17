using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLog.Controllers
{
    public class DayCardsController : Controller
    {
        // GET: Site
        [HttpGet]
        public ActionResult NewCard()
        {
            //Check if today's card has already been filled
            return View();
        }

        [HttpGet]
        public ActionResult CardCompleted()
        {
            //Check if today's card has already been filled
            return View();
        }

       
    }
}