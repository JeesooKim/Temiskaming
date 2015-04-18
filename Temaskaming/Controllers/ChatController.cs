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
            /*
             * Check to see if user session is active *
             * If session active, go to chat partial view and associated chat
             * else return email login partialview *
             * 
             */
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
            /*
             * If session active, show chat
             * else show login
             * */
            if (Session["email"] != null)
            {
                return PartialView();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        [HttpPost]
        public ActionResult Chat(chatModel modelVal)
        {
            if (ModelState.IsValid)
            {
                /*
                 * Start session using email *
                 * grab current time and date, join with email to make unique filename *
                 * create chat log file *
                 * create first line in chat, indicating user has entered chat *
                 * return as partial view *
                 * 
                 */
                Session["email"] = modelVal.email;
                DateTime date = DateTime.Now;
                string fileName = date.ToString() + Session["email"].ToString();
                string fileString = fileName.Replace(" ", "").Replace("@", "_").Replace("/", "_").Replace(":", "_");
                Session["log"] = fileString;
                string filePath = Server.MapPath("~/chatLogs/" + fileString + ".txt");
                objChat.makeChat(Session["email"].ToString(), fileString, date, filePath);
                string firstLine = date.ToString("yyyy-MM-dd hh:mm:ss tt") + " <em><b>(" + Session["email"].ToString() + ")</b> " + " has entered chat.</em><br />";
                objChat.writeChat(firstLine, filePath);
                return PartialView();
            }
            else
            {
                /*
                 * If validation fails, return to index
                 * */
                return Index();
            }
        }

        public ActionResult Display(string file)
        {
            /*
             * Displays the chat log on load
             * */
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

        public ActionResult Send()
        {
            /*
             * Partial view used to send messages
             * */
            return PartialView();
        }

        [HttpPost]
        public ActionResult Send(string message, string file, chatSendModel model)
        {
            /*
             * If message isn't empty, write into log file with appropriate date and user tags
             * */
            if (ModelState.IsValid)
            {
                var date = DateTime.Now;
                string lineToWrite = "<!--" + date.ToString("hh:mm:ss tt") + "--> <span class='umessage'>(" + Session["email"] + ")</span> : " + message + "<br />";
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

        public ActionResult Exit(string file)
        {
            /*
             * Upon exiting chat, write out exit message in chat and abandon session
             * */
            string lineToWrite = " <em>(" + Session["email"] + ") has left chat</em> <br />";
            var path = Server.MapPath("~/chatLogs/" + file + ".txt");
            objChat.writeChat(lineToWrite, path);
            Session.Abandon();
            return PartialView();
        }

        [Authorize(Roles="Nurse")]
        public ActionResult nChat()
        {
            /*
             * Upon page load, grab all chats not yet closed by nurse
             * */
            ViewBag.Group = "Nurse";
            var chats = objChat.getWaitingChats();
            return View(chats);
        }

        public ActionResult ShowChat(int id)
        {
            /*
             * Upon nurse entering chat, sends message to chat room.
             * */
            var chat = objChat.getChat(id);
            DateTime date = DateTime.Now;
            string lineToWrite = date.ToString("yyyy-MM-dd hh:mm:ss tt") + " <em><b>(" + User.Identity.Name + ")</b> " + " has entered chat.</em><br />";
            var path = Server.MapPath("~/chatLogs/" + chat.log_file + ".txt");
            objChat.writeChat(lineToWrite, path);
            return PartialView(chat);
        }



        public ActionResult nSend(string file)
        {
            /*
             * Sets up nurse's send field
             * */
            ViewBag.File = file;
            return PartialView();
        }

        [HttpPost]
        public ActionResult nSend(string message, string file, chatSendModel model)
        {
            /*
             * IF message is not empty string, send to chat room
             * */
            
            if (ModelState.IsValid)
            {
                var date = DateTime.Now;
                string lineToWrite = "<!--" + date.ToString("yyyy-MM-dd hh:mm:ss tt") + "--> <span class='nmessage'>(" + User.Identity.Name + ")</span> : " + message + "<br />";
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
            /*
             * Upon exitting chat, write exit line, and close chat by updating database Nurse column
             * */
            string lineToWrite = "<em><b>(" + User.Identity.Name + ")</b> has left chat.</em> <br />";
            string fileString = file;
            var path = Server.MapPath("~/chatLogs/" + fileString + ".txt");
            objChat.writeChat(lineToWrite, path);
            objChat.closeChat(id);
            return RedirectToAction("nChat");
        }


        [Authorize(Roles="Admin")]
        public ActionResult chatAdmin()
        {
            /*
             * Grabs all chats that been conducted
             * */
            ViewBag.Group = "Admin";
            var chats = objChat.getAllChats();
            return View(chats);
        }

        [Authorize(Roles="Admin")]
        public ActionResult Delete(int id, string file)
        {
            /*
             * Deletes specific chat file and information from database
             * 
             * */
            var path = Server.MapPath("~/chatLogs/" + file + ".txt");
            objChat.deleteChat(id, path);
            return RedirectToAction("chatAdmin");
        }
    }
}