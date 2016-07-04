using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iDAS.Models;
using iDAS.BLL;

namespace iDAS.Controllers
{
    public class StudentDataController : BootstrapBaseController
    {
        //
        // GET: /StudentData/


        [HttpPost]
        public ActionResult CreateStudentInfo(ModelStudent objModelStudent)
        {
            try
            {
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

                return PartialView(customview("_StudentInformation", "Student"), objModelStudent);
            }
            catch
            {
                return View();
            }
        }
        public string customview(string view, string controller)
        {
            if (string.IsNullOrEmpty(controller))
                controller = Request.RequestContext.RouteData.Values["Controller"].ToString();
            return String.Format("~/Views/Shared/{0}/{1}.cshtml", controller, view);
        }
    }
}
