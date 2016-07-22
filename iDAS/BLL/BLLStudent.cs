using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iDAS.DAL;

namespace iDAS.BLL
{
    public class BLLStudent
    {
        public List<ModelStudent> GetStudentList(string SearchCriteria, decimal SchoolAccountId)
        {
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
            DataTable tblStudentList = new DataTable();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SearchCriteria", String.IsNullOrEmpty(SearchCriteria) ? "" : SearchCriteria);
            param[1] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblStudentList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentListBySchoolId]", param);
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    ModelStudent objModelStudent = new ModelStudent();
                    objModelStudent.StudentId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelStudent.ComputerCode =  Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelStudent.RegNo = Convert.ToInt32(aStudent["RegNo"]);
                    objModelStudent.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelStudent.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelStudent.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    objModelStudent.Sex = Convert.ToInt32(aStudent["Gender"]);
                    objModelStudent.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                    objModelStudent.Status = Convert.ToInt32(aStudent["Status"]);
                    objModelStudent.StatusText = Convert.ToString(aStudent["StatusText"]);
                    objModelStudent.StudentClass.ClassName = Convert.ToString(aStudent["ClassName"]);
                    objModelStudent.StudentSection.SectionName = Convert.ToString(aStudent["SectionName"]);
                    lstModelStudent.Add(objModelStudent);
                }
            }
            return lstModelStudent;
        }
        /// <summary>
        /// GET STUDENTS LIST
        /// </summary>
        /// <param name="aStudent"></param>
        /// <returns></returns>

        public List<ModelStudent> GetStudentList()
        {
            List<ModelStudent> lstModelStudent = new List<ModelStudent>();
            DataTable tblStudentList = new DataTable();

            tblStudentList = DALCommon.GetDataByQuery("[sp_GetStudentRocordList]");
            if (tblStudentList.Rows.Count > 0)
            {
                foreach (DataRow aStudent in tblStudentList.Rows)
                {
                    ModelStudent objModelStudent = new ModelStudent();
                    objModelStudent.StudentId = Convert.ToInt32(aStudent["StudentId"]);
                    objModelStudent.ComputerCode = Convert.ToInt32(aStudent["ComputerCode"]);
                    objModelStudent.RegNo = Convert.ToInt32(aStudent["RegNo"]);
                    objModelStudent.StudentName = Convert.ToString(aStudent["StudentName"]);
                    objModelStudent.FatherName = Convert.ToString(aStudent["FatherName"]);
                    objModelStudent.MonthlyFee = Convert.ToInt32(aStudent["MonthlyFee"]);
                    //objModelStudent.Sex = Convert.ToInt32(aStudent["Gender"]);
                    objModelStudent.DateOfBirth = Convert.ToDateTime(aStudent["DateOfBirth"]);
                    objModelStudent.StudentClass.ClassName = Convert.ToString(aStudent["ClassName"]);
                    objModelStudent.StudentSection.SectionName = Convert.ToString(aStudent["SectionName"]);
                    objModelStudent.StatusText = Convert.ToString(aStudent["StatusText"]);
                    objModelStudent.Status =  Convert.ToInt32(aStudent["Status"]);
                    objModelStudent.DateOfAdmission = Convert.ToDateTime(aStudent["DateOfAdmission"]);
                  
                    lstModelStudent.Add(objModelStudent);
                }
            }
            return lstModelStudent;
        }

        public decimal AddStudentInfo(ViewModelStudent aStudent)
        {
            aStudent.AddedBy = Convert.ToInt32(HttpContext.Current.Session[DALVariables.SchoolAccountId]);
          
            SqlParameter[] param = new SqlParameter[22];
            SqlCommand cmd = new SqlCommand();
            // cmd.CommandType = CommandType.StoredProcedure;
          
            param[0] = new SqlParameter("@StudentName", aStudent.StudentName);
            param[1] = new SqlParameter("@FatherName", aStudent.FatherName);
            param[2] = new SqlParameter("@ClassId", aStudent.ClassId);

            param[3] = new SqlParameter("@SectionId", aStudent.SectionId);
            param[4] = new SqlParameter("@MonthlyFee", aStudent.MonthlyFee);
          
            param[5] = new SqlParameter("@Profession", aStudent.Profession);
            param[6] = new SqlParameter("@P", aStudent.P);
            param[7] = new SqlParameter("@Reference", aStudent.Reference);
            param[8] = new SqlParameter("@Gender", aStudent.Sex);
            param[9] = new SqlParameter("@DateOfAdmission", aStudent.DateOfAdmission);
            param[10] = new SqlParameter("@DateOfBirth", aStudent.DateOfBirth);
            param[11] = new SqlParameter("@Address", aStudent.Address);
            param[12] = new SqlParameter("@HomeNumber", aStudent.HomeNumber);
            param[13] = new SqlParameter("@OfficeNumber", aStudent.OfficeNumber);
            param[14] = new SqlParameter("@MoblieNumber", aStudent.MoblieNumber);
            param[15] = new SqlParameter("@FatherCnic", aStudent.FatherCNIC);
            param[16] = new SqlParameter("@Email", aStudent.Email);
            //param[17] = new SqlParameter("@AdmissionClass", aStudent.AdmissionClass);
            param[17] = new SqlParameter("@MobileNo", aStudent.OtherMobNo);
            param[18] = new SqlParameter("@CampusID", aStudent.CampusId);
            param[19] = new SqlParameter("@Religon", aStudent.Religon);
            param[20] = new SqlParameter("@StudentImage", aStudent.Simage);
            param[21] = new SqlParameter("@AddedBy", aStudent.AddedBy);
      

            return DALCommon.ExecuteNonQueryReturnIdentity("[InsertUpdateStudentInfo]", param);


        }

        public decimal UpdateStudent(ViewModelStudent aStudent)
        {
            

            SqlParameter[] param = new SqlParameter[18];
            SqlCommand cmd = new SqlCommand();
            // cmd.CommandType = CommandType.StoredProcedure;
            aStudent.ModifyBy = Convert.ToInt32(HttpContext.Current.Session[DALVariables.SchoolAccountId]);

            param[0] = new SqlParameter("@StudentId", aStudent.StudentId);

            param[1] = new SqlParameter("@StudentName", aStudent.StudentName);
            param[2] = new SqlParameter("@FatherName", aStudent.FatherName);
            param[3] = new SqlParameter("@ClassId", aStudent.ClassId);

            param[4] = new SqlParameter("@SectionId", aStudent.SectionId);
            param[5] = new SqlParameter("@MonthlyFee", aStudent.MonthlyFee);

            param[6] = new SqlParameter("@Profession", aStudent.Profession);
         
            param[7] = new SqlParameter("@Reference", aStudent.Reference);
         
            param[8] = new SqlParameter("@DateOfAdmission", aStudent.DateOfAdmission);
            param[9] = new SqlParameter("@DateOfBirth", aStudent.DateOfBirth);
            param[10] = new SqlParameter("@Address", aStudent.Address);
            param[11] = new SqlParameter("@HomeNumber", aStudent.HomeNumber);
            param[12] = new SqlParameter("@OfficeNumber", aStudent.OfficeNumber);
            param[13] = new SqlParameter("@MoblieNumber", aStudent.MoblieNumber);
            param[14] = new SqlParameter("@FatherCnic", aStudent.FatherCNIC);
            param[15] = new SqlParameter("@Email", aStudent.Email);
            //param[17] = new SqlParameter("@AdmissionClass", aStudent.AdmissionClass);
            param[16] = new SqlParameter("@MobileNo", aStudent.OtherMobNo);
           
            
            
            param[17] = new SqlParameter("@ModifyBy", aStudent.ModifyBy);


            return DALCommon.ExecuteNonQueryReturnIdentity("[InsertUpdateStudentInfo]", param);


        }
        public List<SelectListItem> GetClassDropdown(decimal SchoolAccountId)
        {
            List<SelectListItem> lstClasses = new List<SelectListItem>();
            lstClasses.Add(new SelectListItem { Text = "--- Select Class --- ", Value = "0" });

            DataTable tblClassList = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblClassList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetClassDropdownListBySchoolId]", param);
            if (tblClassList.Rows.Count > 0)
            { 
                foreach (DataRow aClass in tblClassList.Rows)
                {
                    lstClasses.Add(new SelectListItem { Text = Convert.ToString(aClass["ClassName"]), Value = Convert.ToString(aClass["ClassId"]) });
                }
            }
            return lstClasses;
        }

        public List<SelectListItem> GetClassSectionDropdown(decimal SchoolAccountId)
        {
            List<SelectListItem> lstClassSection = new List<SelectListItem>();
            lstClassSection.Add(new SelectListItem { Text = "--- Select Class Section--- ", Value = "0" });

            DataTable tblClassList = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblClassList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetClassSectionDropdownListBySchoolId]", param);
            if (tblClassList.Rows.Count > 0)
            {
                foreach (DataRow aClass in tblClassList.Rows)
                {
                    lstClassSection.Add(new SelectListItem { Text = Convert.ToString(aClass["SectionName"]), Value = Convert.ToString(aClass["SectionId"]) });
                }
            }
            return lstClassSection;
        }

        // 
        public List<SelectListItem> GetFeeMonthDropdown(decimal SchoolAccountId)
        {
            List<SelectListItem> lstMonth = new List<SelectListItem>();
            lstMonth.Add(new SelectListItem { Text = "--- Select Month --- ", Value = "0" });

            DataTable tblClassList = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@SchoolAccountId", SchoolAccountId);

            tblClassList = DALCommon.GetDataUsingDataTable("[sp_GetAllFeeMonth]", param);
            if (tblClassList.Rows.Count > 0)
            {
                foreach (DataRow aClass in tblClassList.Rows)
                {
                    lstMonth.Add(new SelectListItem { Text = Convert.ToString(aClass["FeeMonth"]), Value = Convert.ToString(aClass["FeeMonthId"]) });
                }
            }
            return lstMonth;
        }

        public ViewModelStudent GetStudentInfoById(decimal StudentID)
        {
            ViewModelStudent objViewModelStudent = new ViewModelStudent();
            DataTable tblStudentInfo = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@StudentId", StudentID);

            tblStudentInfo = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentInfoByStudentId]", param);
            if (tblStudentInfo.Rows.Count > 0)
            {
                objViewModelStudent.StudentId = Convert.ToInt32(tblStudentInfo.Rows[0]["StudentId"].ToString());


                   //   MoblieNumber, AdmissionClass, Status, StudentClasses.ClassName,ClassSection.SectionName,(CASE WHEN Religon =0 THEN 'Muslim' WHEN Status = 1 THEN 'Non-Muslim' END)Gender, isnull(FatherCnic,0)FatherCnic, isnull(Email,Null)Email,ISNULL(MobileNo,Null)MobileNo
                objViewModelStudent.StudentName = Convert.ToString(tblStudentInfo.Rows[0]["StudentName"].ToString());
                objViewModelStudent.FatherName = Convert.ToString(tblStudentInfo.Rows[0]["FatherName"].ToString());
                objViewModelStudent.MonthlyFee = Convert.ToInt32(tblStudentInfo.Rows[0]["MonthlyFee"].ToString());
                objViewModelStudent.Profession = Convert.ToString(tblStudentInfo.Rows[0]["Profession"].ToString());
                 objViewModelStudent.Reference = Convert.ToString(tblStudentInfo.Rows[0]["Reference"].ToString());
              // objViewModelStudent.Sex = Convert.ToInt32(tblStudentInfo.Rows[0]["Gender"].ToString());
                objViewModelStudent.DateOfBirth = Convert.ToDateTime(tblStudentInfo.Rows[0]["DateOfBirth"].ToString());
                objViewModelStudent.DateOfAdmission = Convert.ToDateTime(tblStudentInfo.Rows[0]["DateOfAdmission"].ToString());
                objViewModelStudent.Address = Convert.ToString(tblStudentInfo.Rows[0]["Address"].ToString());
                 objViewModelStudent.HomeNumber = Convert.ToString(tblStudentInfo.Rows[0]["HomeNumber"].ToString());
                  objViewModelStudent.OfficeNumber = Convert.ToString(tblStudentInfo.Rows[0]["OfficeNumber"].ToString());
                  objViewModelStudent.MoblieNumber = Convert.ToString(tblStudentInfo.Rows[0]["MoblieNumber"].ToString());
                objViewModelStudent.Status = Convert.ToInt32(tblStudentInfo.Rows[0]["Status"].ToString());
                objViewModelStudent.ClassId = Convert.ToInt32(tblStudentInfo.Rows[0]["ClassId"].ToString());
                objViewModelStudent.SectionId = Convert.ToInt32(tblStudentInfo.Rows[0]["SectionId"].ToString());
               objViewModelStudent.FatherCNIC = Convert.ToString(tblStudentInfo.Rows[0]["FatherCNIC"].ToString());
               objViewModelStudent.Email = Convert.ToString(tblStudentInfo.Rows[0]["Email"].ToString());
               //objViewModelStudent.Religon = Convert.ToInt32(tblStudentInfo.Rows[0]["Religon"].ToString());
             // objViewModelStudent.CampusId = Convert.ToInt32(tblStudentInfo.Rows[0]["CampusId"].ToString());
            ///  objViewModelStudent.OtherMobNo = Convert.ToInt32(tblStudentInfo.Rows[0]["MobileNo"].ToString());
             // objViewModelStudent.Simage = Convert.ToString(tblStudentInfo.Rows[0]["StudentImage"].ToString());

            }
            return objViewModelStudent;
        }



    }
}