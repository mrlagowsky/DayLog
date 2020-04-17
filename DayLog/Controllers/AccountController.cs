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

        /// <summary>
        /// Post method to try and authenticate a user 
        /// </summary>
        /// <param name="response">Passing in the model object with populated username and password values</param>
        /// <returns>A new LoginResponse object with information about the login attempt</returns>
        [HttpPost]
        public ActionResult Login(LoginResponse response)
        {
            //Creating a helper object and trying to login the user
            DBHelper helper = new DBHelper();
            LoginResponse _loginResponse;

            //Checking if the modelstate is valid
            if (ModelState.IsValid)
            {
                _loginResponse = helper.TryLogin(response.Username, response.Password);
            }
            else
            {
                return View(response);
            }
            
            //In case of unsuccessful login attempt
            if(!_loginResponse.Authenticated)
            {
                ViewBag.Message = "Invalid login";
                return View(_loginResponse);
            }
            else if(!_loginResponse.Authenticated && !_loginResponse.User_Found)
            {
                ViewBag.Message = "User not found";
                return View(_loginResponse);
            }
            //Successful login
            else
            {
                string authId = Guid.NewGuid().ToString();

                Session["AuthID"] = authId;

                var cookie = new HttpCookie("AuthID");
                cookie.Value = authId;
                Response.Cookies.Add(cookie);

                try
                {
                    if (_loginResponse.UserID != 0 && helper.IsCardNeeded(_loginResponse.UserID))
                    {
                        //Sending the user to create a new journal entry for today
                        return RedirectToAction("NewCard", "DayCards");
                    }
                    else
                    {
                        //Telling the end user that a card has already been completed for today
                        return RedirectToAction("CardCompleted", "DayCards");
                    }
                }
                catch(Exception ex)
                {
                    return RedirectToAction("ErrorPage");
                }
                
                
            }
        }

        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}