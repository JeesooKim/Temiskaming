using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(cmsValidation))]
    public partial class navigation
    {

    }

    [Bind(Exclude="id")]
    public class cmsValidation
    {
        [Required]
        public string name { get; set; }

        public string viewpath { get; set; }

        public string controller { get; set; }

        public string group { get; set; }
    }
}