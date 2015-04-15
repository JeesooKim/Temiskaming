using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class directoryClass
    {
        //class name: directoryClass
        //this directory class is to represent directory in a database. Each instance of a Directory object will correspond to a row within db (directory: departments, staff) tables (or linq object), and each property of the directory class will map to a column in the table.`
        //1. create an instance of Linq object, which contains every data of departments and staff including their repationship
        //Department*****
        //2. method : get the list of departments thru linq object
        //3. method : get one department by ID        
        //4. define commitInsert for department
        //5. define commitUpdate for department
        //6. define commitDelete for department
        //Staff*****
        //7. method : get the list of staff thru linq obj
        //8. method : get one staff by ID
        //9. define commitInsert for staff
        //10.define commitUpdate for staff
        //11.define commitDelete for staff

        //creates an instance of LINQ object:objDirectory
        databaseDataContext objDirectory = new databaseDataContext();

        //***************** Departments ********************//
        //linq object:objDirectory
        //tables: departmets, staffs

        // method: get the list of departments thru linq object
        public IQueryable<department> getDepartments()
        {
            var allDepartments = objDirectory.departments.Select(x => x);
            return allDepartments;
        }

        // method : get one department by ID
        public department getDepartmentByID(int _id)
        {
            var alldepartment = objDirectory.departments.SingleOrDefault(x => x.d_id == _id);
            return alldepartment;
        }

        //method : get the name of the department by its id  
        public IQueryable<string> getDepartmentNameById(int _id)
        {
            var d_name = objDirectory.departments.Where(x=>x.d_id == _id).Select(x=>x.d_name).Distinct();
            return d_name;
        }

        //Insert method with a parameter, department object
        public bool commitInsertD(department d)
        {
            using (objDirectory)
            {
                objDirectory.departments.InsertOnSubmit(d);
                objDirectory.SubmitChanges();
                return true;
            }
        }

        //Update method for department
        public bool commitUpdateD(int _id, string _name, string _phone, string _ext, string _fax)
        {
            using (objDirectory)
            {
                var objUpD = objDirectory.departments.Single(x => x.d_id == _id);                
                objUpD.d_name = _name;
                objUpD.d_phone = _phone;
                objUpD.d_ext = _ext;
                objUpD.d_fax = _fax;

                objDirectory.SubmitChanges();
                return true;
            }
        }

        //Delete department
        public bool commitDeleteD(int _id)
        {
            using (objDirectory)
            {
                var objDelD = objDirectory.departments.Single(x => x.d_id == _id);
                objDirectory.departments.DeleteOnSubmit(objDelD);
                objDirectory.SubmitChanges();
                return true;
            }
        }

        //*********************Staff ********************//
        // method : get the list of staff thru linq obj
        public IQueryable<staff> getStaff()
        {
            var allStaff = objDirectory.staffs.Select(x => x);
            return allStaff;
        }

        // method : get one staff by ID
        public staff getStaffByID(int _id)
        {
            var allstaff = objDirectory.staffs.SingleOrDefault(x => x.staff_id == _id);
            return allstaff;
        }

        //public staff getStaffByDepartmentID(int _id)
        //{//returns all staff who belong to the department with ID(parameter)
        //    var allstaffInD = objDirectory.staffs.SingleOrDefault(x => x.staff_departmentId == _id);
            
        //    return allstaffInD;
        //}

        public IQueryable<staff> getStaffByDepartmentID(int _id)
        {//returns all staff who belong to the department with ID(parameter)
            //var allstaffInD = objDirectory.staffs.SingleOrDefault(x => x.staff_departmentId == _id);
            //var allstaffInD = objDirectory.staffs.Where(x => x.staff_departmentId == _id).Select(x => x);
            var allstaffInD = from s in objDirectory.staffs
                              where s.staff_departmentId == _id
                              orderby s.staff_id
                              select s;

            return allstaffInD;
        }

        
        //Insert method with a parameter, staff object
        public bool commitInsertS(staff s)
        {
            using (objDirectory)
            {

                objDirectory.staffs.InsertOnSubmit(s);                
                objDirectory.SubmitChanges();
                return true;
            }
        }
                

        //Update method for staff
        public bool commitUpdateS(int _id, string _fname, string _lname, string _position, string _phone, string _ext, string _email, string _departmentName, int _departmentId)
        {
            using (objDirectory)
            {
                var objUpS = objDirectory.staffs.Single(x => x.staff_id == _id);
                objUpS.staff_fname = _fname;
                objUpS.staff_lname = _lname;
                objUpS.staff_position = _position;
                objUpS.staff_phone = _phone;
                objUpS.staff_ext = _ext;
                objUpS.staff_email = _email;
                objUpS.staff_departmentName = _departmentName;
                objUpS.staff_departmentId = _departmentId;
                
                objDirectory.SubmitChanges();
                return true;
            }
        }

        //Delete staff
        public bool commitDeleteS(int _id)
        {
            using (objDirectory)
            {
                var objDelStaff = objDirectory.staffs.Single(x => x.staff_id == _id);
                objDirectory.staffs.DeleteOnSubmit(objDelStaff);
                objDirectory.SubmitChanges();
                return true;
            }
        }

        //[Team2]Temiskaming-Hospital website Redesign Project @ Humber College
        //Feature: Directory -Model
        //File: directoryClass.cs
        //Author: Jeesoo Kim
        //March30, 2015
    }
}