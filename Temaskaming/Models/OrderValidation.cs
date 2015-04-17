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
    public partial class Order
    {

    }
    
    [Bind(Exclude="OrderId")]
    public class OrderValidtion
    {
        [Required]
        public string Item { get; set; }
        [Required]
        public string OrderDate { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string ToPatient { get; set; }
        [Required]
        public string From { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public decimal Price { get; set; }


    }
}