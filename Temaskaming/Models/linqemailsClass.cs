using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class linqemailsClass
    {
        //creating an instance of LINQ object
        databaseDataContext objSignup = new databaseDataContext();
        //method which inserts created ecard
        public bool commitInsert(email_signup esignup)
        {
            using (objSignup)
            {
                objSignup.email_signups.InsertOnSubmit(esignup);
                objSignup.SubmitChanges();
                return true;
            }
        }
        public IQueryable<email_signup> getSignups()
        {
            //creating anonymous varible with value of instance of LINQ object
            var allSignups = objSignup.email_signups.Select(x => x);
            return allSignups;
        }
    }
}