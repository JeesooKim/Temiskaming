using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

//model used for ecard data
namespace Temiskaming.Models
{
    [MetadataType(typeof(ecardValidation))]
    public partial class ecard
    {
    }

    [Bind(Exclude = "Id")]
    public class ecardValidation
    {
        //parameters for sender name textbox + validation for empty field
        [DisplayName("From:")]
        [Required(ErrorMessage = "Enter your name")]
        public string sname { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("To (first name and last name):")]
        [Required(ErrorMessage = "Enter recepient name")]
        public string rname { get; set; }

        //parameters for message textfield + validation for empty field
        [DisplayName("Your message:")]
        [Required(ErrorMessage = "Enter your message")]
        public string emessage { get; set; }

        //parameters for date to be sent on + validation for empty field
        [DisplayName("Date to be sent on:")]
        [DisplayFormat(DataFormatString = "{0:MMMM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Enter a date")]
        public DateTime mdate { get; set; }

        //variable for url used when selecting an image
        public string urlpath { get; set; }
    }
}