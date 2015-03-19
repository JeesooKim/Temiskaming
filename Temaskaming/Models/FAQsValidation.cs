using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(FAQsValidation))]
    public partial class FAQ
    {

    }

    [Bind(Exclude = "Id")]
    public class FAQsValidation
    {
        //parameters for question + validation for empty field
        [DisplayName("Question")]
        [Required(ErrorMessage = "Please enter a valid question")]
        public string Question { get; set; }

        //parameters for answer + validation for empty field
        [DisplayName("Answer")]
        [Required(ErrorMessage = "Please enter a valid answer")]
        public string Answer { get; set; }
    }


}