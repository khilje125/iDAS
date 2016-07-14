using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iDAS.Models;
using iDAS.BLL;
using iDAS.DAL;

namespace iDAS.Controllers
{
    public class StudentDataController : BootstrapBaseController
    {
        //
        // GET: /StudentData/
        public void BindDropdownlist()
        {
            BLLStudent objBLLStudent = new BLLStudent();

            ViewBag.dpClass = objBLLStudent.GetClassDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

            ViewBag.dpSection = objBLLStudent.GetClassSectionDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));
            ViewBag.dpFeeMonths = objBLLStudent.GetFeeMonthDropdown(Convert.ToInt32(Session[DALVariables.SchoolAccountId]));

        }

        [HttpPost]
        public ActionResult CreateStudentInfo(ModelStudent objModelStudent)
        {
            try
            {
                BindDropdownlist();
                BLLStudent objBLLStudent = new BLLStudent();               
                if (ModelState.IsValid)
                {
                   // aModelStu.Status = 1;
                   
                    decimal result = objBLLStudent.AddStudentInfo(objModelStudent);
                    if (result > 0)
                    {
                        Success("Record Added at" + DateTime.Now);
                    }
                    else
                    {
                        Error("Data Submition Error, Plz Correct All Fields and Try Again");
                    }
                }
                Error("Plz Fill out All Fields");
                return PartialView(customview("_StudentInformation", "Student"), objModelStudent);
            }
            catch
            {
                return View();
            }
        }
        private int CheckObjNull(string value)
        {
            int result = 0;
            if (!String.IsNullOrEmpty(value.Trim()))
            {
                result = Convert.ToInt32(value.ToString());
            }
            return result;
        }
        public string customview(string view, string controller)
        {
            if (string.IsNullOrEmpty(controller))
                controller = Request.RequestContext.RouteData.Values["Controller"].ToString();
            return String.Format("~/Views/Shared/{0}/{1}.cshtml", controller, view);
        }
    }
}
