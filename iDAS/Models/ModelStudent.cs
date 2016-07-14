using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelStudent
    {
        public ModelStudent()
        {
            StudentClass = new ModelStudentClass();
            StudentSection = new ModelStudentSection();
            StudentMonthlyFeeList = new List<ModelStudentFee>();
        }

        public int StudentId { get; set; }
        public int ComputerCode { get; set; }
        public int RegNo { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int MonthlyFee { get; set; }
        public string Profession { get; set; }
        public string P { get; set; }
        public string Reference { get; set; }
        public int Sex { get; set; }
        public DateTime DateOfAdmission { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string HomeNumber { get; set; }
        public string OfficeNumber { get; set; }
        public string MoblieNumber { get; set; }
        public int AdmissionClass { get; set; }
        public DateTime LeaveDate { get; set; }
        public string Dues { get; set; }
        public string LeaveClass { get; set; }
        public string Reason { get; set; }
        public int LeaveDues { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }

        public string ClassNameText { get; set; }
        public string SectionNameText { get; set; }

        public virtual ModelStudentClass StudentClass { get; set; }
        public virtual ModelStudentSection StudentSection { get; set; }

        public List<ModelStudentFee> StudentMonthlyFeeList { get; set; }

    }
}