using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

// controller which creates new Ecard
namespace Temiskaming.Controllers
{
    public class EcardController : Controller
    {
        linqecardsClass objEcard = new linqecardsClass();

        //action method which displays all images for Ecard to be selected
        public ActionResult SelectImage()
        {
            var cardimage = objEcard.getEcardImages();
            return View(cardimage);
        }

        //action method which shows a form to create Ecard
        public ActionResult CreateEcard(string passurl)
        {
            @ViewBag.valurl = passurl;
            return View();
        }

        //action method which redirects to the first page of creating Ecard if user gets final ecardsend page not through post method
        public ActionResult CardSent()
        {
            return RedirectToAction("CreateEcard");
        }

        //action result which on post inserts new data into Ecard table is the data is valid
        [HttpPost]
        public ActionResult CardSent(ecard valid)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    objEcard.commitInsert(valid);
                    return PartialView(valid);
                }
                catch
                {
                    return View("CreateEcard");
                }
            }
            else
            {
                return View("CreateEcard");
            }
        }
    }
}
