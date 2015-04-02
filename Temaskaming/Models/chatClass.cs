using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Temiskaming.Models
{
    public class chatClass
    {
        databaseDataContext objChat = new databaseDataContext();

        public IQueryable<chat> getAllChats()
        {
            var chats = objChat.chats.Select(x => x).OrderByDescending(x => x.log_date);
            return chats;
        }

        public IQueryable<chat> getWaitingChats()
        {
            var chats = objChat.chats.Where(x => x.nurse == null);
            return chats;
        }

        public chat getChat(int _id)
        {
            var chat = objChat.chats.SingleOrDefault(x => x.id == _id);
            return chat;
        }

        public bool closeChat(int _id)
        {
            var chat = objChat.chats.SingleOrDefault(x => x.id == _id);
            chat.nurse = "NURSE";
            objChat.SubmitChanges();
            return true;
        }

        public bool makeChat(string email, string logfile, DateTime logdate, string filepath)
        {
            File.Create(filepath).Close();
            using (objChat)
            {
                chat newChat = new chat();
                newChat.email = email;
                newChat.log_date = logdate;
                newChat.log_file = logfile;
                objChat.chats.InsertOnSubmit(newChat);
                objChat.SubmitChanges();
                return true;
            }
        }

        public bool deleteChat(int _id, string _filepath)
        {
            File.Delete(_filepath);
            using (objChat)
            {
                var chat = objChat.chats.SingleOrDefault(x => x.id == _id);
                objChat.chats.DeleteOnSubmit(chat);
                objChat.SubmitChanges();
                return true;
            }
        }

        public bool writeChat(string _message, string _logpath)
        {
            var stree = File.AppendText(_logpath);
            stree.WriteLine(_message);
            stree.Close();
            return true;
        }
    }

    public class chatSendModel
    {
        [Required(ErrorMessage= "Please enter a message to send")]
        public string message { get; set; }

    }

    public class chatModel
    {
        [Required(ErrorMessage= "Please enter your email")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage= "Please enter valid email")]
        public string email { get; set; }

    }
}