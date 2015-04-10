using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace volunteers.Controllers
{//Controller to manage the volunteers menu
    public class VolunteersController : Controller
    {

        //instantiating an object in the volunteersClass, which has methods to get the data from db
        volunteersClass objVol = new volunteersClass();


        //****************PUBLIC *****************//
        public ActionResult Index()
        {
            ViewBag.Group="JoinOurTeam";

            //linq query to select the opportunities
            var opportunities = from o in objVol.getOpportunites()
                                select o;
            return View(opportunities);
        }

        public ActionResult Form()
        {//Reference: Course Material - Week 5 

            ViewBag.Group="JoinOurTeam";

            var provList = new SelectList(new[] { 
                "AB", "BC","MB", "NB","NL","NS","NT","NU","ON","PE", "QC","SK","YT",""});
            //@*AB (Alberta)
            //BC (British Columbia)
            //MB (Manitoba)
            //NB (New Brunswick)
            //NL (Newfoundland and Labrador)
            //NS (Nova Scotia)
            //NT (Northewest Territories)
            //NU (Nunavut)
            //ON (Ontario)
            //PE (Prince Edward Island)
            //QC (Quebec)
            //SK (Saskatchewan)
            //YT (Yukon)

            ViewBag.provList = provList;
            return View();
        }

        public ActionResult Thanks()
        {
            ViewBag.Group = "JoinOurTeam";
            return RedirectToAction("Form");
        }

        [HttpPost]
        public ActionResult Thanks(volnunteerValidation valid)
        {
            ViewBag.Group = "JoinOurTeam";
            if (ModelState.IsValid)
            {
                return PartialView(valid);
            }
            else
            {
                return PartialView();
            }
        }

    }

    //[Team2]Temiskaming-Hospital website Redesign project @ Humber College
    //Feature: Volunteers - Controller
    //File : VolunteersController.cs
    //Author: Jeesoo Kim
    //Created: April 6, 2015
}

