using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temaskaming.Models
{
    [MetadataType(typeof(jobValidation))]
    public partial class job
    {
    }

    [Bind(Exclude = "Id")]
    public class jobValidation
    {
        [DisplayName("Job Title")]
        [Required(ErrorMessage = "Enter job title")]
        public string jobtitle { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Job description")]
        [Required(ErrorMessage = "Enter job description")]
        public string jobdescr { get; set; }

        //parameters for message textfield + validation for empty field
        [DisplayName("Posted date")]
        [Required(ErrorMessage = "Enter posted date")]
        [DisplayFormat(DataFormatString = "{0:MMMM-dd}", ApplyFormatInEditMode = true)]
        public DateTime posteddate { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Qualifications")]
        [Required(ErrorMessage = "Enter qualifications")]
        public string qualifications { get; set; }

        //parameters for receiver textbox + validation for empty field
        [DisplayName("Duration")]
        [Required(ErrorMessage = "Enter duration")]
        public string duration { get; set; }

    }
}