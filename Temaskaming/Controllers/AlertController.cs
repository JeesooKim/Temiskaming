using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Temiskaming.Controllers
{
    public class AlertController : Controller
    {
        //
        // GET: /Alert/

        public PartialViewResult Index()
        {
            return PartialView();
        }

    }
}
