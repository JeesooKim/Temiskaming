using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class ContactFormClass
    {
        databaseDataContext objCF = new databaseDataContext();



        //get everything from the table
   public IQueryable<ContactForm> getCF()
        {
            var allCF = from x in objCF.ContactForms select x;
            return allCF; 
        }

        //get values by id 
         public ContactForm getCFById(int _id)
   {
       var AllCF = objCF.ContactForms.SingleOrDefault(x => x.id == _id);
                 return AllCF;
   }
        //INSERT
        public bool  CommitInsert(ContactForm CF)
         {
            using (objCF)
            {
                objCF.ContactForms.InsertOnSubmit(CF);
                objCF.SubmitChanges();
                return true;
            }
         }
    //DELETE
        public bool CommitDelete(int _id)
        {
            using (objCF)
            {
                var CFDel =
                    objCF.ContactForms.Single(x => x.id == _id);
                objCF.ContactForms.DeleteOnSubmit(CFDel);
                objCF.SubmitChanges();
                return true;
            }
        }

    }

   


}