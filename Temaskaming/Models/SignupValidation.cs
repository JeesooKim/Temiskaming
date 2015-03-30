using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(signupValidation))]
    public partial class email_signup
    {
    }

    [Bind(Exclude = "Id")]
    public class signupValidation
    {
        //parameters for sender name textbox + validation for empty field
        [DisplayName("First name:")]
        [Required(ErrorMessage = "Enter your name")]
        public string ename { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Enter your last name")]
        public string elname { get; set; }

        //parameters for message textfield + validation for empty field
        [DisplayName("Your email")]
        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Enter a valid Email")]
        public string eemail { get; set; }
    }
}