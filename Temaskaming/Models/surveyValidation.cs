using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{

    [MetadataType(typeof(surveyValidation))]
    public partial class poll
    {

    }


    [Bind(Exclude = "id")]
    public class surveyValidation
    {
        
        public int id;

        public string question;

        public string option1;

        public string option2;

        public int choice1;

        public int choice2;

        public bool published;
    
    }
}