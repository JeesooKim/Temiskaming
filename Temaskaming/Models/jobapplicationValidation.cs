using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

//job application model used for validation
namespace Temiskaming.Models
{
    [MetadataType(typeof(jobapplicationValidation))]
    public partial class jobapplication
    {
    }

    [Bind(Exclude = "Id")]
    public class jobapplicationValidation
    {
        //parameters for job title + validation for empty field
        [DisplayName("Job Title")]
        [Required(ErrorMessage = "Enter job title")]
        public string jobtitle { get; set; }

        //parameters for name + validation for empty field
        [DisplayName("Name")]
        [Required(ErrorMessage = "Enter your name")]
        public string aname { get; set; }

        //parameters for phone + validation for empty field and regular expression for phone number
        [DisplayName("Phone Number")]
        [RegularExpression("^\\(?([0-9]{3})\\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Enter a valid Email")]
        [Required(ErrorMessage = "Enter a phone number (416)-999-9999")]
        public string aphone { get; set; }

        //parameters for email + validation for empty field and regular expression for email
        [DisplayName("Email")]
        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid Email")]
        public string aemail { get; set; }

        //parameters for resume + validation for empty field
        [DisplayName("Resume")]
        [Required(ErrorMessage = "Submit resume")]
        public string aresume { get; set; }

        //parameters for coverletter + validation for empty field
        [DisplayName("Coverletter")]
        [Required(ErrorMessage = "Enter coverletter")]
        public string acoverletter { get; set; }

        //parameters for notes
        [DisplayName("Notes")]
        public string anotes { get; set; }
    }
}