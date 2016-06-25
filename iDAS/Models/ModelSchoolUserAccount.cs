using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelSchoolUserAccount
    {
        public ModelSchoolUserAccount()
        {
            SchoolAccount = new ModelSchoolAccount();
        }

        [Required(ErrorMessage = "Please enter School ID")]
        [Display(Name = "User ID")]
        [MaxLength(8)]
        [MinLength(5)]
        [RegularExpression("[^0-9]", ErrorMessage = "School ID must be in numeric")]
        public int UserAccountId { get; set; }
        public int SchoolAccountId { get; set; }
        public string UserFName { get; set; }
        public string UserLName { get; set; }
        public string UserFatherName { get; set; }
        public string UserCNIC { get; set; }
        public string UserAddress { get; set; }
        public string UserCity { get; set; }
        [Required(ErrorMessage = "Please enter email address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please enter correct email address")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Please must fill password field")]
        [DataType(DataType.Password)]
        [Display(Name = "MerchantPassword")]
        public string UserPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("UserPassword", ErrorMessage = "It should be similar to Password")]
        public string UserPasswordConfirm { get; set; }

        public string UserEmailActivationCode { get; set; }
        public int UserPasswordType { get; set; }
        public int UserAccountType { get; set; }
        public string UserProfileImage { get; set; }
        public string UserContactNumber { get; set; }
        public string UserContactNumber2 { get; set; }
        public string UserContactNumber3 { get; set; }
        public string UserReferenceName { get; set; }
        public string UserReferenceContact { get; set; }
        public string UserReferenceAddress { get; set; }
        public DateTime AddedDate { get; set; }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int UseAccountStatus { get; set; }
        public string UserAccountStatusDescription { get; set; }

        public ModelSchoolAccount SchoolAccount { get; set; }
    }
}