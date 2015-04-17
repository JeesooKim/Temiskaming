using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{
    public class volunteerClass
    {   
        //initiating an instance of Linq object(objVolunteers) to bring the data into this class(or this feature)
        databaseDataContext objVolunteers = new databaseDataContext();

        //******************* Opportunities ********************//
        //for the linq object initiated above, data in the 'opportunites' table is to be used

        //method: get the list of opportunities
        public IQueryable<volunteer_opportunity> getOpportunites()
        {
            var allOpportunites = objVolunteers.volunteer_opportunities.Select(x => x);
            return allOpportunites;
        }
        
        //method: Get One opportunity (for Detail) by its ID (o_id)
        public volunteer_opportunity getOpportunityById(int _id)
        {
            var anOpportunity = objVolunteers.volunteer_opportunities.SingleOrDefault(x => x.o_id == _id);
            return anOpportunity;
        }

        //method: Insert a New Opportunity
        public bool commitInsertO(volunteer_opportunity o)
        {
            using (objVolunteers)
            {
                objVolunteers.volunteer_opportunities.InsertOnSubmit(o);
                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //method: Update an opportunity selected
        //public bool commitUpdateO(int _id, string _name, DateTime _date, TimeSpan _start, TimeSpan _end, string _day, string _location, string _description)
        public bool commitUpdateO(int _id, string _name, bool _regular, DateTime? _date, TimeSpan? _start, TimeSpan? _end, string _day, string _location, string _description)
        {
            using(objVolunteers){
                var objUpOpp = objVolunteers.volunteer_opportunities.SingleOrDefault(x => x.o_id == _id);
                objUpOpp.o_name = _name;
                objUpOpp.o_regular = _regular;   //check if the opportunity is regular(weekly)-based
                objUpOpp.o_date = _date;
                objUpOpp.o_start = _start;
                objUpOpp.o_end = _end;
                objUpOpp.o_day = _day;
                objUpOpp.o_location = _location;
                objUpOpp.o_description = _description;

                objVolunteers.SubmitChanges();
                return true;
            }                        
        }

        //Method: Delete an opportunity selected
        public bool commitDeleteO(int _id)
        {
            using (objVolunteers)
            {
                var objDelOpp = objVolunteers.volunteer_opportunities.Single(x => x.o_id == _id);
                objVolunteers.volunteer_opportunities.DeleteOnSubmit(objDelOpp);
                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //***************************** Volunteers ********************************//
        //for the linq object initiated above, data in the 'volunteers' table is to be used

        //method: get the list of volunteers
        public IQueryable<volunteer> getVolunteers()
        {
            var allVolunteers = objVolunteers.volunteers.Select(x => x);
            return allVolunteers;
        }

        //method: Get One volunteer (for Detail) by its ID (v_id)
        public volunteer getVolunteerById(int _id)
        {
            var aVolunteer = objVolunteers.volunteers.SingleOrDefault(x => x.v_id == _id);
            return aVolunteer;
        }

        //method: Get volunteers for a certain Opportunity (o_id)
        public volunteer getVolunteerByOppId(int _id)
        {
            var volunteers = objVolunteers.volunteers.SingleOrDefault(x=>x.v_oppId == _id);
            return volunteers;
        }


        //*********************** Volunteer Sign Up/Sign In ****************************************//
        //Ref1: http://geekswithblogs.net/dotNETvinz/archive/2011/06/03/asp.net-mvc-3-creating-a-simple-sign-up-form.aspx
        //Ref2:  http://geekswithblogs.net/dotNETvinz/archive/2011/12/30/asp.net-mvc-3---creating-a-simple-log-in-form.aspx

        public void Add(volunteer user)
        {//same method as commitInsertV
            using (objVolunteers)
            {
                user.v_status = "Under Review";
                objVolunteers.volunteers.InsertOnSubmit(user);
                objVolunteers.SubmitChanges();
            }
        }

        public bool IsUserLoginIDExist(string userLogIn)
        {
            return (from v in objVolunteers.volunteers where v.v_email == userLogIn select v).Any();
        }
        //check if user login ID, here EMAIL, does exist
        ////////////////////

        public string GetUserPassword(string userInput)
        {
            var user = from v in objVolunteers.volunteers where v.v_email == userInput select v;
            if (user.ToList().Count > 0)
                return user.First().v_password;
            else
                return string.Empty;
        }

        //method: Update by a volunteer
        public bool commitUpdateProfileV(int _id, string _fname, string _lname, string _address, string _city, string _prov, string _postal, string _phone, string _email, string _schedule, int _oppId)
        {
            using (objVolunteers)
            {
                var objUpVol = objVolunteers.volunteers.SingleOrDefault(x => x.v_id == _id);
                objUpVol.v_fname = _fname;
                objUpVol.v_lname = _lname;
                objUpVol.v_address = _address;
                objUpVol.v_city = _city;
                objUpVol.v_province = _prov;
                objUpVol.v_postalCode = _postal;
                objUpVol.v_phone = _phone;
                objUpVol.v_email = _email;
                objUpVol.v_schedule = _schedule;
                objUpVol.v_oppId = _oppId;

                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //******************************************************************************//


        //method: Insert a New Volunteer
        public bool commitInsertV(volunteer v)
        {
            using (objVolunteers)
            {
                objVolunteers.volunteers.InsertOnSubmit(v);
                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //method: Update a volunteer selected
        public bool commitUpdateV(int _id, string _fname, string _lname, string _address,  string _city, string _prov, string _postal, string _phone, string _email,string _status, string _schedule, int _oppId)
        {
            using (objVolunteers)
            {
                var objUpVol = objVolunteers.volunteers.SingleOrDefault(x => x.v_id == _id);
                objUpVol.v_fname = _fname;
                objUpVol.v_lname = _lname;
                objUpVol.v_address = _address;
                objUpVol.v_city = _city;
                objUpVol.v_province = _prov;
                objUpVol.v_postalCode = _postal;
                objUpVol.v_phone = _phone;
                objUpVol.v_email = _email;
                objUpVol.v_status = _status;
                objUpVol.v_schedule = _schedule;
                objUpVol.v_oppId = _oppId;

                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //Method: Delete an opportunity selected
        public bool commitDeleteV(int _id)
        {
            using (objVolunteers)
            {
                var objDelVol = objVolunteers.volunteers.Single(x => x.v_id == _id);
                objVolunteers.volunteers.DeleteOnSubmit(objDelVol);
                objVolunteers.SubmitChanges();
                return true;
            }
        }

       
    }
    //[Team2]Temiskaming-Hospital website Redesign project @ Humber College
    //Feature: Volunteer - Model
    //File : volunteerClass.cs
    //Author: Jeesoo Kim
    //Created: April 6, 2015
}