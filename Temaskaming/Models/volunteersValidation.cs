using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Temiskaming.Models
{//volunteers.Models => Temiskaming.Models

    [MetadataType(typeof(opportunityValidation))]
    public partial class opportunity
    {
    }

    [MetadataType(typeof(volnunteerValidation))]
    public partial class volunteer
    {
    }

    [MetadataType(typeof(scheduleValidation))]
    public partial class schedule
    {
    }


    [Bind(Exclude = "o_id")]
    public partial class opportunityValidation
    {
        [DisplayName("Opportunity Name")]
        [Required(ErrorMessage = "Please enter opportunity name")]
        public string o_name { get; set; }


    }

    [Bind(Exclude = "v_id")]
    public partial class volnunteerValidation
    {
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
        [Required(ErrorMessage = "Please enter the first name")]
        public string v_postalCode { get; set; }

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Please enter the first name")]
        public string v_phone { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string v_email { get; set; }


    }

    [Bind(Exclude = "s_id")]
    public partial class scheduleValidation
    {
        [DisplayName("Schedule Status")]
        [Required(ErrorMessage = "Please choose the status of the schedule")]
        public string s_status { get; set; }
    }


    //[Team2]Temiskaming-Hospital website Redesign project @ Humber College
    //Feature: Volunteers - Model
    //File : volunteersValidation.cs
    //Author: Jeesoo Kim
    //Created: April 6, 2015
}