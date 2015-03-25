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
         public ContactForm getCFId(int _id)
   {
       var AllCF = objCF.ContactForms.SingleOrDefault(x => x.id == _id);
                 return AllCF;
   }
        public bool  CFInsert(ContactForm CF)
         {
            using (objCF)
            {
                objCF.ContactForms.InsertOnSubmit(CF);
                objCF.SubmitChanges();
                return true;
            }
         }
    
    }

   


}