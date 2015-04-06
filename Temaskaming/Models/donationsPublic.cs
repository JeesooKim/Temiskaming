using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace Temiskaming.Models
{
    public class donationsPublic
    {
        public int id { get; set; }

        [Display(Name="First Name: ")]
        [Required(ErrorMessage = "First Name required.")]
        public string fname { get; set; }

        [Display(Name="Last Name: ")]
        [Required(ErrorMessage = "Last Name required.")]
        public string lname { get; set; }

        [Display(Name="Email: ")]
        [Required(ErrorMessage = "Email required.")]
        [RegularExpression("^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$", ErrorMessage = "Invaid email.")]
        public string email { get; set; }

        [Display(Name="Donation Amount: ")]
        [Required(ErrorMessage = "Donation amount required.")]
        public Decimal amount { get; set; }

        [Display(Name="Personal Message: ")]
        public string message { get; set; }

        public DateTime donation_date { get; set; }
    }
}