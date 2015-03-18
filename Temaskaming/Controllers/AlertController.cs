using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class AlertController : Controller
    {
        // properties
        alertClass alertObj = new alertClass();

        //
        // GET: /Alert/

        public ActionResult Index()
        {
            var allAlerts = alertObj.getAllAlerts();
            if (allAlerts == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(allAlerts);
            }

        }

    }
}
