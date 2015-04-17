using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class FAQClass
    {
        //class name FAQClass
        databaseDataContext objFAQ = new databaseDataContext();

        public IQueryable<FAQ> getFAQ()
        {
            var allFAQ = from x in objFAQ.FAQs select x;
            return allFAQ;
        }


        //get id
        public FAQ getFAQByID(int _id)
        {
            var allFAQ = objFAQ.FAQs.SingleOrDefault(x => x.Id == _id);
            return allFAQ;
        }

        //insert
        public bool commitInsert(FAQ fa)
        {
            using (objFAQ) //displose of our connection
            {
                objFAQ.FAQs.InsertOnSubmit(fa);
                objFAQ.SubmitChanges();
                return true;
            }

        }

        //update
        public bool commitUpdate(int _id, string _question, string _answer)
        {
            using (objFAQ)
            {
                var FAQUpd = objFAQ.FAQs.Single(x => x.Id == _id);
                FAQUpd.Question = _question;
                FAQUpd.Answer = _answer;
                objFAQ.SubmitChanges();
                return true;
            }

        }

        //Delete
        public bool commitDelete(int _id)
        {
            using (objFAQ)
            {
                var FAQDel =
                    objFAQ.FAQs.Single(x => x.Id == _id);
                objFAQ.FAQs.DeleteOnSubmit(FAQDel);
                objFAQ.SubmitChanges();
                return true;
            }
        }



    }
}