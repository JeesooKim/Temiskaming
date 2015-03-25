using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;
// controller which creates new job application and shows job postings
namespace Temiskaming.Controllers
{
    public class JobPostingsController : Controller
    {
        linqjobsClass objJob = new linqjobsClass();
        
        //action method which shows all job postings
        public ActionResult ViewPostings()
        {
            var jobpostings = objJob.getJobs();
            return View(jobpostings);
        }

        //action method which directs user to apply form for selected job
        public ActionResult ApplytoJob(string passjob)
        {
            @ViewBag.passjob = passjob;
            return View();
        }

        //action method which on post inserts user job application if the data is valid
        [HttpPost]
        public ActionResult ApplytoJob(jobapplication valid)
        {

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
