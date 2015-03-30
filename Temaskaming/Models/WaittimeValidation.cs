using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Temiskaming.Models
{
    [MetadataType(typeof(WaittimeValidation))]
    public partial class waittime
    {
        
    }

    [Bind(Exclude = "id")]
    public class WaittimeValidation
    {
        [DisplayName("Patient's name: ")]
        [Required]
        public string name { get; set; }


        [Required]
        [DisplayName("Time of Admission: ")]
        public DateTime time_admit { get; set; }

        [DisplayName("Time of seeing a doctor: ")]
        public DateTime time_doctor { get; set; }
    }
}