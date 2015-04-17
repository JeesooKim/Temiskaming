using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Web.Helpers;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class SurveyController : Controller
    {
        //
        // GET: /Survey/

        // properties
        surveyClass pollObj = new surveyClass();

        //
        // 

        [Authorize(Roles = "Admin")]
        public ActionResult Index()                 // Display a list of polls from db to the admin
        {
            ViewBag.Group = "Admin";
            var allPolls = pollObj.getAllPolls();
            if (allPolls == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(allPolls);              // return a list of polls to index view
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult PublishPoll(int id)                 // Admin, publish a poll from a list of polls
        {                                                       
            ViewBag.Group = "Admin";
            bool result = pollObj.publishPoll(id);              // changes the value of the publish column to true for a particular id
            if (result == false)                                // and if any post is already published, changes the value to false
            {                                                   // so that only one poll is active at a given time.
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult PostedPoll()                    // Get : Public page, display the users with a poll that has been published  
        {                                                   // by admin
           
            var onePoll = pollObj.getActivePoll();
            if (onePoll == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(onePoll);
            }

        }


        [HttpPost]
        public ActionResult PostedPoll(FormCollection form)  // POST : Public page, accepts the value from user and 
        {                                                    // updates the value to the database.
            
            if(form["option1"] != null)
            { 
                try 
                {
                    pollObj.setPollChoice(1);                   //update count of choice 1
                    return RedirectToAction("PostedPoll");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                try
                {
                    
                    pollObj.setPollChoice(0);                   // update count of choice 2
                    return RedirectToAction("PostedPoll");
                }
                catch
                {
                    return View();
                }
            }

  
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddPoll()               // Get: Admin Page, shows a form to add a poll
        {
            ViewBag.Group = "Admin";
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddPoll(poll newPoll)    // Post : Admin Page, form is submitted and added to the database
        {
            newPoll.published = false;              // published vlaue is by default set to false.
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    pollObj.commitInsertPoll(newPoll);
                    return RedirectToAction("Index");         // redirects to list of all polls
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdatePoll(int id)       //  Get : admin page , update a poll from the list of polls
        {
            ViewBag.Group = "Admin";
            poll loadPoll = pollObj.getPollByID(id);      
            if (loadPoll == null)
            {
                return View();
            }
            else
            {
                return View(loadPoll);               // displays the poll will all the data that can be edited
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdatePoll(int id, poll updPoll)      // Post : admin page, with the data that has been edited
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    pollObj.commitUpdatePoll(id, updPoll.question, updPoll.option1, updPoll.option2);   // data sent for updation
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePoll(int id)            // get : admin page, delete a poll from a list of polls 
        {
            ViewBag.Group = "Admin";
            poll loadPoll = pollObj.getPollByID(id);
            if (loadPoll == null)
            {
                return View();
            }
            else
            {
                TempData["id"] = id;
                return View(loadPoll);                      // loads the poll that has to be deleted, with a option to confirm delete
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult ConfirmDeletePoll()             // get : admin page, when confirm delete is clicked, 
        {                                                   // delete poll from database
            int id = (int)TempData["id"];
            ViewBag.Group = "Admin";
            if (id > 0)
            {
                try
                {
                    pollObj.commitDeletePoll(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DisplayChart()                  // get : admin page, display the results in form of charts.
        {

           // var myChart = new Chart();

       //     var myChart = new Chart(width: 600, height: 400)
       //.AddTitle("Employees")
       //.AddSeries(chartType: "pie",
       //   xValue: new[] { "Peter", "Andrew", "Julie", "Mary", "Dave" },
       //   yValues: new[] { "2", "6", "4", "5", "3" });

       //     ViewBag.Chart = myChart;

           return View();

        }

       
    }
}
