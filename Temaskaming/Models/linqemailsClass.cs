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
        public email_signup getSignupsbyID(int _id)
        {
            var allSignups = objSignup.email_signups.SingleOrDefault(x => x.Id == _id);
            return allSignups;
        }
        //method which delets a review based on id
        public bool commitDelete(int _id)
        {
            using (objSignup)
            {
                var emailDel = objSignup.email_signups.Single(x => x.Id == _id);
                objSignup.email_signups.DeleteOnSubmit(emailDel);
                objSignup.SubmitChanges();
                return true;
            }
        }
        public bool commitUpdate(int _id, string _ename, string _elname, string _eemail)
        {
            using (objSignup)
            {
                var emailUpd = objSignup.email_signups.Single(x => x.Id == _id);
                emailUpd.ename = _ename;
                emailUpd.elname = _elname;
                emailUpd.eemail = _eemail;
                objSignup.SubmitChanges();
                return true;
            }
        }
    }
}