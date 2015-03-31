using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;

// controller which inserts, updates, and deletes data from tables for images and Ecards
namespace Temiskaming.Controllers
{
    public class AdminEcardController : Controller
    {
        linqecardsClass objEcard = new linqecardsClass();

        // action result for page admin page showing table of Ecards
        public ActionResult AdminShowEcards()
        {
            var card = objEcard.getEcards();
            return View(card);
        }
        // action result which displays the selected Ecard
        public ActionResult Display(int id)
        {
            var card = objEcard.getEcardById(id);
            if (card == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(card);
            }
        }
        // action result which shows the card to be updated
        public ActionResult Update(int id)
        {
            var card = objEcard.getEcardById(id);
            if (card == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(card);
            }
        }
        // action result of post method of update Ecard if the input is valid
        [HttpPost]
        public ActionResult Update(int id, ecard card)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    objEcard.commitUpdate(id, card.sname, card.rname, card.emessage, card.mdate);
                    return RedirectToAction("AdminShowEcards");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
        // action method which deletes Ecard from the database based on id 
        public ActionResult Delete(int id)
        {
            var card = objEcard.getEcardById(id);
            if (card == null)
            {
                return View("NotFound");
            }
            else
            {
                objEcard.commitDelete(id);
                return RedirectToAction("AdminShowEcards");
            }
        }

        //action result which shows all images to be edited
        public ActionResult EditImages()
        {
            var cardimage = objEcard.getEcardImages();
            return View(cardimage);
        }

        //action result which on post inserts the image into the datdabase
        [HttpPost]
        public ActionResult EditImages(HttpPostedFileBase file, ecardimage cardimage)
        {

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                file.SaveAs(path);
                var pathurl = "~/Content/Images/" + fileName;
                cardimage.urlpath = pathurl;
                objEcard.commitInsertImage(cardimage);
                return RedirectToAction("EditImages");
            }
            else
                return RedirectToAction("EditImages");
        }

        //action result which deletes ecard image from database
        public ActionResult DeleteImage(int id)
        {
            var card = objEcard.getEcardImageById(id);
            if (card == null)
            {
                return View("NotFound");
            }
            else
            {
                objEcard.commitImageDelete(id);
                return RedirectToAction("EditImages");
            }
        }

        // action result which displays not found view in case of the error
        public ActionResult NotFound()
        {
            return View();
        }
    }
}
