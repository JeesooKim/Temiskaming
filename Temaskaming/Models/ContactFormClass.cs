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
         public ContactForm 
    
    
    }

   


}