using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class FAQController : Controller
    {

        FAQClass objFAQ = new FAQClass();




        public ActionResult Index(string searchString)
        {
          
         
            

            //REFERENCE http://stackoverflow.com/questions/5374481/like-operator-in-linq
            //how to search within the table if the question contains these value narrow it down 
            var Fa = from q in objFAQ.getFAQ() select q; 

            if(!string.IsNullOrEmpty(searchString))
            {
                Fa = Fa.Where(q => q.Question.Contains(searchString));
            }
            return View(Fa);
        }


        //Admin page , will hold links to insert, update, delete
        [Authorize(Roles = "Admin")]
        public ActionResult FAQAdmin()
        {
            ViewBag.Group = "FAQ";
            var Fa = objFAQ.getFAQ();
            if (Fa == null)
            {
                return View("FAQAdmin");
            }
            else
            {
                return View(Fa);
            }
        }


        //insert
        [Authorize(Roles = "Admin")]
        public ActionResult FAQInsert()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult FAQInsert(FAQ fa)
        {

            ViewBag.Group = "FAQ";

            if (ModelState.IsValid)
            {
                try
                {
                    objFAQ.commitInsert(fa);
                    return RedirectToAction("FAQAdmin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        //end of insert

        //update 
        [Authorize(Roles = "Admin")]
        public ActionResult FAQUpdate(int id)
        {
            var fa = objFAQ.getFAQByID(id);
            if (fa == null)
            {
                return View("Not Found");
            }
            else
            {
                return View(fa);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult FAQUpdate(int id, FAQ fa)
        {

            ViewBag.Group = "FAQ";

            if (ModelState.IsValid)
            {
                try
                {
                    objFAQ.commitUpdate(id, fa.Question, fa.Answer);
                    return RedirectToAction("FAQAdmin");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        //end of updatw

        //start delete
        [Authorize(Roles = "Admin")]
        public ActionResult FAQDelete(int id)
        {

            var fa = objFAQ.getFAQByID(id);
            if (fa == null)
            {
                return View("FAQAdmin");
            }
            else
            {
                return View(fa);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult FAQDelete(int id, FAQ fa)
        {

            ViewBag.Group = "FAQ";
            try
            {
                objFAQ.commitDelete(id);
                return RedirectToAction("FAQAdmin");
            }
            catch
            {
                return View();
            }
        }
        //end of delete

    }
}