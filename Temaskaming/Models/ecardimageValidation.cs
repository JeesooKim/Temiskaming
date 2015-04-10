using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;
// model used for ecard images
namespace Temiskaming.Models
{

    [MetadataType(typeof(ecardimageValidation))]
    public partial class ecardimage
    {
    }

    [Bind(Exclude = "Id")]
    public class ecardimageValidation
    {
        //parameters for url path and error message for ecard images
        [DisplayName("URL")]
        [Required(ErrorMessage = "Select url")]
        public string urlpath { get; set; }
    }
}