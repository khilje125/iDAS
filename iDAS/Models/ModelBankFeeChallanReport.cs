using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelBankFeeChallanReport
    {
        public double FeeId { get; set; }
        public double StudentId { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int ComputerCode { get; set; }
        public int AdmissionFee { get; set; }
        public int Admission { get; set; }
        public int ReAdmissionFee { get; set; }
        public int RegistrationFee { get; set; }
        public int MonthlyFee { get; set; }
        public int ComputerFee { get; set; }
        public int Fine { get; set; }
        public int ExamFee { get; set; }
        public int GeneratorFee { get; set; }
        public int FeeMonthId { get; set; }
        public string FeeMonth { get; set; }
        public int Total { get; set; }
        public string HomeContact { get; set; }
        public string OfficeContact { get; set; }
        public string MobileContact { get; set; }
        public string OtherContact { get; set; }

    }
}