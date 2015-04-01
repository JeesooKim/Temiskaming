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

        public bool writeChat(string message, string logpath)
        {
            var stree = File.AppendText(logpath);
            stree.WriteLine(message);
            stree.Close();
            return true;
        }
    }

    public class chatSendModel
    {
        [Required]
        public string message { get; set; }
    }

    public class chatModel
    {
        [Required]
        public string email { get; set; }

    }
}