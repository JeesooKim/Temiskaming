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
            //using (objChat)
            //{
                
            //    return true;
            //}
            return true;
        }
    }

    public class chatModel
    {
        [Required]
        public string email { get; set; }

    }
}