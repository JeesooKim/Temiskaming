using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Temiskaming.Models
{
    public class chatClass
    {
        databaseDataContext objChat = new databaseDataContext();
        
        public bool makeChat()
        {
            using (objChat)
            {

                return true;
            }
        }
    }

    public class chatModel
    {
        [Required]
        public string email { get; set; }

    }
}