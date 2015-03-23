using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Temiskaming.Controllers
{
    public class ChatController : Controller
    {
        //
        // GET: /Chat/

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Chat()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Chat(string email)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Email = email;
                return PartialView();
            }
            else
            {
                return Index();
            }
        }

        public ActionResult Send()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Send(string message)
        {
            if (ModelState.IsValid)
            {
                return PartialView();
            }
            else
            {
                return PartialView();
            }
            
        }

        public ActionResult Exit()
        {
            return RedirectToAction("Index");
        }

    }
}
