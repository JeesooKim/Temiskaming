using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class doctorClass
    {

        //class name: doctorClass
        //this doctorClass is to represent doctors in a database. Each instance of a Doctor object will correspond to a row within db (doctors) tables (or linq object), and each property of the directory class will map to a column in the table.`
        //1. [propery] objDoc: an instance of Linq object, which contains every data of doctors 
        //2. [method] IQueryable<doctor> getDoctors() : get the list of doctors thru linq object, objDoc
        //3. [method] doctor getDoctorByID(int _id) : get one doctor by ID        
        //4. [method] bool commitInsert(doctor doc): define commitInsert for department
        //5. [method]bool commitUpdate(int _id, string _fname, string _lname, string _title, string _department, string _role, string _program, string _status, string _email, string _ext, string _phone, string _office, string _officehr, string _bio): define commitUpdate for department
        //6. [method] bool commitDelete(int _id): define commitDelete for department

        //creating an instance of LINQ object: objDoc
        databaseDataContext objDoc = new databaseDataContext();

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
//[Team2]Temiskaming-Hospital Website Redesign Project @ Humber college
//Feature: Find a doctor - Model
//File:doctorClass.cs
//Author: Jeesoo Kim
// Feb 17, 2015