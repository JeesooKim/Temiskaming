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




        public ActionResult Index(string search)
        {

            var Fa = objFAQ.getFAQ();
            return View();
        }

        public ActionResult Informations(int id)
        {
            var fa = objFAQ.getFAQ();
            return View(fa);
        }


        [HttpPost]
        public ActionResult insert(FAQ fa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objFAQ.commitInsert(fa);
                    return RedirectToAction("FAQ");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        public ActionResult update(int id)
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

        [HttpPost]
        public ActionResult update(int id, FAQ fa)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objFAQ.commitUpdate(id, fa.Question, fa.Answer);
                    return RedirectToAction("Informations" + id);
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult delete(int id)
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
        [HttpPost]
        public ActionResult delete(int id, FAQ fa)
        {
            try
            {
                objFAQ.commitDelete(id);
                return RedirectToAction("FAQ");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult NotFound()
        {
            return View();
        }
    }
}