using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelPaymentService
    {
        public int PaymentServiceTypeID { get; set; }
        public string srtPaymentServiceTypeID { get; set; }
        public string PaymentServiceTypeName { get; set; }
        public bool IsActive { get; set; }
        public string PostUrl { get; set; }
        public string WebUrl { get; set; }
        public string WapUrl { get; set; }
        public string QueryUrl { get; set; }
        public string DashboardUrl { get; set; }
        public string FeedBackUrl { get; set; }
        public string ServiceLogo { get; set; }

        public int intMerchantStatus { get; set; }
        public bool IsMerchantSubscribe { get; set; }
        public bool IsMerchantCallBackURLNotNull { get; set; }
    }
}