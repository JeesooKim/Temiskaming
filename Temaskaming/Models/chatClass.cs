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
        
        public bool makeChat(string email, string logfile, string logdate, string filepath)
        {
            File.Create(filepath);
            using (objChat)
            {

                return true;
            }
        }

        public bool writeChat(string message, string logpath)
        {
            //File.AppendAllText(logpath, message);
            var stree = File.AppendText(logpath);
            stree.WriteLine(message);
            stree.Close();
            return true;
        }
    }

    public class chatModel
    {
        [Required]
        public string email { get; set; }

    }
}