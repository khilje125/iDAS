using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelMerchantUser
    {
        [Required(ErrorMessage = "Please enter School ID")]
        [Display(Name = "School ID")]
        [MaxLength(8)]
        [MinLength(5)]
        [RegularExpression("[^0-9]", ErrorMessage = "School ID must be in numeric")]
        public string MerchantAccountNo { get; set; }
        public int MerchantUserAccountNo { get; set; }

        [Required(ErrorMessage = "Please enter your full name")]
        [Display(Name = "Full Name")]
        public string MerchantNameEN { get; set; }
        public string MerchantNameAR { get; set; }

        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter correct email address")]
        public string MerchantEmail { get; set; }
        
        [Required(ErrorMessage = "Please must fill password field")]
        [DataType(DataType.Password)]
        [Display(Name = "MerchantPassword")]
        public string MerchantPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("MerchantPassword", ErrorMessage = "It should be similar to Password")]
        public string MerchantPasswordConfirm { get; set; }

        public string MerchantKey { get; set; }
        public string MerchantReferenceID { get; set; }

        [Required(ErrorMessage = "Please enter your mobile number")]
        [Display(Name = "Mobile Number")]
        public string MerchantMobileNumber { get; set; }
        public int CountryID { get; set; }
        public int CityID { get; set; }
        public string MerchantAddressEN { get; set; }
        public string MerchantAddressAR { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }

        [Required(ErrorMessage = "Please enter your company name")]
        [Display(Name = "Company Name")]
        public string CompanyNameEN { get; set; }
        public string CompanyNameAR { get; set; }
        public string CompanyDescEN { get; set; }
        public string CompanyDescAR { get; set; }
        public string CompanyWebSite { get; set; }
        public string CompanyLogo { get; set; }
        public int AccountStatus { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public string LoginAttempt { get; set; }
        public string MerchantIPAddress { get; set; }
        public string MerchantPreferedLanguage { get; set; }

    }
}