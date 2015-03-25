using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class ContactFormClass
    {

        databaseDataContext objCF = new databaseDataContext();

   public IQueryable<ContactForm> getCF()
        {
            var allCF = from x in objCF.ContactForms select x;
            return allCF; 
        }

        //get id 
   public ContactForm getCFByID(int _id)
   {
       var allCF = objCF.ContactForms.SingleOrDefault(x => x.id == _id);
       return allCF; 
   }

        //insert for public view page
        public bool commitInsert(ContactForm CF)
   {
            using (objCF)//displose of our connection
            {
                objCF.ContactForms.InsertOnSubmit(CF);
                objCF.SubmitChanges();
                return true; 
            }
   }
   
        //Delete for admin 
        public bool commitDelete(int _id)
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