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
                string fileString = fileName.Replace(" ", "").Replace("@", "_").Replace("/", "_").Replace(":", "_");
                Session["log"] = fileString;
                string filePath = Server.MapPath("~/chatLogs/" + fileString + ".txt");
                objChat.makeChat(Session["email"].ToString(), fileString, date, filePath);
                string firstLine = date.ToString("yyyy-MM-dd hh:mm:ss tt") + Session["email"].ToString() + " has entered chat.";
                objChat.writeChat(firstLine, filePath);
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
                ViewBag.FileName = "error";
                return PartialView();
            }
        }

        public ActionResult Send(chatSendModel model)
        {
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Send(string message, string file, chatSendModel model)
        {
            if (ModelState.IsValid)
            {
                var date = DateTime.Now;
                string lineToWrite = date.ToString("hh:mm:ss tt") + " (" + Session["email"] + ") : " + message + "<br />";
                string fileString = file;
                var path = Server.MapPath("~/chatLogs/" + fileString + ".txt");
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
            string lineToWrite = " (" + Session["email"] + ") has left chat <br />";
            var path = Server.MapPath("~/chatLogs/" + Session["log"] + ".txt");
            objChat.writeChat(lineToWrite, path);
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult nChat()
        {
            ViewBag.Group = "Nurse";
            var chats = objChat.getWaitingChats();
            return View(chats);
        }

        public ActionResult ShowChat(int id)
        {
            var chat = objChat.getChat(id);
            return PartialView(chat);
        }



        public ActionResult nSend(string file, chatSendModel model)
        {
            ViewBag.File = file;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult nSend(string message, string file, chatSendModel model)
        {
            if (ModelState.IsValid)
            {
                var date = DateTime.Now;
                string lineToWrite = "<!--" + date.ToString("yyyy-MM-dd hh:mm:ss tt") + "-->" + " (" +User.Identity.Name+ "NURSE" + ") : " + message + "<br />";
                string fileString = file;
                var path = Server.MapPath("~/chatLogs/" + fileString + ".txt");
                objChat.writeChat(lineToWrite, path);
                return PartialView();
            }
            else
            {
                return PartialView();
            }
        }

        public ActionResult nExit(int id, string file)
        {
            string lineToWrite = User.Identity.Name + " has left chat. <br /> ::CHATEND";
            string fileString = file;
            var path = Server.MapPath("~/chatLogs/" + fileString + ".txt");
            objChat.writeChat(lineToWrite, path);
            objChat.closeChat(id);
            return RedirectToAction("nChat");
        }
    }
}