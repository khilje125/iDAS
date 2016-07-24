using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            ModelStudentFee = new List<ModelStudentFee>();
        }
        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please Enter ComputerCode")]
       // [RegularExpression("[^0-9]", ErrorMessage = "Computer Code must be in numeric")]
        public int ComputerCode { get; set; }
        [Required(ErrorMessage = "Please Enter RegNo")]
        //[RegularExpression("[^0-9]", ErrorMessage = "Registration No must be in numeric")]
        public int RegNo { get; set; }
       [Required(ErrorMessage = "Please Enter StudentName")]
        public string StudentName { get; set; }
       [Required(ErrorMessage = "Please Enter FatherName")]
        public string FatherName { get; set; }
       [Required(ErrorMessage = "Please Enter Class")]
        public int ClassId { get; set; }
      //  [Required(ErrorMessage = "Please Enter Section")]
        public int SectionId { get; set; }
      //  [Required(ErrorMessage = "Please Enter MonthlyFee")]
       // [Required(ErrorMessage = "Please Enter MonthlyFee")]
      // [RegularExpression("[^0-9]", ErrorMessage = "MonthlyFee  must be in numeric")]
       // [MaxLength(4)]
       // [MinLength(3)]
        public int MonthlyFee { get; set; }
      //  [Required(ErrorMessage = "Please Enter Profession")]

        public string Profession { get; set; }
      //  [Required(ErrorMessage = "Please Enter Name")]
        public string P { get; set; }
       // [Required(ErrorMessage = "Please Enter Reference")]
        public string Reference { get; set; }
       [Display(Name = "Gender")]
        public int Sex { get; set; }
        //[Required(ErrorMessage = "Please Enter DateOfAdmission")]
        public DateTime DateOfAdmission { get; set; }
       // [Required(ErrorMessage = "Please Enter DateOfBirth")]
         [Required(ErrorMessage = "Must Select Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
       [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
       // [Required(ErrorMessage = "Please Enter Name")]
        public string HomeNumber { get; set; }
       // [Required(ErrorMessage = "Please Enter OfficeNumber")]
        public string OfficeNumber { get; set; }
        [Required(ErrorMessage = "Please Enter MoblieNumber")]
        public string MoblieNumber { get; set; }
       // [Required(ErrorMessage = "Please Enter AdmissionClass")]
        public int AdmissionClass { get; set; }
       // [Required(ErrorMessage = "Please Enter LeaveDate")]
        public DateTime LeaveDate { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Dues { get; set; }
       // [Required(ErrorMessage = "Please Enter LeaveClass")]
        public string LeaveClass { get; set; }
       // [Required(ErrorMessage = "Please Enter Reason")]
        public string Reason { get; set; }
        //[Required(ErrorMessage = "Please Enter LeaveDues")]


        public int LeaveDues { get; set; }
     
        public int Status { get; set; }
       // [Required(ErrorMessage = "Please Enter StatusText")]
        public string StatusText { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]

        public string ClassNameText { get; set; }
        public string SectionNameText { get; set; }

        public virtual ModelStudentClass StudentClass { get; set; }
        public virtual ModelStudentSection StudentSection { get; set; }

        public List<ModelStudentFee> ModelStudentFee { get; set; }

    }
}