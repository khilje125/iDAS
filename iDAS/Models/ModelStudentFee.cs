using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelStudentFee
    {
        public decimal FeeId { get; set; }
        public decimal StudentId { get; set; }
        public int ComputerCode { get; set; }
        public int SerialNo { get; set; }
        public DateTime FeeDate { get; set; }
        public int AdmissionFee { get; set; }
        public int Admission { get; set; }
        public int ReAdmissionFee { get; set; }
        public int RegistrationFee { get; set; }
        public int MonthlyFee { get; set; }
        public int ComputerFee { get; set; }
        public int Fine { get; set; }
        public int ExamFee { get; set; }
        public int GeneratorFee { get; set; }
        public string FeeMonth { get; set; }
        public int Total { get; set; }
        public int FeePaidStatus { get; set; }
        
    }
}