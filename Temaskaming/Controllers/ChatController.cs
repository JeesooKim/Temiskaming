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
        chatSendModel sendmodel = new chatSendModel();

        public ActionResult Index()
        {
            if (Session["email"] != null)
            {
                return PartialView("Chat");
            }
            else
            {
                ViewBag.Chat = 0;
                return PartialView();
            }
        }

        public ActionResult Chat()
        {
            if (Session["email"] != null)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
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
                DateTime date = DateTime.Now;
                string fileName = date.ToString() + Session["email"].ToString();
                string fileString = fileName.Replace(" ", "").Replace("@","_").Replace("/","_").Replace(":","_");
                Session["log"] = fileString;
                string filePath = Server.MapPath("~/chatLogs/" + fileString + ".txt");
                objChat.makeChat(Session["email"].ToString(), fileString, date, filePath);
                return PartialView();
            }
            else
            {
                return Index();
            }
        }

        public ActionResult Display(string file)
        {
            if (file != "")
            {
                ViewBag.FileName = file;
                return PartialView();
            }
            else
            {
                ViewBag.Error = "SSSSS";
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
                var date = DateTime.Now;
                string lineToWrite = date.ToString("hh:mm:ss tt") + " (" + Session["email"] + ") : " + message + "<br />";
                string fileString = (String)Session["log"];
                var path = Server.MapPath("~/chatLogs/"+ fileString +".txt");
                objChat.writeChat(lineToWrite, path);
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
            return RedirectToAction("Index");
        }

        public ActionResult nChat()
        {
            ViewBag.Group = "Nurse";
            return View();
        }

        public ActionResult nChatPartial()
        {
            return PartialView();
        }

    }
}
