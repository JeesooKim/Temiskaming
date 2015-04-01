using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    /*
     * This class is used for all database related activities for 
     * Table - appointments
    */
    public class appointmentClass
    {


        databaseDataContext objAppointment = new databaseDataContext();
        //creating an instance of LINQ object: objAppointment


        public IQueryable<appointment> getAllAppointments()
        {
            var allAppointments = objAppointment.appointments.Select(x => x);

            return allAppointments;
        }

        public appointment getAppointmentByID(int _id)
        {
            var oneAppointment = objAppointment.appointments.SingleOrDefault(x => x.id == _id);
            return oneAppointment;
        }

        public bool commitInsert(appointment newAppointment)
        {

            using (objAppointment)
            {
                objAppointment.appointments.InsertOnSubmit(newAppointment);
                objAppointment.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _id, int _docID, DateTime _bookingDate, string _email, string _phone)
        {
            using (objAppointment)
            {

                var updateAppointment = objAppointment.appointments.Single(x => x.id == _id);
                updateAppointment.doctor_id = _docID;
                updateAppointment.booking_date = _bookingDate;
                updateAppointment.email = _email;
                updateAppointment.phone = _phone;

                objAppointment.SubmitChanges();
                return true;

            }
        }

        public bool commitDelete(int _id)
        {

            using (objAppointment)
            {

                var objDelAppointment = objAppointment.appointments.Single(x => x.id == _id);

                objAppointment.appointments.DeleteOnSubmit(objDelAppointment);
                objAppointment.SubmitChanges();
                return true;

            }
        }

    }
}