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
    public class volSignInValidation
    {
        public int v_id { get; set; }

        [DisplayName("Email Address")]
        [Required(ErrorMessage = "Please enter email address")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string v_email { get; set; }

        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please Enter Password (4 digits)")]
        [RegularExpression("[0-9]{4}", ErrorMessage = "Please enter 4 digits")]
        public string v_password { get; set; }
    }
}
//[Team2]Temiskaming-Hospital website Redesign project @ Humber College
//Feature: Volunteer - Model
//File : volSignInValidation.cs
//Author: Jeesoo Kim
//Created: April 16, 2015