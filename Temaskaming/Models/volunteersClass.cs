using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Temiskaming.Models
{

    public class volunteersClass
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
        public bool commitUpdateO(int _id, string _name, DateTime _date, TimeSpan _start, TimeSpan _end, string _day, string _location, string _description)
        {
            using(objVolunteers){
                var objUpOpp = objVolunteers.volunteer_opportunities.SingleOrDefault(x => x.o_id == _id);
                objUpOpp.o_name = _name;
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
        public volunteer getVolunteertyById(int _id)
        {
            var aVolunteer = objVolunteers.volunteers.SingleOrDefault(x => x.v_id == _id);
            return aVolunteer;
        }

        //method: Get volunteers for a certain Opportunity (o_id)
        public volunteer getVolunteertyByOppId(int _id)
        {
            var volunteers = objVolunteers.volunteers.SingleOrDefault(x=>x.v_opportunityId == _id);
            return volunteers;
        }

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
        public bool commitUpdateV(int _id, string _fname, string _lname, string _address,  string _city, string _prov, string _postal, string _phone, string _email)//, int _oppId)
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
                //objUpVol.v_opportunityId = _oppId;

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

        //************************* Schedule********************************//
        //for the linq object initiated above, data in the 'schedules' table is to be used

        //method: get the list of schedules
        public IQueryable<volunteer_schedule> getSchedules()
        {
            var allSchedules = objVolunteers.volunteer_schedules.Select(x => x);
            return allSchedules;
        }

        //method: get the list of shcedules for a certain opportunity
        public IQueryable<volunteer_schedule> getSchedulesByOpportunity(int _id)
        {
            var schedulesByOpp = from s in objVolunteers.volunteer_schedules
                                      where s.o_id== _id
                                      select s;
            return schedulesByOpp;
        }

        //method: get a schedule for a volunteer
        //parameter is the volunteer_id then the result would be his/her schedule from this method
        public volunteer_schedule getScheduleForVolunteer(int _id)
        {
            var aScheduleForVol = objVolunteers.volunteer_schedules.Single(x => x.s_id == _id);
            return aScheduleForVol;
        }

        //method: Get One schedule (for Detail) by its ID (s_id)
        public volunteer_schedule getScheduleyById(int _id)
        {
            var aSchedule = objVolunteers.volunteer_schedules.SingleOrDefault(x => x.s_id == _id);
            return aSchedule;
        }

        public volunteer_schedule getScheduleyByOppId(int _id)
        {
            var aSchedule = objVolunteers.volunteer_schedules.SingleOrDefault(x => x.o_id == _id);
            return aSchedule;
        }

        public volunteer_schedule getScheduleyByVolId(int _id)
        {
            var aSchedule = objVolunteers.volunteer_schedules.SingleOrDefault(x => x.v_id == _id);
            return aSchedule;
        }

        //method: Insert a New Schedule
        public bool commitInsertS(volunteer_schedule s)
        {
            using (objVolunteers)
            {
                objVolunteers.volunteer_schedules.InsertOnSubmit(s);
                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //method: Update a schedule selected
        //for a selected volunteer, schedule will be updated for the volunteer 
        public bool commitUpdateS(int _id, int _oppId, int _volId, string _status, string _detail, string _day)
        {
            using (objVolunteers)
            {
                var objUpSch = objVolunteers.volunteer_schedules.SingleOrDefault(x => x.s_id == _id);
                objUpSch.o_id = _oppId;
                objUpSch.v_id = _volId;
                objUpSch.s_status = _status;
                objUpSch.s_detail = _detail;
                objUpSch.s_day = _day;

                objVolunteers.SubmitChanges();
                return true;
            }
        }

        //Method: Delete a schedule selected
        public bool commitDeleteS(int _id)
        {
            using (objVolunteers)
            {
                var objDelSch = objVolunteers.volunteer_schedules.Single(x => x.s_id == _id);
                objVolunteers.volunteer_schedules.DeleteOnSubmit(objDelSch);
                objVolunteers.SubmitChanges();
                return true;
            }
        }
    }
    //[Team2]Temiskaming-Hospital website Redesign project @ Humber College
    //Feature: Volunteers - Model
    //File : volunteersClass.cs
    //Author: Jeesoo Kim
    //Created: April 6, 2015
}