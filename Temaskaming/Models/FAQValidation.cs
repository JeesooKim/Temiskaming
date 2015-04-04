using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace Temiskaming.Models
{
    [MetadataType(typeof(FAQValidation))]
    public partial class FAQ
    {

    }

    [Bind(Exclude="id")]
    public class FAQValidation
    {
        [DisplayName("Question")]
        [Required]
        public string Question { get; set; }

        [DisplayName("Answer")]
        [Required]
        public string Answer { get; set; }

    }

}