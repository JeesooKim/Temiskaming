using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(jobapplicationValidation))]
    public partial class jobapplication
    {
    }

    [Bind(Exclude = "Id")]
    public class jobapplicationValidation
    {
        //parameters for sender name textbox + validation for empty field
        [DisplayName("Job Title")]
        [Required(ErrorMessage = "Enter job title")]
        public string jobtitle { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Your name")]
        [Required(ErrorMessage = "Enter your name")]
        public string aname { get; set; }

        //parameters for message textfield + validation for empty field
        [DisplayName("Your phone number")]
        [Required(ErrorMessage = "Enter a phone number (416)-999-9999")]
        public DateTime aphone { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Your email")]
        [Required(ErrorMessage = "Enter your email")]
        public string aemail { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Submit resume")]
        [Required(ErrorMessage = "Submit resume")]
        public string aresume { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Coverletter")]
        [Required(ErrorMessage = "Enter coverletter")]
        public string acoverletter { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Duration")]
        public string anotes { get; set; }
    }
}