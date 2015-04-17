using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    public class volSignUpValidation
    {
        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please enter Email Address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a VALID email")]
        public string v_email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password (4 digits)")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Please enter 4 digits")]
        public string v_password { get; set; }
        
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter First name")]
        public string v_fname { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter Last name")]
        public string v_lname { get; set; }

        [DisplayName("Address")]
        [Required(ErrorMessage = "Please enter Address")]
        public string v_address { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "Please enter City")]
        public string v_city { get; set; }

        [DisplayName("Province")]
        [Required(ErrorMessage = "Please choose Province")]
        public string v_province { get; set; }

        [DisplayName("Postal Code")]
        [Required(ErrorMessage = "Please enter Postal code")]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "Please enter a VALID Canadian postal code")]
        public string v_postalCode { get; set; }

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Please enter Phone Number")]
        [RegularExpression("\\d{10}", ErrorMessage = "Enter a 10 digit Canadian phone number without space")]
        public string v_phone { get; set; }

        [DisplayName("Schedule")]
        [Required(ErrorMessage = "Please choose the Available Day of the week")]
        public string v_schedule { get; set; }

        [Key]
        [ForeignKey("volunteer_opportunity")]
        [DisplayName("Volunteer Opportunity Id")]
        [Required(ErrorMessage = "Please choose Interested Opportunity")]
        public int v_oppId { get; set; }

    }
}

//[Team2]Temiskaming-Hospital website Redesign project @ Humber College
//Feature: Volunteer - Model
//File : volSignUpValidation.cs
//Author: Jeesoo Kim
//Created: April 17, 2015