using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace Temiskaming.Models
{
    [MetadataType(typeof(ContactFormValidation))]
    public partial class ContactForm
    {

    }

    [Bind(Exclude = "id")]
    public class ContactFormValidation
    {
        [DisplayName("Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }

        [DisplayName("Comment")]
        [Required]
        public string Comment { get; set; }

    }

}