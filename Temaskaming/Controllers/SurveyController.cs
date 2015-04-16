using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        // GET: /Alert/

        public ActionResult Index()
        {
            ViewBag.Group = "Admin";
            var allPolls = pollObj.getAllPolls();
            if (allPolls == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(allPolls);
            }

        }

        public ActionResult PublishPoll(int id)
        {
            ViewBag.Group = "Admin";
            bool result = pollObj.publishPoll(id);
            if (result == false)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult PostedPoll()
        {
           
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
        public ActionResult PostedPoll(FormCollection form)
        {
            
            if(form["option1"] != null)
            { 
                try 
                {
                    pollObj.setPollChoice(1);
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
                    
                    pollObj.setPollChoice(0);
                    return RedirectToAction("PostedPoll");
                }
                catch
                {
                    return View();
                }
            }

  
        }

        public ActionResult AddPoll()
        {
            ViewBag.Group = "Admin";
            return View();
        }


        [HttpPost]
        public ActionResult AddPoll(poll newPoll)
        {
            newPoll.published = false;
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    pollObj.commitInsertPoll(newPoll);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult UpdatePoll(int id)
        {
            ViewBag.Group = "Admin";
            poll loadPoll = pollObj.getPollByID(id);
            if (loadPoll == null)
            {
                return View();
            }
            else
            {
                return View(loadPoll);
            }
        }

        [HttpPost]
        public ActionResult UpdatePoll(int id, poll updPoll)
        {
            ViewBag.Group = "Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    pollObj.commitUpdatePoll(id, updPoll.question, updPoll.option1, updPoll.option2);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult DeletePoll(int id)
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
                return View(loadPoll);
            }
        }

        [HttpPost]
        public ActionResult ConfirmDeletePoll()
        {
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

       
    }
}
