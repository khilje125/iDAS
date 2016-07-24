using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelStudentBankChallan
    {
        public double VoucherId { get; set; }
        public double StudentId { get; set; }
        public int ComputerCode { get; set; }
        public int RegNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int MonthlyFee { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public string ClassNameText { get; set; }
        public string SectionNameText { get; set; }
        public DateTime VoucherDueDate { get; set; }
    }
}