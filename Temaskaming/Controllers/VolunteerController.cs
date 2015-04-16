using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class VolunteerController : Controller
    {       
        //
        // GET: /volunteer/

        //instantiating an object in the volunteersClass, which has methods to get the data from db
        volunteerClass objVol = new volunteerClass();

        public ActionResult Index()
        {
            //ViewBag.Group = "JoinOurTeam";

            //linq query to select the opportunities
            var opportunities = from o in objVol.getOpportunites()
                                select o;
            return View(opportunities);
        }

        public ActionResult Form()
        {//Reference: Course Material - Week 5 

            //ViewBag.Group = "JoinOurTeam";

            var provList = new SelectList(new[] { 
                "AB", "BC","MB", "NB","NL","NS","NT","NU","ON","PE", "QC","SK","YT",""});            

            ViewBag.provList = provList;
            return View();
        }

        public ActionResult Thanks()
        {
            //ViewBag.Group = "JoinOurTeam";
            return RedirectToAction("Form");
        }

        [HttpPost]
        public ActionResult Thanks(volnunteerValidation valid)
        {
            //ViewBag.Group = "JoinOurTeam";

            if (ModelState.IsValid)
            {
                return PartialView(valid);
            }
            else
            {
                return PartialView();
            }
        }

        //-----------Public Login ---------//
        //login page
        //after login page to request schedule change similar to  volunteer update
        //logout page


        public ActionResult Schedule()
        {
            return View();
        }

        //----------------------------- Admin -------------------------//
        //---------------------opportunities ----------------------//

        public ActionResult VolunteerAdmin_Index()
        {
            //ViewBag.Group="Join Our Team";

            //linq query to select the opportunities
            var opportunities = from o in objVol.getOpportunites()
                                select o;
            return View(opportunities);
        }

        public ActionResult _Admin_oppDetails(int id)
        {//parameter id: opportunity ID
            //ViewBag.Group="Admin";

            var opportunity = objVol.getOpportunityById(id);

            if (opportunity == null)
            {
                return PartialView("NotFound");
            }
            else
            {
                return PartialView(opportunity);
            }
        }

        public ActionResult Admin_oppDetails(int id)
        {//parameter id: opportunity ID
            //ViewBag.Group="Admin";

            var opportunity = objVol.getOpportunityById(id);

            if (opportunity == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(opportunity);
            }
        }

        //============ opportunities RIUD ==================//       

        public ActionResult Admin_oppInsert()
        {
            //ViewBag.Group="Admin";

            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //http://www.asp.net/mvc/overview/getting-started/introduction/examining-the-edit-methods-and-edit-view
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin_oppInsert([Bind(Include = "o_name, o_regular, o_date, o_day, o_start, o_end, o_location, o_description")] volunteer_opportunity o)       
        {
            //ViewBag.Group="Admin";

            if (ModelState.IsValid)
            {
                try
                {
                    objVol.commitInsertO(o);
                    return RedirectToAction("VolunteerAdmin_Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //--------------------UPDATE (opportunities )--------------------
        public ActionResult Admin_oppUpdate(int id)
        {
            //ViewBag.Group="Admin";
            var opp = objVol.getOpportunityById(id);
            if (opp == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(opp);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin_oppUpdate([Bind(Include = "o_name, o_regular,  o_date, o_day, o_start, o_end, o_location, o_description")] int id, volunteer_opportunity opp)
        {
            //ViewBag.Group="Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objVol.commitUpdateO(id, opp.o_name, opp.o_regular, opp.o_date, opp.o_start, opp.o_end, opp.o_day, opp.o_location, opp.o_description);

                    return RedirectToAction("Admin_oppDetails/" + id);
                    
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        //-----------------------DELETE(opportunities )-------------------------
        public ActionResult Admin_oppDelete(int id)
        {
            //ViewBag.Group="Admin";
            var d = objVol.getOpportunityById(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(d);
            }
        }

        [HttpPost]
        public ActionResult Admin_oppDelete(int id, volunteer_opportunity opp)
        {
            try
            {
                objVol.commitDeleteO(id);
                return RedirectToAction("VolutneerAdmin_Index");
            }
            catch
            {
                return View();
            }
        }


        //========================== volunteers ==============================//

        public ActionResult VolunteerAdmin_volIndex()
        {
            //ViewBag.Group="Join Our Team";

            //linq query to select the opportunities
            var volunteers = from v in objVol.getVolunteers()
                             select v;
            return View(volunteers);
        }

        public ActionResult _Admin_volDetails(int id)
        {//parameter id: volunteer ID
            //ViewBag.Group="Admin";

            var volunteer = objVol.getVolunteerById(id);

            if (volunteer == null)
            {
                return PartialView("NotFound");
            }
            else
            {
                return PartialView(volunteer);
            }
        }

        public ActionResult Admin_volDetails(int id)
        {//parameter id: volunteer ID
            //ViewBag.Group="Admin";

            var volunteer = objVol.getVolunteerById(id);

            if (volunteer == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(volunteer);
            }
        }

        //============ volunteers RIUD ==================//



        public ActionResult Admin_volInsert()
        {
            //ViewBag.Group="Admin";

            IEnumerable<SelectListItem> items =
            objVol.getOpportunites().Select(o =>
                   new SelectListItem
                   {
                       Value = o.o_id.ToString(),
                       Text = o.o_name
                   });

            ViewBag.OpportunityID = items;

            return View();
        }

        [HttpPost]        
        public ActionResult Admin_volInsert(volunteer v)
        {
            //ViewBag.Group="Admin";

            if (ModelState.IsValid)
            {
                try
                {
                    objVol.commitInsertV(v);
                    return RedirectToAction("VolunteerAdmin_volIndex");
                }
                catch
                {
                    return View();
                }
            }

            IEnumerable<SelectListItem> items =
            objVol.getOpportunites().Select(o =>
                   new SelectListItem
                   {
                       Value = o.o_id.ToString(),
                       Text = o.o_name
                   });

            ViewBag.OpportunityID = items;

            return View();
        }

        public ActionResult Admin_volUpdate(int id)
        {
            //ViewBag.Group="Admin";

            IEnumerable<SelectListItem> items =
            objVol.getOpportunites().Select(o =>
                   new SelectListItem
                   {
                       Value = o.o_id.ToString(),
                       Text = o.o_name
                   });

            ViewBag.OpportunityID = items;

            var vol = objVol.getVolunteerById(id);
            if (vol == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(vol);
            }
        }

        [HttpPost]
        public ActionResult Admin_volUpdate(int id, volunteer vol)
        {
            //ViewBag.Group="Admin";
            if (ModelState.IsValid)
            {
                try
                {
                    objVol.commitUpdateV(id, vol.v_fname, vol.v_lname, vol.v_address, vol.v_city, vol.v_province, vol.v_postalCode, vol.v_phone, vol.v_email, vol.v_status, vol.v_schedule, vol.v_oppId);
                    
                    return RedirectToAction("Admin_volDetails/" + id);                    
                }
                catch
                {
                    return View();
                }
            }

            IEnumerable<SelectListItem> items =
            objVol.getOpportunites().Select(o =>
                   new SelectListItem
                   {
                       Value = o.o_id.ToString(),
                       Text = o.o_name
                   });

            ViewBag.OpportunityID = items.ToList();

            return View();
        }


        public ActionResult Admin_volDelete(int id)
        {
            //ViewBag.Group="Admin";
            var d = objVol.getVolunteerById(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(d);
            }
        }

        [HttpPost]
        public ActionResult Admin_volDelete(int id, volunteer vol)
        {
            //ViewBag.Group="Admin";
            try
            {
                objVol.commitDeleteV(id);
                return RedirectToAction("VolunteerAdmin_volIndex");
            }
            catch
            {
                return View();
            }
        }
    }
    //[Team2]Temiskaming-Hospital website Redesign project @ Humber College
    //Feature: Volunteer - Controller
    //File : VolunteerController.cs
    //Author: Jeesoo Kim
    //Created: April 6, 2015
}

