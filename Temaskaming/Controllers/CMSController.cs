using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class CMSController : Controller
    {
        cmsClass objCMS = new cmsClass();

        public PartialViewResult Error()
        {
            return PartialView();
        }

        public PartialViewResult CMS()
        {
            if (Session["ID"] != null)
            {
                var nav = objCMS.getPages();
                return PartialView(nav);
            }
            else
            {
                return PartialView("Error");
            }

        }

        public PartialViewResult Insert()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult Insert(navigation nav)
        {
            return PartialView();
        }

        public PartialViewResult Edit()
        {
            return PartialView();
        }

    }
}
