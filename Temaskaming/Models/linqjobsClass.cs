using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class linqjobsClass
    {
        //creating an instance of LINQ object
        databaseDataContext objJob = new databaseDataContext();

        public IQueryable<job> getJobs()
        {
            //creating anonymous varible with value of instance of LINQ object
            var allJobs = objJob.jobs.Select(x => x);
            return allJobs;
        }
        //method which inserts created ecard
        public bool commitInsertApplic(jobapplication japplic)
        {
            using (objJob)
            {
                objJob.jobapplications.InsertOnSubmit(japplic);
                objJob.SubmitChanges();
                return true;
            }
        }
        //method which gets a review from database by id
        public job getJobsById(int _id)
        {
            var allJobs = objJob.jobs.SingleOrDefault(x => x.Id == _id);
            return allJobs;
        }
    }
}