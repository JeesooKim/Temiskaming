using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace Temiskaming.Models
{
    [MetadataType(typeof(OrderValidtion))]
    [Bind(Exclude="OrderId")]

    public partial class Order
    {

    }

    public class OrderValidtion
    {
        [Required]
        public string itemId { get; set; }
        [Required]
        public string OrderDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ToPatient { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string Message { get; set; }
     

    }
}