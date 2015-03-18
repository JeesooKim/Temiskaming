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
    public class alertValidation
    {

        public int alertId;

        public string alertTitle;

        public string alertDescription;

        public string alertLevel;

        public DateTime alertTimeline;

        public bool alertStatus;

    }
}