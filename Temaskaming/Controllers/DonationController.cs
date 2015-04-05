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
            return View();
        }

        public ActionResult SubmitDonation()
        {
            return Index();
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
                return Index();
            }
        }


        
        public ActionResult IPN()
        {
            var ipnVals = new Dictionary<string, string>();
            ipnVals.Add("cmd", "_notify-validate");
            string url = "https://www.sandbox.paypal.com/cgi-bin/webscr";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            byte[] param = Request.BinaryRead(Request.ContentLength);
            string strReq = Encoding.UTF8.GetString(param);

            StringBuilder sb = new StringBuilder();
            sb.Append(strReq);

            foreach (string key in ipnVals.Keys)
            {
                sb.AppendFormat("&{0}={1}", key, ipnVals[key]);
            }
            strReq += sb.ToString();
            req.ContentLength = strReq.Length;

            string response = "";
            using (StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.UTF8))
            {
                streamOut.Write(strReq);
                streamOut.Close();
                using (StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream()))
                {
                    response = streamIn.ReadToEnd();
                }
            }

            if (response == "VERIFIED")
            {
                string tranID = Request["txn_id"];
                string amount = Request["mc_gross"];
                string donation_id = Request["donation_id"];

                //objDon.verifyDonation(Convert.ToInt32(donation_id));
            }
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

    }
}
