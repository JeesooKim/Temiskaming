using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class JobPostingsController : Controller
    {
        linqjobsClass objJob = new linqjobsClass();
        
        //action method which shows all job postings
        public ActionResult ViewPostings()
        {
            ViewBag.Group = "JoinOurTeam";
            var jobpostings = objJob.getJobs();
            return View(jobpostings);
        }

        //action method which directs user to apply form for selected job
        public ActionResult ApplytoJob(string passjob)
        {
            ViewBag.Group = "JoinOurTeam";
            @ViewBag.passjob = passjob;
            return View();
        }

        //action method which on post inserts user job application if the data is valid
        [HttpPost]
        public ActionResult ApplytoJob(jobapplication valid)
        {
            ViewBag.Group = "JoinOurTeam";

            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitInsertApplic(valid);
                    return PartialView("Appliedpartial",valid);
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}
