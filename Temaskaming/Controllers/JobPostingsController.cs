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

        public ActionResult ViewPostings()
        {
            var jobpostings = objJob.getJobs();
            ViewBag.Group = "JoinOurTeam";
            return View(jobpostings);
        }

        public ActionResult ApplytoJob()
        {
            ViewBag.Group = "JoinOurTeam";
            return View();
        }

        [HttpPost]
        public ActionResult ApplytoJob(jobapplication valid)
        {
            ViewBag.Group = "JoinOurTeam";
            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitInsertApplic(valid);
                    return PartialView("Appliedpartial", valid);
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
