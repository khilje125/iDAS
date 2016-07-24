using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iDAS.Models
{
    public class ModelStudentDynamicReport
    {
        public int? StudentId { get; set; }
        public int? ComputerCode { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ClassNameText { get; set; }
    }
}