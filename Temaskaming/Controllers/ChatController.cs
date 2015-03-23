using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Temiskaming.Models;

namespace Temiskaming.Controllers
{
    public class ChatController : Controller
    {
        chatClass objChat = new chatClass();
        chatModel model = new chatModel();

        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult Chat()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Chat(chatModel modelVal)
        {
            if (ModelState.IsValid)
            {
                Session["email"] = modelVal.email;
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
            Session.Abandon();
            return PartialView();
        }

    }
}
