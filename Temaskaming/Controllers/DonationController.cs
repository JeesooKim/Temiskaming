using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.IO;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class DonationController : Controller
    {

        donationsDB objDon = new donationsDB();
        donationsPublic viewDon = new donationsPublic();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DonationRev(donationsPublic don)
        {
            if (don.id != null)
            {
                return View();
            }
            else
            {
                return View("Index");
            }
        }

        public ActionResult SubmitDonation()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult SubmitDonation(donationsPublic don)
        {
            if (ModelState.IsValid)
            {
                objDon.makeDonation(don);
                var donation = objDon.getLatestDonation();
                don.id = donation.id;
                return View("DonationRev", don);
            }
            else
            {
                return View("Index");
            }
        }

        [HttpPost]
        public EmptyResult IPN()
        {
            //Instant Payment Notification (IPN) listener, built with reference to http://blog.liamcavanagh.com/2012/06/how-to-use-paypal-with-asp-net-mvc/

            //url to the sandbox test site for paypal. Change this to https://www.paypal.com/cgi-bin/webscr on live sites with actual business account made
            string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";

            //Create httpwebrequest object that will be sent out to paypal
            //Set method to POST, and establish content type
            HttpWebRequest ppreq = (HttpWebRequest)WebRequest.Create(url);
            ppreq.Method = "POST";
            ppreq.ContentType = "application/x-www-form-urlencoded";

            //Read in the POSTed data from paypal, and parse it as ASCII encoded string as per PayPal documentation
            byte[] data = Request.BinaryRead(Request.ContentLength);
            string strReq = Encoding.ASCII.GetString(data);

            //Create string to return to PayPal for verification of notification as per PayPal documentation
            StringBuilder sb = new StringBuilder();
            sb.Append(strReq);
            sb.Append("&cmd=_notify-validate");
            strReq += sb.ToString();
            //The string to be sent out will be placed inside the HTTPWEBREQUEST object created earlier, prepare it
            ppreq.ContentLength = strReq.Length;

            //Send out POST request for verification from PayPal and immediately catch the response from paypal in streamin
            //response will be a string of 'VERIFIED' or 'INVALID'
            string response = "";
            using (StreamWriter streamOut = new StreamWriter(ppreq.GetRequestStream(), Encoding.ASCII))
            {
                streamOut.Write(strReq);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(ppreq.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            //If response is VERIFIED, then donation has been made and we update database using our custom variable that is passed back to us
            if (response == "VERIFIED")
            {
                string donation_id = Request["custom"];

                //objDon.verifyDonation(Convert.ToInt32(donation_id));
                
            }
            return null;
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult donationAdmin()
        {
            ViewBag.Group = "Admin";
            var donations = objDon.getAllDonations();
            return View(donations);
        }

        public ActionResult Delete(int id)
        {
            objDon.deleteDonation(id);
            return RedirectToAction("donationAdmin");
        }
    }
}
