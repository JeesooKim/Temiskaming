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
                /*
                 * Start session using email
                 * grab current time and date, join with email to make unique filename
                 * create chat log file
                 * 
                 */
                Session["email"] = modelVal.email;
                string date = DateTime.Now.ToString();
                string fileName = date.ToString() + Session["email"].ToString();
                string fileString = fileName.Replace(" ", "").Replace("@","").Replace("/","_").Replace(":",".");
                string filePath = Server.MapPath("~/chatLogs/" + fileString + ".html");
                objChat.makeChat(Session["email"].ToString(), fileString, date, filePath);
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
