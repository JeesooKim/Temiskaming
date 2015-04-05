using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc.Html;

namespace Temiskaming.Models
{
    public class WaittimeClass
    {
        databaseDataContext objDB = new databaseDataContext();

        public int GetWaitingPatients()
        {
            var waitingPatients = objDB.waittimes.Where(x => x.time_doctor == null).Count();
            return waitingPatients;
        } 

        public IQueryable<waittime> GetRecentPatients(int num)
        {
            var recentPatients = objDB.waittimes.Where(x => x.time_doctor != null).OrderByDescending(x => x.time_doctor).Take(num);
            return recentPatients;
        }

        public double GetWaitTime(int num)
        {
            var patients = GetRecentPatients(num);
            List<double> waittimeList = new List<double>();
            foreach (var x in patients)
            {
                DateTime startTime = x.time_admit;
                DateTime endTime = (DateTime)x.time_doctor;
                var waittime = endTime.Subtract(startTime).TotalMinutes;
                waittimeList.Add(waittime);
            }
            return waittimeList.Average();
        }

        public IQueryable<waittime> GetPatients()
        {
            var allPatients = objDB.waittimes.Select(x => x);
            return allPatients;
        }

        public waittime GetPatientByID(int _id)
        {
            var patient = objDB.waittimes.SingleOrDefault(x => x.id == _id);
            return patient;
        }

        public bool PatientInsert(waittime patient)
        {
            using (objDB)
            {
                objDB.waittimes.InsertOnSubmit(patient);
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool PatientUpdate(int _id,  string _name, DateTime _timeadmit, DateTime? _timedoctor)
        {
            using (objDB)
            {
                var patientUpd = objDB.waittimes.Single(x => x.id == _id);
                patientUpd.name = _name;
                patientUpd.time_admit = _timeadmit;
                patientUpd.time_doctor = _timedoctor;
                objDB.SubmitChanges();
                return true;
            }
        }

        public bool PatientDelete(int _id)
        {
            using (objDB)
            {
                var patientDel = objDB.waittimes.Single(x => x.id == _id);
                objDB.waittimes.DeleteOnSubmit(patientDel);
                objDB.SubmitChanges();
                return true;
            }
        }
    }
}