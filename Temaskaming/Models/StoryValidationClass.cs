using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(StoryValidationClass))]
    public partial class story
    {
         
    }

    [Bind(Exclude = "id")]
    public class StoryValidationClass
    {
        [DisplayName("Author: ")]
        [Required]
        public string author { get; set; }

        [DisplayName("Email: ")]
        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email address.")]
        public string email { get; set; }

        [Required]
        [DisplayName("Title: ")]
        public string title { get; set; }

        [Required]
        [DisplayName("Story: ")]
        public string story1 { get; set; }
    }
}