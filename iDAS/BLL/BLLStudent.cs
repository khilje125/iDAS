using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
                    objModelStudent.Sex = Convert.ToInt32(aStudent["Sex"]);
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

        public ModelStudent GetStudentInfoById(decimal StudentID)
        {
            ModelStudent objModelStudent = new ModelStudent();
            DataTable tblStudentInfo = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@StudentId", StudentID);

            tblStudentInfo = DALCommon.GetDataUsingDataTable("[sp_Admin_GetStudentInfoByStudentId]", param);
            if (tblStudentInfo.Rows.Count > 0)
            {
                objModelStudent.StudentId = Convert.ToInt32(tblStudentInfo.Rows[0]["StudentId"].ToString());
                objModelStudent.ComputerCode = Convert.ToInt32(tblStudentInfo.Rows[0]["ComputerCode"].ToString());
                objModelStudent.RegNo = Convert.ToInt32(tblStudentInfo.Rows[0]["RegNo"].ToString());
                objModelStudent.StudentName = Convert.ToString(tblStudentInfo.Rows[0]["StudentName"].ToString());
                objModelStudent.FatherName = Convert.ToString(tblStudentInfo.Rows[0]["FatherName"].ToString());
                objModelStudent.MonthlyFee = Convert.ToInt32(tblStudentInfo.Rows[0]["MonthlyFee"].ToString());
                //objModelStudent.Sex = Convert.ToInt32(tblStudentInfo.Rows[0]["Sex"].ToString());
                objModelStudent.DateOfBirth = Convert.ToDateTime(tblStudentInfo.Rows[0]["DateOfBirth"].ToString());
                objModelStudent.Status = Convert.ToInt32(tblStudentInfo.Rows[0]["Status"].ToString());
                objModelStudent.ClassId = Convert.ToInt32(tblStudentInfo.Rows[0]["ClassId"].ToString());
                objModelStudent.SectionId = Convert.ToInt32(tblStudentInfo.Rows[0]["SectionId"].ToString());
            }
            return objModelStudent;
        }



    }
}