using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelTransactionByCreditCard
    {
        public int TransactionID { get; set; }
        public string UserID { get; set; }
        public string ProductID { get; set; }
        public decimal MerchantAccountNo { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public string TransactionDetails { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public bool TestMode { get; set; }
        public decimal FortId { get; set; }
        public string StatusCode { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerIp { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string AuthorizationCode { get; set; }

        public string PaymentType { get; set; }
    }
}