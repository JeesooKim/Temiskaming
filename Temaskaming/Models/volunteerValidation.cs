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

   [MetadataType(typeof(opportunityValidation))]
    public partial class opportunity
    {
    }

    [MetadataType(typeof(volnunteerValidation))]
    public partial class volunteer
    {
    }



    [Bind(Exclude = "o_id")]
    public partial class opportunityValidation
    {
        [DisplayName("Opportunity Name")]
        [Required(ErrorMessage = "Please enter opportunity name")]
        public string o_name { get; set; }        

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Schedule Date")]
        public DateTime? o_date { get; set; }

        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:h:mm TT}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public TimeSpan? o_start { get; set; }

        //[DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:h:mm TT}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Time")]
        public TimeSpan? o_end { get; set; }
    }

    //Ref: http://stackoverflow.com/questions/17434935/jquery-time-picker-and-mvc-4-validation-the-field-must-be-a-date


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
        [Required(ErrorMessage = "Please enter Postal code")]
        [RegularExpression("[ABCEGHJKLMNPRSTVXY][0-9][ABCEGHJKLMNPRSTVWXYZ] ?[0-9][ABCEGHJKLMNPRSTVWXYZ][0-9]", ErrorMessage = "Please enter a VALID Canadian postal code")]
        public string v_postalCode { get; set; }

        [DisplayName("Phone Number")]
        [Required(ErrorMessage = "Please enter Phone Number")]
        public string v_phone { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please enter Email Address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a VALID email")]
        public string v_email { get; set; }

        //[DisplayName("Status")]
        //[Required(ErrorMessage = "Please choose status")]        
        //public string v_status { get; set; }

        [DisplayName("Schedule")]
        [Required(ErrorMessage = "Please choose the Available Day of the week")]        
        public string v_schedule { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password (4 digits)")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Please enter 4 digits")]
        public string v_password { get; set; }

        [Key]
        [ForeignKey("volunteer_opportunity")]
        [DisplayName("Volunteer Opportunity Id")]
        [Required(ErrorMessage = "Please choose Interested Opportunity")]
        public int v_oppId { get; set; }
    }

    


    //[Team2]Temiskaming-Hospital website Redesign project @ Humber College
    //Feature: Volunteer - Model
    //File : volunteerValidation.cs
    //Author: Jeesoo Kim
    //Created: April 6, 2015
}