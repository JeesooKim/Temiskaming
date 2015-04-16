using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Temiskaming.Models
{

    [MetadataType(typeof(departmentValidation))]
    public partial class department
    {
    }

    [MetadataType(typeof(staffValidation))]
    public partial class staff
    {
    }

    
    //when I had one validation class, such that 
    //[MetadataType(typeof(directoryValidation))]public partial class directoryValidation{}
    //including every fields from both tables
    //Validation was not working
    //but as separating validation class for each table, it starts working...probably(?) becase each of them is partial
    //April 1, 2015...Sumeet helped
    
    [Bind(Exclude="d_id")]
    public partial class departmentValidation
    {       

        [DisplayName("Department Name")]
        [Required(ErrorMessage = "Please enter department name")]
        public string d_name { get; set; }

        [DisplayName("Department Phone")]
        [Required(ErrorMessage = "Please enter department phone")]
        public string d_phone { get; set; }

        [DisplayName("Department Extension")]
        [Required(ErrorMessage = "Please enter department extension")]
        public string d_ext { get; set; }

        [DisplayName("Department Fax")]
        [Required(ErrorMessage = "Please enter department fax")]
        public string d_fax { get; set; }
    }

    //public class departmentNameIdCompare : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid
    //        (object value, ValidationContext validationContext)
    //    {
    //        string departmentName = (string)value;
    //        staff ...
    //    }
    //april 5,2015....tried to build a custom validation model..
    //}


    [Bind(Exclude = "staff_id")]
    public partial class staffValidation
    {

        [DisplayName("Staff Firstname")]
        [Required(ErrorMessage = "Please enter staff's firstname")]
        public string staff_fname { get; set; }

        [DisplayName("Staff Lastname")]
        [Required(ErrorMessage = "Please enter staff's lastname")]
        public string staff_lname { get; set; }

        [DisplayName("Staff Position")]
        [Required(ErrorMessage = "Please enter staff's position")]
        public string staff_position { get; set; }

        [DisplayName("Staff Phone")]
        [Required(ErrorMessage = "Please enter staff's phone")]
        public string staff_phone { get; set; }

        [DisplayName("Staff Extension")]
        [Required(ErrorMessage = "Please enter staff's extension")]
        public string staff_ext { get; set; }

        [DisplayName("Staff Email")]
        [Required(ErrorMessage = "Please enter staff's e-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Please enter a valid email")]
        public string staff_email { get; set; }        

        [Key]
        [ForeignKey("department")]
        [DisplayName("Staff Department Id")]
        [Required(ErrorMessage = "Please choose the department ID as its name")]        
        public int staff_departmentId { get; set; }
    }


    //[Team2]Temiskaming-Hospital website Redesign Project @ Humber College
    //Feature: Directory - Model
    //File: directoryValidation.cs
    //Author: Jeesoo Kim
    //March30, 2015
}