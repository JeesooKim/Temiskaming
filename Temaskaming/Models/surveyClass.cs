using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class surveyClass
    {
        
        databaseDataContext objPoll = new databaseDataContext();

        public IQueryable<poll> getAllPolls()
        {
            var allPolls = objPoll.polls.Select(x => x);

            return allPolls;
        }

        public poll getPollByID(int _id)
        {
            var onePoll = objPoll.polls.SingleOrDefault(x => x.id == _id);
            return onePoll;
        }

        public poll getActivePoll()
        {
            var onePoll = objPoll.polls.SingleOrDefault(x => x.published == true);
            return onePoll;
        }

        public bool commitInsertPoll(poll newPoll)
        {

            using (objPoll)
            {
                objPoll.polls.InsertOnSubmit(newPoll);
                objPoll.SubmitChanges();
                return true;
            }
        }


        public bool commitUpdatePoll(int _id, string _question, string _option1, string _option2)
        {
            using (objPoll)
            {

                var updatePoll = objPoll.polls.Single(x => x.id == _id);

                updatePoll.question = _question;
                updatePoll.option1 = _option1;
                updatePoll.option2 = _option2;
                
                objPoll.SubmitChanges();
                return true;

            }
        }

        public bool commitDeletePoll(int _id)
        {

            using (objPoll)
            {

                var deletepoll = objPoll.polls.Single(x => x.id == _id);

                objPoll.polls.DeleteOnSubmit(deletepoll);
                objPoll.SubmitChanges();
                return true;

            }
        }



    }
    
}