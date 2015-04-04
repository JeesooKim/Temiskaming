using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult DonationRev()
        {
            return Index();
        }

        [HttpPost]
        public ActionResult DonationRev(donationsPublic don)
        {
            if (ModelState.IsValid)
            {

                return View();
            }
            else
            {
                return Index();
            }
        }

    }
}
