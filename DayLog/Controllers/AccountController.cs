using DayLog.Models.Data;
using DayLog.Models.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLog.Controllers
{
    public class AccountController : Controller
    {
        /// <summary>
        /// Action to get the Login page
        /// </summary>
        /// <returns>A login page view</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Post method to try and authenticate a user 
        /// </summary>
        /// <param name="response">Passing in the model object with populated username and password values</param>
        /// <returns>A new LoginResponse object with information about the login attempt</returns>
        [HttpPost, ValidateHeaderAntiForgeryToken]
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
            else if(!_loginResponse.Authenticated && !_loginResponse.UserFound)
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
                catch (Exception ex)
                {
                    return RedirectToAction("ErrorPage", new { exceptionMessage = ex.Message });
                }
                
                
            }
        }

        /// <summary>
        /// Action taking the user to the register page
        /// </summary>
        /// <returns>A form view with registration fields</returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Register POST action which will try to create a new user in the system
        /// </summary>
        /// <param name="userDetails">New user's details</param>
        /// <returns>Will perform a redirect to a successful registration page if it passes the model validation</returns>
        [HttpPost, ValidateHeaderAntiForgeryToken]
        public ActionResult Register(UserDetails userDetails)
        {
            //Check if the model validation is passed
            if(ModelState.IsValid)
            {
                try
                {
                    DBHelper helper = new DBHelper();
                    bool isRegistered = helper.TryRegister(userDetails.Username, userDetails.FirstName, userDetails.Password);
                    if (isRegistered)
                    {
                        return RedirectToAction("RegisterSuccess");
                    }
                    else
                    { 
                        return View(userDetails);
                    }
                }
                catch(Exception ex)
                {
                    //If the registration doesn't work display the 404
                    return RedirectToAction("ErrorPage", new { exceptionMessage = ex.Message });
                }
            }
            else
            {
                //If the details are incorrect, return them to the end user
                return View(userDetails);
            }
        }

        /// <summary>
        /// Action to redirect the end user to a more friendly 404 page with the exception details
        /// </summary>
        /// <param name="exceptionMessage">Passed on details about the encountered problem</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ErrorPage(string exceptionMessage)
        {
            //Give the error message to the error page internally
            ViewBag.Message = exceptionMessage;
            return View();
        }


        /// <summary>
        /// Action to redirect the end user to a welcome page after a successful registration
        /// </summary>
        /// <returns>The welcome view</returns>
        [HttpGet]
        public ActionResult RegisterSuccess()
        {
            return View();
        }
    }
}