using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(appointmentValdation))]
    public partial class appointment
    { }

    [Bind(Exclude="id")]
    public class appointmentValdation
    {
        [Required]
        public int doctor_id;
         
        [Required]
        public System.DateTime booking_date;

        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string email;

        [Required]
       // [RegularExpression("")]
        public string phone;
    }

}