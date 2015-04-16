using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    //This is validation class that validates user input of all the fields required to insert and update news articles.
    [MetadataType(typeof(NewsValidation))]
    public partial class news
    {
        
    }

    [Bind(Exclude = "id")]
    public class NewsValidation
    {
        [DisplayName("Title:")]
        [Required]
        public string title { get; set; }
        
        [DisplayName("Content:")]
        [Required]
        public string content { get; set; }
    }
}