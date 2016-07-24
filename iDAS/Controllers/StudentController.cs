using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iDAS.Controllers
{
    public class StudentController : BootstrapBaseController
    {
        ModelStudent objModelStudent = new ModelStudent();
        //
        // GET: /Add/

        public void BindDropdownlist()
        {
            BLLStudent objBLLStudent = new BLLStudent();

            ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

            ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
            ViewBag.dpFeeMonths = objBLLStudent.GetFeeMonthDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

        }
        public ActionResult Index()
        {
            try
            {
                BindDropdownlist();
                var objStudentModel = new ViewModelStudent();
                GetAllStudentData();
                return PartialView(customview("_StudentInformation", "Student"), objStudentModel);
               

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        [HttpPost]
        public ActionResult GetAllStudentData()
        {
            try
            {
                List<ModelStudent> lstModelStudent = new List<ModelStudent>();

                BLLStudent objBLLStudent = new BLLStudent();

                lstModelStudent = objBLLStudent.GetStudentList();
                return PartialView(customview("_GetStudentList", "Student"), lstModelStudent);

            }
            catch (Exception)
            {
                
                throw;
            }
        }
       
        //
        // GET: /Search/
        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        //
        // GET: /Search1/
        [HttpGet]
        public ActionResult Search1()
        {
            BLLStudent objBLLStudent = new BLLStudent();

            ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
            ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

            return PartialView(customview("_SearchFormPartial", "Student"));
        }

        //bulk fees
       
        [HttpPost]
        public ActionResult Search1(string ComputerCode, string RegNo, string StudentName, string FatherName, string StudentStatus, string dropDownClass, string dropDownSection)
        {
            StringBuilder strSearchCriteria = new StringBuilder();
            string searchCriteria = "";
            //If Computer Code Is Not Empty
            if ((!string.IsNullOrEmpty(RegNo.Trim())))
            {
                strSearchCriteria.Append(" Students.RegNo =" + RegNo.Trim() + " AND ");
            }

            //If Computer Code Is Not Empty
            if ((!string.IsNullOrEmpty(ComputerCode.Trim())))
            {
                strSearchCriteria.Append(" Students.ComputerCode =" + ComputerCode.Trim() + " AND ");
            }

            //If Student Name Is Not Empty
            if ((!string.IsNullOrEmpty(StudentName.Trim())))
            {
                strSearchCriteria.Append(" (Students.StudentName LIKE '%" + StudentName.Trim() + "%') AND ");
            }

            //If Father Name Is Not Empty
            if ((!string.IsNullOrEmpty(FatherName.Trim())))
            {
                strSearchCriteria.Append(" (Students.FatherName LIKE '%" + FatherName.Trim() + "%') AND ");
            }

            //If Student Statuse Is Not Empty
            if ((!string.IsNullOrEmpty(StudentStatus.Trim())))
            {
                int _studentStaus = Convert.ToInt32(StudentStatus.Trim());
                if (_studentStaus > 0)
                {
                    strSearchCriteria.Append(" Students.Status = " + _studentStaus + " AND ");
                }
            }

            //If Selected Class Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownClass.Trim())))
            {
                int _selectedClass = Convert.ToInt32(dropDownClass.Trim());
                if (_selectedClass > 0)
                {
                    strSearchCriteria.Append(" Students.ClassId = " + _selectedClass + " AND ");
                }
            }

            //If Class Section Is Not Empty
            if ((!string.IsNullOrEmpty(dropDownSection.Trim())))
            {
                int _selectedSection = Convert.ToInt32(dropDownSection.Trim());
                if (_selectedSection > 0)
                {
                    strSearchCriteria.Append(" Students.SectionId = " + _selectedSection + " AND ");
                }
            }

            //strSearchCriteria.Append(" ServiceTypeCategoryId = " + CategoryId + " AND ");

            //If Filter Expression Is Not Null Or Empty
            if ((!string.IsNullOrEmpty(strSearchCriteria.ToString())))
            {
                //Set Filter Expression
                searchCriteria = strSearchCriteria.ToString().Remove(strSearchCriteria.ToString().Length - 4, 4);

                //User Log
                DALUtility.InsertUserLog("Search Manage Short Code. Search Criteria - " + searchCriteria, "Student/Search",Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }
            else
            {
                //User Log
                DALUtility.InsertUserLog("Search Manage Short Code. Search Criteria - Nothing", "Student/Search", Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
            }

            try
            {
                List<ModelStudent> lstModelStudent = new List<ModelStudent>();

                BLLStudent objBLLStudent = new BLLStudent();

                lstModelStudent = objBLLStudent.GetStudentList(searchCriteria, Convert.ToInt32(Session[DALVariables.SchoolAccountId].ToString()));
                return PartialView(customview("_SearchResultsPartial", "Student"), lstModelStudent);
            }
            catch (Exception ex)
            {
                
                DALUtility.ErrorLog(ex.Message, "StudentController, Search1");
            }
            return PartialView("Error");
        }

        //Get Student info by Studentid
        [HttpPost]
        public ActionResult ViewStudent(string studentID)
        {
            ViewModelStudent objModelStudent = new ViewModelStudent();
            if (!String.IsNullOrEmpty(studentID.Trim()))
            {
                BLLStudent objBLLStudent = new BLLStudent();

                objModelStudent = objBLLStudent.GetStudentInfoById(Convert.ToDecimal(studentID));

                ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
                ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

            }
            return PartialView(customview("_ViewStudentInformation", "Student"), objModelStudent);
        }
      
        //
        // POST: /EditStudent/
        [HttpPost]
        public ActionResult EditStudent(string studentID)
        {
            ViewModelStudent objModelStudent = new ViewModelStudent();
            if (!String.IsNullOrEmpty(studentID.Trim()))
            {
                BLLStudent objBLLStudent = new BLLStudent();

                objModelStudent = objBLLStudent.GetStudentInfoById(Convert.ToDecimal(studentID));

                ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
                ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

            }
            return PartialView(customview("_EditFormStudent", "Student"), objModelStudent);
        }

        //
        // Custom View Controller
        public string customview(string view, string controller)
        {
            if (string.IsNullOrEmpty(controller))
                controller = Request.RequestContext.RouteData.Values["Controller"].ToString();
            return String.Format("~/Views/Shared/{0}/{1}.cshtml", controller, view);
        }


    }
}
