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
        public string question;

        [DisplayName("Option 1")]
        [Required(ErrorMessage = "Please enter first option")]
        public string option1;

        [DisplayName("Option 2")]
        [Required(ErrorMessage = "Please enter second option")]
        public string option2;

        public int choice1;

        public int choice2;

        public bool published;
    
    }
}