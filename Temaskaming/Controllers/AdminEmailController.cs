using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Temiskaming.Models;
namespace Temiskaming.Controllers
{
    // controller which inserts, updates, and deletes data email signup table

    public class AdminEmailController : Controller
    {
        linqemailsClass objSignup = new linqemailsClass();

        //action result which shows all email signups
        public ActionResult AdminShowSignups()
        {
            var signups = objSignup.getSignups();
            return View(signups);
        }

    }
}
