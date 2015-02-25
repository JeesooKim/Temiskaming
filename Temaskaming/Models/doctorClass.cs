using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindADoctor.Models
{//FindADoctor: name of the project
    public class doctorClass
    {

        //class name: doctorClass

        doctorLinqDataContext objDoc = new doctorLinqDataContext();
        //creating an instance of LINQ object: objDoc


        public IQueryable<doctor> getDoctors()
        {
            var allDoctors = objDoc.doctors.Select(x => x);

            return allDoctors;
        }

        public doctor getDoctorByID(int _id)
        {
            var alldoctor = objDoc.doctors.SingleOrDefault(x => x.Id == _id);
            return alldoctor;
        }

        public bool commitInsert(doctor doc)
        {

            using (objDoc)
            {
                objDoc.doctors.InsertOnSubmit(doc);
                objDoc.SubmitChanges();
                return true;
            }
        }

        public bool commitUpdate(int _id, string _fname, string _lname, string _title, string _department, string _role, string _program, string _status, string _email, string _ext, string _phone, string _office, string _officehr, string _bio)
        {
            using (objDoc)
            {

                var objUpDoc = objDoc.doctors.Single(x => x.Id == _id);
                objUpDoc.fname = _fname;
                objUpDoc.lname = _lname;
                objUpDoc.title = _title;
                objUpDoc.department = _department;
                objUpDoc.role = _role;
                objUpDoc.program = _program;
                objUpDoc.status = _status;
                objUpDoc.email = _email;
                objUpDoc.extension = _ext;
                objUpDoc.phone = _phone;
                objUpDoc.office = _office;
                objUpDoc.office_hr = _officehr;
                objUpDoc.bio = _bio;

                objDoc.SubmitChanges();
                return true;

            }
        }

        public bool commitDelete(int _id)
        {

            using (objDoc)
            {

                var objDelDoc = objDoc.doctors.Single(x => x.Id == _id);

                objDoc.doctors.DeleteOnSubmit(objDelDoc);
                objDoc.SubmitChanges();
                return true;

            }
        }
    }
}


//[Team2]Temiskaming-Hospital website Dedesign Project @ Humber college
//Feature: Find a doctor
//Author: Jeesoo Kim
// Feb 17, 2015