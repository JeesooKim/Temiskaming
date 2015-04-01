using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

// controller which inserts, updates, and deletes data from tables for job applications and job postings
namespace Temiskaming.Controllers
{
    public class AdminJobPostingsController : Controller
    {
        linqjobsClass objJob = new linqjobsClass();

        // action result that shows all job postings
        public ActionResult AdminShowJobPostings()
        {
            var jobpostings = objJob.getJobs();
            return View(jobpostings);
        }

        // action result which shows all job applications
        public ActionResult ShowJobApp()
        {
            var jobapps = objJob.getApplications();
            return View(jobapps);
        }

        //action result which shows the form for new job posting to be created
        public ActionResult NewJob()
        {
            return View();
        }

        // action result which on post inserts new job posting into database
        [HttpPost]
        public ActionResult NewJob(job validjob)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitInsert(validjob);
                    return RedirectToAction("AdminShowJobPostings");
                }
                catch
                {
                    return View();
                }
            }
            else return View();
        }

        //action result which shows selected job posting to be updated
        public ActionResult ViewJobDetails(int id)
        {
            var job = objJob.getJobsById(id);
            if (job == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(job);
            }
        }

        // action result which on post updates job posting if the data is valid
        [HttpPost]
        public ActionResult ViewJobDetails(int id, job validjob)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitUpdatejob(id, validjob.jobtitle, validjob.jobdescr, validjob.posteddate.Value, validjob.qualifications, validjob.duration);
                    return RedirectToAction("AdminShowJobPostings");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //action result which on post updates selected job application if the data is valid
        [HttpPost]
        public ActionResult ViewDetailsJobApp(int id, jobapplication validjobapp)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objJob.commitUpdateApp(id, validjobapp.anotes, validjobapp.jobtitle, validjobapp.aname, validjobapp.aphone, validjobapp.aemail, validjobapp.aresume, validjobapp.acoverletter);
                    return RedirectToAction("ShowJobApp");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //action result which shows selected job application
        public ActionResult ViewDetailsJobApp(int id)
        {
            //setting variables in the viewbag to be used later in hidden fields which don't change when updating the job application
            var jobapplication = objJob.getAppsById(id);
            ViewBag.jobtitle = jobapplication.jobtitle;
            ViewBag.aname = jobapplication.aname;
            ViewBag.aphone = jobapplication.aphone;
            ViewBag.aemail = jobapplication.aemail;
            ViewBag.aresume = jobapplication.aresume;
            ViewBag.acoverletter = jobapplication.acoverletter;

            if (jobapplication == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(jobapplication);
            }
        }

        // action method which deletes job posting from the database based on id 
        public ActionResult DeleteJob(int id)
        {
            var job = objJob.getJobsById(id);
            if (job == null)
            {
                return View("NotFound");
            }
            else
            {
                objJob.commitDelete(id);
                return RedirectToAction("AdminShowJobPostings");
            }
        }

        // action method which deletes job application from the database based on id 
        public ActionResult DeleteApp(int id)
        {
            var app = objJob.getAppsById(id);
            if (app == null)
            {
                return View("NotFound");
            }
            else
            {
                objJob.commitAppDelete(id);
                return RedirectToAction("ShowJobApp");
            }
        }

    }
}
