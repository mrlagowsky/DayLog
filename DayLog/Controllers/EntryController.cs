using DayLog.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DayLog.Controllers
{
    /// <summary>
    /// Controller for the site's internal day entry logic
    /// </summary>
    public class EntryController : Controller
    {
        [HttpGet]
        public ActionResult NewEntry()
        {
            //Check if today's entry has already been filled
            return View();
        }

        /// <summary>
        /// POST Method to create a new journal entry
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult NewEntry(EntryDetails details)
        {
            //Checking if the model's properties are all validated
            if(ModelState.IsValid)
            {
                try
                {
                    //Extract the user ID from the cookies
                    int _userID = Convert.ToInt32(Session["UserID"]);
                    DBHelper helper = new DBHelper();

                    //Creating the entry, expecting value of true for a successful insert
                    if (helper.CreateJournalEntry(_userID, details.EntryContent, details.MoodID))
                    {
                        return RedirectToAction("EntrySuccess");
                    }
                    else
                    {
                        //No exception and no errors but entry wasn't successful logic
                        return RedirectToAction("ErrorPage", "Account", new { exceptionMessage = "Something went wrong, please try again later!" });
                    }
                }
                //Catch the exception and pass its content onto the error page
                catch (Exception ex)
                {
                    return RedirectToAction("ErrorPage", "Account", new { exceptionMessage = ex.Message });
                }
            }
            //Check if today's card has already been filled
            return View();
        }

        [HttpGet]
        public ActionResult EntryCompleted()
        {
            //Check if today's card has already been filled
            return View();
        }

        [HttpGet]
        public ActionResult ViewEntries()
        {
            ViewBag.Images = new string[] { "Hey", "Hey2", "Hey3" };
            return View();
        }
       
    }
}