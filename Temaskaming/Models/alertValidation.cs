using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(alertValidation))]
    public partial class alert
    {

    }


    [Bind(Exclude = "alertId")]
    public class alertValidation                // Validation Model for Alert model
    {

        public int alertId { get; set; }

        [DisplayName("Alert Title")]
        [Required(ErrorMessage = "Please enter the title")]
        public string alertTitle { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter the description")]
        public string alertDescription { get; set; }

        [DisplayName("Level")]
        [Required(ErrorMessage = "Please enter alert level")]
        public string alertLevel { get; set; }

        [DisplayName("TimeLine")]
        [Required(ErrorMessage = "Please pick a date")]
        public DateTime alertTimeline { get; set; }

        
        public bool alertStatus { get; set; }

    }
}