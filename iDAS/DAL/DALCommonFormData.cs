using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iDAS.DAL
{
    public class DALCommonFormData
    {

        public static List<SelectListItem> GetClassDropdown(decimal SchoolAccountId)
        {
            List<SelectListItem> lstClasses = new List<SelectListItem>();
            try
            {
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
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "GetClassDropdown, DALCommonFormData");
            }
            return lstClasses;
        }
        public static List<SelectListItem> GetClassSectionDropdown(decimal SchoolAccountId)
        {
            List<SelectListItem> lstClassSection = new List<SelectListItem>();
            try
            {
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
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "GetClassSectionDropdown, DALCommonFormData");
            }
            return lstClassSection;
        }
        

        //Get Report List Dropdown
        public static List<SelectListItem> GetReportListDropdown(double SchoolAccountId, int ReportCategory)
        {
            List<SelectListItem> lstReports = new List<SelectListItem>();
            try
            {
                lstReports.Add(new SelectListItem { Text = "--- Select Report Type --- ", Value = "0" });

                DataTable tblReportsList = new DataTable();
                SqlParameter[] param = new SqlParameter[2];

                param[0] = new SqlParameter("@SchoolAccountId", SchoolAccountId);
                param[1] = new SqlParameter("@ReportCategory", ReportCategory);

                tblReportsList = DALCommon.GetDataUsingDataTable("[sp_GetReportInfoBySchoolId]", param);
                if (tblReportsList.Rows.Count > 0)
                {
                    foreach (DataRow aClass in tblReportsList.Rows)
                    {
                        lstReports.Add(new SelectListItem { Text = Convert.ToString(aClass["ReportName"]), Value = Convert.ToString(aClass["ReportId"]) });
                    }
                }
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "GetReportListDropdown, DALCommonFormData");
            }
            return lstReports;
        }

        //Get Repor Info Data Table
        public static DataTable GetReportInfoDataTable(int ReportId)
        {
            DataTable tblDataTable = new DataTable();
            try
            {
                DataTable tblReportsList = new DataTable();
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@ReportId", ReportId);

                tblReportsList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetReportInfoByReportId]", param);
                return tblDataTable;
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "GetReportListDropdown, DALCommonFormData");
            }

            return tblDataTable;
        }

        //Get Repor Info Data Table
        public static string GetReportFileNamePath(int ReportId)
        {
            string reportFileName = "";
            try
            {
                DataTable tblReportsList = new DataTable();
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@ReportId", ReportId);

                tblReportsList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetReportInfoByReportId]", param);
                if (tblReportsList.Rows.Count > 0)
                {
                    reportFileName = Convert.ToString(tblReportsList.Rows[0]["ReportFilePathName"]);
                }
                
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "GetReportListDropdown, DALCommonFormData");
            }

            return reportFileName;
        }

        //Get Repor Info Data Table
        public static ModelReportInfo GetReportInfoModel(int ReportId)
        {
            ModelReportInfo objModelReportInfo = new ModelReportInfo();
            try
            {
                DataTable tblReportsList = new DataTable();
                SqlParameter[] param = new SqlParameter[1];

                param[0] = new SqlParameter("@ReportId", ReportId);

                tblReportsList = DALCommon.GetDataUsingDataTable("[sp_Admin_GetReportInfoByReportId]", param);
                if (tblReportsList.Rows.Count > 0)
                {
                    objModelReportInfo.ReportFilePathName = Convert.ToString(tblReportsList.Rows[0]["ReportFilePathName"]);
                    objModelReportInfo.SchoolAddressForReport = Convert.ToString(tblReportsList.Rows[0]["SchoolAddressForReport"]);
                    objModelReportInfo.SchoolTitleForReport = Convert.ToString(tblReportsList.Rows[0]["SchoolTitleForReport"]);

                }
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "GetReportListDropdown, DALCommonFormData");
            }
            return objModelReportInfo;
        }

    }
}