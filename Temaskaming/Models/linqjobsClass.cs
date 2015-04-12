using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class linqjobsClass
    {
        databaseDataContext objJob = new databaseDataContext();

        //method which gets job postings from database
        public IQueryable<job> getJobs()
        {
            var allJobs = objJob.jobs.Select(x => x);
            return allJobs;
        }
        //method which gets job applications from database
        public IQueryable<jobapplication> getApplications()
        {
            var allApplications = objJob.jobapplications.Select(x => x);
            return allApplications;
        }

        //method which inserts job application to database
        public bool commitInsertApplic(jobapplication japplic)
        {
            using (objJob)
            {
                objJob.jobapplications.InsertOnSubmit(japplic);
                objJob.SubmitChanges();
                return true;
            }
        }
        //method which gets job posting by id
        public job getJobsById(int _id)
        {
            var allJobs = objJob.jobs.SingleOrDefault(x => x.Id == _id);
            return allJobs;
        }

        //method which gets job application by id
        public jobapplication getAppsById(int _id)
        {
            var allApps = objJob.jobapplications.SingleOrDefault(x => x.Id == _id);
            return allApps;
        }

        //method which insets job posting
        public bool commitInsert(job jobvalid)
        {
            using (objJob)
            {
                objJob.jobs.InsertOnSubmit(jobvalid);
                objJob.SubmitChanges();
                return true;
            }
        }

        //method which deletes job posting based on id
        public bool commitDelete(int _id)
        {
            using (objJob)
            {
                var jobdDel = objJob.jobs.Single(x => x.Id == _id);
                objJob.jobs.DeleteOnSubmit(jobdDel);
                objJob.SubmitChanges();
                return true;
            }
        }

        //method which deletes job application based on id
        public bool commitAppDelete(int _id)
        {
            using (objJob)
            {
                var appdDel = objJob.jobapplications.Single(x => x.Id == _id);
                objJob.jobapplications.DeleteOnSubmit(appdDel);
                objJob.SubmitChanges();
                return true;
            }
        }

        //method which updades job posting
        public bool commitUpdatejob(int _id, string _jobtitle, string _jobdescr, DateTime _posteddate, string _qualifications, string _duration)
        {
            using (objJob)
            {
                var jobdUpd = objJob.jobs.Single(x => x.Id == _id);
                jobdUpd.jobtitle = _jobtitle;
                jobdUpd.jobdescr = _jobdescr;
                jobdUpd.posteddate = _posteddate;
                jobdUpd.qualifications = _qualifications;
                jobdUpd.duration = _duration;
                objJob.SubmitChanges();
                return true;
            }
        }

        //method which updates job application
        public bool commitUpdateApp(int _id, string _anotes, string _jobtitle, string _aname, string _aphone, string _aemail, string _aresume, string _acoverletter)
        {
            using (objJob)
            {
                var appdUpd = objJob.jobapplications.Single(x => x.Id == _id);
                appdUpd.anotes = _anotes;
                appdUpd.jobtitle = _jobtitle;
                appdUpd.aname = _aname;
                appdUpd.aphone = _aphone;
                appdUpd.aemail = _aemail;
                appdUpd.aresume = _aresume;
                appdUpd.acoverletter = _acoverletter;
                objJob.SubmitChanges();
                return true;
            }
        }
    }
}