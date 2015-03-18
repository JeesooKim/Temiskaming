using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class alertClass{
        
        databaseDataContext objAlert = new databaseDataContext();

        public IQueryable<alert> getAllAlerts()
        {
            var allAlerts = objAlert.alerts.Select(x => x);

            return allAlerts;
        }

        public alert getAlertByID(int _id)
        {
            var oneAlert = objAlert.alerts.SingleOrDefault(x => x.alertId == _id);
            return oneAlert;
        }

        public bool commitInsertAlert(alert newAlert)
        {

            using (objAlert)
            {
                objAlert.alerts.InsertOnSubmit(newAlert);
                objAlert.SubmitChanges();
                return true;
            }
        }


        public bool commitUpdateAlert(int _id, string _title, string _level, string _description, DateTime _timeline, bool _status)
        {
            using (objAlert)
            {

                var updateAlert = objAlert.alerts.Single(x => x.alertId == _id);

                updateAlert.alertTitle = _title;
                updateAlert.alertLevel = _level;
                updateAlert.alertDescription = _description;
                updateAlert.alertTimeline = _timeline;
                updateAlert.alertStatus = _status;
                
                objAlert.SubmitChanges();
                return true;

            }
        }

        public bool commitDeleteAlert(int _id)
        {

            using (objAlert)
            {

                var deleteAlert = objAlert.alerts.Single(x => x.alertId == _id);

                objAlert.alerts.DeleteOnSubmit(deleteAlert);
                objAlert.SubmitChanges();
                return true;

            }
        }



    }
}