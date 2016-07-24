using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelStudentFeeFollowUpMonthCountReport
    {
        //UnPaidMonthsCount,StudentId,ComputerCode,StudentName,FatherName,Total,ClassId,SectionId,ClassName,SectionName
        public double StudentId { get; set; }
        public int ComputerCode { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int Total { get; set; }
        public int UnPaidMonthCount { get; set; }
        public string HomeNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string MoblieNumber { get; set; }
        public string OtherNumber { get; set; }
    }
}