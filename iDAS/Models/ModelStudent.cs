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
        public int Sex { get; set; }
        public string Reference { get; set; }


        public DateTime DateOfAdmission { get; set; }

        public string Email { get; set; }

        public string Religon { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string HomeNumber { get; set; }

        public string Simage { get; set; }

        public string OfficeNumber { get; set; }

        public string MoblieNumber { get; set; }

        public string OtherMobNo { get; set; }

        public int AdmissionClass { get; set; }

        public DateTime LeaveDate { get; set; }

        public string Dues { get; set; }

        public string FatherCNIC { get; set; }

        public string LeaveClass { get; set; }

        public string Reason { get; set; }

        public int LeaveDues { get; set; }

        public int Status { get; set; }

        public string StatusText { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]

        public string ClassNameText { get; set; }
        public string SectionNameText { get; set; }

        public virtual ModelStudentClass StudentClass { get; set; }
        public virtual ModelStudentSection StudentSection { get; set; }

        public List<ModelStudentFee> StudentMonthlyFeeList { get; set; }
    }
    public class ViewModelStudent
    {
        public ViewModelStudent()
        {
            StudentClass = new ModelStudentClass();
            StudentSection = new ModelStudentSection();
            StudentMonthlyFeeList = new List<ModelStudentFee>();
        }


        [Key]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Please Enter StudentName")]

        public string StudentName { get; set; }
        [Required(ErrorMessage = "Please Enter FatherName")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Please Enter Class")]
        public int ClassId { get; set; }
        [Required(ErrorMessage = "Please Enter Section")]
        public int SectionId { get; set; }
        public int CampusId { get; set; }
        public string Profession { get; set; }
       
        public string P { get; set; }
        [Required(ErrorMessage = "Please Enter MonthlyFee")]
        public int MonthlyFee { get; set; }
        public string Reference { get; set; }
        [Display(Name = "Gender")]
        public int Sex { get; set; }
        [Required(ErrorMessage = "Please Enter Date Of Admission")]
        public DateTime DateOfAdmission { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage = "Must Select Email")]
        [StringLength(16, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
        public string Email { get; set; }
       // [Required(ErrorMessage = "Must Select Religon")]
        public int Religon { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string Address { get; set; }
        // [Required(ErrorMessage = "Please Enter Name")]
        public string HomeNumber { get; set; }
        //[Required(ErrorMessage = "Please Enter Address")]
        public string Simage { get; set; }

        [Required(ErrorMessage = "Please Enter OfficeNumber")]
        public string OfficeNumber { get; set; }
        [Required(ErrorMessage = "Please Enter MoblieNumber 1")]
        public string MoblieNumber { get; set; }
        [Required(ErrorMessage = "Please Enter MoblieNumber 2")]
        public int OtherMobNo { get; set; }
   //   [Required(ErrorMessage = "Please Enter AdmissionClass")]
      
        [Required(ErrorMessage = "Please Enter CNIC")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "CNIC Max or Min 13 Charcters")]
        public string FatherCNIC { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]


        public int AddedBy { get; set; }
       
        public int ModifyBy { get; set; }
      
        public virtual ModelStudentClass StudentClass { get; set; }
        public virtual ModelStudentSection StudentSection { get; set; }

        public List<ModelStudentFee> StudentMonthlyFeeList { get; set; }

    }
    
}