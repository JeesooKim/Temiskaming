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
        public string Item { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
     
    }
}