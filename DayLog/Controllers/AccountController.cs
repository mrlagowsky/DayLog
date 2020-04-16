using DayLog.Models.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLog.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginResponse response)
        { 
            if(!response.Authenticated)
            {
                return View();
            }
            else
            {
                string authId = Guid.NewGuid().ToString();

                Session["AuthID"] = authId;

                var cookie = new HttpCookie("AuthID");
                cookie.Value = authId;
                Response.Cookies.Add(cookie);

                return RedirectToAction("Private");
            }
        }
    }
}