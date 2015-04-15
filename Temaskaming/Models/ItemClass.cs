using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Temiskaming.Models
{

    public class ItemClass
    {

        public int ItemId { get; set; }
        
        [DisplayName("Item Name")]
        [Required]
        public string Item { get; set; }

        [DisplayName("Description")]
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        
        [Required(ErrorMessage ="Required")]
        [Range(3.00, 200.00, ErrorMessage= "Price must range from $3.00 to $200")]
        public decimal Price { get; set; }


        public string Image { get; set; }
        
        [Required]
        public int Inventory { get; set; }

    }
}