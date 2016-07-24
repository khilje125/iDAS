using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelStudentFee
    {
        public double VoucherId { get; set; }
        public double FeeId { get; set; }
        public double StudentId { get; set; }
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
        public string FeeMonthText { get; set; }
        public int Total { get; set; }
        public int FeePaidStatus { get; set; }
        public ModelStudentClass ModelStudentClass { get; set; }
        public ModelStudentSection ModelStudentSection { get; set; }
        
    }

    public class FeeViewModel
    {
      
        public decimal studentId { get; set; }      
         public int FeeMonth { get; set; }
         public int AdmissionFee { get; set; }
         public int ReAdmissin { get; set; }
         public int RegistrationFee { get; set; }
         public int ComputerFee { get; set; }
         public int Fine { get; set; }
         public int ExamFee { get; set; }
         public int GenatorFee { get; set; }      
         public string FeeDate { get; set; }
         public int SchoolAccountId { get; set; }
    
    }
}