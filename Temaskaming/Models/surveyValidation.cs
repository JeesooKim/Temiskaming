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
    public class surveyValidation                   // Validation Model for Survey model
    {
        
        public int id;

        [DisplayName("Question")]
        [Required(ErrorMessage = "Please enter the question")]
        public string question {get; set;}

        [DisplayName("Option 1")]
        [Required(ErrorMessage = "Please enter first option")]
        public string option1 { get; set; }

        [DisplayName("Option 2")]
        [Required(ErrorMessage = "Please enter second option")]
        public string option2 { get; set; }

        public int choice1 { get; set; }

        public int choice2 { get; set; }

        public bool published { get; set; }
    
    }
}