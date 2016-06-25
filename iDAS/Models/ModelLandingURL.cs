using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelLandingURL
    {
        public int MerchantAccountNo { get; set; }

        [Required(ErrorMessage = "Must enter your success response URL")]
        [Display(Name = "Success Response URL")]
        [Url]
        public string MerchantSuccessURL { get; set; }
        [Required(ErrorMessage = "Must enter your failure response URL")]
        [Display(Name = "Fail Response URL")]
        [Url]
        public string MerchantFailURL { get; set; }
    }
}