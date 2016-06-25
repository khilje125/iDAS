using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelSchoolAccount
    {
        public ModelSchoolAccount()
        { 
        
        }

        [Required(ErrorMessage = "Please enter School ID")]
        [Display(Name = "School ID")]
        [MaxLength(8)]
        [MinLength(5)]
        [RegularExpression("[^0-9]", ErrorMessage = "School ID must be in numeric")]

        public int SchoolAccountId { get; set; }
        public string SchoolName { get; set; }
        public string SchoolProfileLogo { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolCity { get; set; }
        public string SchoolPhone { get; set; }
        public string SchoolMobile { get; set; }
        public string SchoolFax { get; set; }
        public string SchoolContactPerson { get; set; }
        public string SchoolContactPersonMobile { get; set; }
        public string SchoolAccountStatus { get; set; }
        public string SchoolAccounStatusDescription { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public int ModifyBy { get; set; }
    }
}