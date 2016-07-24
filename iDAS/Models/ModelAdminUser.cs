using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelAdminUser
    {
        public int UserAccountNo { get; set; }

        [Required(ErrorMessage = "Please Enter your User Name")]
        [Display(Name = "UserName")]
        public String UserName { get; set; }

        [Required(ErrorMessage = "Please First Name")]
        [Display(Name = "FirstName")]
        public String UserFirstName { get; set; }

        [Required(ErrorMessage = "Please Last Name")]
        [Display(Name = "LastName")]
        public String UserLastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email Address")]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$",
        ErrorMessage = "Please Enter Correct Email Address")]
        public String UserEmail { get; set; }

        [Required(ErrorMessage = "Please must fill password field")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public String UserPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "It should be similar to Password")]
        public String UserConfirmPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "MobileNumber")]
        public Double UserMobileNumber { get; set; }

        public string UserProfileImage { get; set; }

        public int UserAccountStatus { get; set; }
        public int UserAccountType { get; set; }
        public DateTime UserRegistrationDate { get; set; }

    }
}