using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace Temiskaming.Models
{
     [MetadataType(typeof(GiftValidation))]

    public partial class Gift
     {

     }


    [Bind(Exclude = "ItemId")]
    public class GiftValidation
    {
        [Required]
        public string Item { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
       
        public string Image { get; set; }


     
    }
}