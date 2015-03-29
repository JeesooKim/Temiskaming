using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{

    [MetadataType(typeof(ecardimageValidation))]
    public partial class ecardimage
    {
    }

    [Bind(Exclude = "Id")]
    public class ecardimageValidation
    {
        //parameters for sender name textbox + validation for empty field
        [DisplayName("URL")]
        [Required(ErrorMessage = "Select url")]
        public string urlpath { get; set; }
    }
}