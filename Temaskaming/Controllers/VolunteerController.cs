using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            ViewBag.Group = "JoinOurTeam";

            //linq query to select the opportunities
            var opportunities = from o in objVol.getOpportunites()
                                select o;
            return View(opportunities);
        }

        //************* added on April 17 ***********************//
        //-----------Public Login ---------//
        //login page
        //after login page to request schedule change similar to  volunteer update
        //logout page

        //*********************** Volunteer Sign Up/Sign In ****************************************//
        //Ref1: http://geekswithblogs.net/dotNETvinz/archive/2011/06/03/asp.net-mvc-3-creating-a-simple-sign-up-form.aspx
        //Ref2:  http://geekswithblogs.net/dotNETvinz/archive/2011/12/30/asp.net-mvc-3---creating-a-simple-log-in-form.aspx

        //GET
        public ActionResult SignUp()
        {
            var provList = new SelectList(new[] { 
                "", "AB", "BC","MB", "NB","NL","NS","NT","NU","ON","PE", "QC","SK","YT"});
            ViewBag.provList = provList;

            var availDay = new SelectList(new[]{
                "", "Monday - Friday", "Monday","Tuesday","Wednesday", "Thursday","Friday","Saturday","Sunday"});
            ViewBag.availDay =availDay;

            IEnumerable<SelectListItem> items =
            objVol.getOpportunites().Select(o =>
                   new SelectListItem
                   {
                       Value = o.o_id.ToString(),
                       Text = o.o_name
                   });
            ViewBag.OpportunityID = items;

            return View("SignUp");
        }


        //POST
        [HttpPost]
        public ActionResult SignUp(volunteer v)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //volunteerClass userManager = new volunteerClass();

                    if (!objVol.IsUserLoginIDExist(v.v_email))
                    {
                        objVol.Add(v);
                        FormsAuthentication.SetAuthCookie(v.v_fname, false);
                        //return RedirectToAction("Welcome", "Home");
                        return PartialView("Thanks", v);
                    }
                    else
                    {
                        ModelState.AddModelError("", "LogID already taken");
                    }
                }
                catch
                {
                    return View(v);
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

            return View(v);
        }

        //Sign In//
        //GET
        public ActionResult SignIn()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        //public ActionResult SignIn(UserSignIn model, string returnUrl)
        public ActionResult SignIn(volSignInValidation model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //UserManager userManager = new UserManager();
                volunteerClass vol = new volunteerClass();
                var v = (from vv in vol.getVolunteers()
                         where vv.v_email == model.v_email
                         select vv.v_id).FirstOrDefault();

                string password = objVol.GetUserPassword(model.v_email);

                if (string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "The user login or password provided is incorrect.");
                }

                if (model.v_password == password)
                {
                    FormsAuthentication.SetAuthCookie(model.v_email, false);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        //return RedirectToAction("Admin_volDetails/" + vol.v_id,"Volunteer");

                        return RedirectToAction("Welcome/" + v, "Volunteer");
                        //return RedirectToAction("Welcome/"+ model.v_id, "Volunteer");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [Authorize]
        public ActionResult Welcome(int id)
        {
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


        [Authorize]
        [HttpPost]
        public ActionResult Welcome(int id, volunteer vol)
        {

            //ViewBag.Group="Admin";
            if (ModelState.IsValid)
            {

                try
                {
                    objVol.commitUpdateProfileV(id, vol.v_fname, vol.v_lname, vol.v_address, vol.v_city, vol.v_province, vol.v_postalCode, vol.v_phone, vol.v_email, vol.v_schedule, vol.v_oppId);

                    return RedirectToAction("Welcome/" + id);
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
        
       

        // GET: /Sign Out
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Volunteer");
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
        //-----------------------End of  Public ------------------------------//
        

        //-------------------Below,   Admin  (START) -------------------------//
        //-------------------------opportunities ----------------------//

        [Authorize(Roles = "Admin")]
        public ActionResult VolunteerAdmin_Index()
        {
            ViewBag.Group = "Admin";

            //linq query to select the opportunities
            var opportunities = from o in objVol.getOpportunites()
                                select o;
            return View(opportunities);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _Admin_volList(int _oppId)
        {
            ViewBag.Group = "Admin";

            var vol = from v in objVol.getVolunteers()
                      where v.v_oppId == _oppId                      
                      select v;
            if (vol == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView(vol);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_volList(int _oppId)
        {
            ViewBag.Group = "Admin";

            var vol = from v in objVol.getVolunteers()
                      where v.v_oppId == _oppId
                      orderby v.v_lname
                      select v;
            if (vol == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(vol);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _Admin_oppDetails(int id)
        {//parameter id: opportunity ID

            ViewBag.Group = "Admin";

            var opportunity = objVol.getOpportunityById(id);

            if (opportunity == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView(opportunity);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_oppDetails(int id)
        {//parameter id: opportunity ID

            ViewBag.Group = "Admin";

            var opportunity = objVol.getOpportunityById(id);

            if (opportunity == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(opportunity);
            }
        }

        //============ opportunities RIUD ==================//       
        [Authorize(Roles = "Admin")]
        public ActionResult Admin_oppInsert()
        {
            ViewBag.Group="Admin";

            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //http://www.asp.net/mvc/overview/getting-started/introduction/examining-the-edit-methods-and-edit-view

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin_oppInsert([Bind(Include = "o_name, o_regular, o_date, o_day, o_start, o_end, o_location, o_description")] volunteer_opportunity o)       
        {
            ViewBag.Group="Admin";

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
        [Authorize(Roles = "Admin")]
        public ActionResult Admin_oppUpdate(int id)
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin_oppUpdate([Bind(Include = "o_name, o_regular,  o_date, o_day, o_start, o_end, o_location, o_description")] int id, volunteer_opportunity opp)
        {
            ViewBag.Group="Admin";

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
        [Authorize(Roles = "Admin")]
        public ActionResult Admin_oppDelete(int id)
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Admin_oppDelete(int id, volunteer_opportunity opp)
        {
            ViewBag.Group = "Admin";

            try
            {
                objVol.commitDeleteO(id);
                return RedirectToAction("VolunteerAdmin_Index");
            }
            catch
            {
                return View();
            }
        }


        //========================== volunteers ==============================//
        [Authorize(Roles = "Admin")]
        public ActionResult VolunteerAdmin_volIndex()
        {
            ViewBag.Group = "Admin";

            //linq query to select the opportunities
            var volunteers = from v in objVol.getVolunteers()
                             select v;
            return View(volunteers);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult _Admin_volDetails(int id)
        {//parameter id: volunteer ID
            
            ViewBag.Group="Admin";

            var volunteer = objVol.getVolunteerById(id);

            if (volunteer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return PartialView(volunteer);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_volDetails(int id)
        {//parameter id: volunteer ID
            
            ViewBag.Group="Admin";

            var volunteer = objVol.getVolunteerById(id);

            if (volunteer == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(volunteer);
            }
        }

        //============ volunteers RIUD ==================//


        [Authorize(Roles = "Admin")]
        public ActionResult Admin_volInsert()
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        [HttpPost]        
        public ActionResult Admin_volInsert(volunteer v)
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_volUpdate(int id)
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Admin_volUpdate(int id, volunteer vol)
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        public ActionResult Admin_volDelete(int id)
        {
            ViewBag.Group="Admin";

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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Admin_volDelete(int id, volunteer vol)
        {
            ViewBag.Group="Admin";

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

