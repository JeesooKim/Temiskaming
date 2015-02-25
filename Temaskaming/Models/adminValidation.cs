using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(adminValidation))]
    public partial class admin
    {

    }

    [Bind(Exclude="id")]
    public class adminValidation
    {
        [DisplayName("Username: ")]
        [Required]
        public string login { get; set; }

        [DisplayName("Password: ")]
        [Required]
        public string pass { get; set; }
    }
}