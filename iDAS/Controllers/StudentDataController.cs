using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iDAS.Models;
using iDAS.BLL;
using iDAS.DAL;
using System.Text;

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
        public ActionResult CreateStudentInfo(ViewModelStudent objModelStudent)
        {
            try
            {
                BindDropdownlist();
                BLLStudent objBLLStudent = new BLLStudent();
                if (ModelState.IsValid)
                {
                    objModelStudent.Status = 1;

                    objModelStudent.P = "N/A";

                    if (Request.Files.Count > 0)
                    {
                        if (Request.Files["Simage"].ContentLength > 0)
                        {
                            var image = System.Drawing.Image.FromStream(Request.Files["Simage"].InputStream);
                            var rName = SecureRandomString(15) + System.IO.Path.GetExtension(Request.Files["Simage"].FileName);
                            image.Save(Server.MapPath("/StudentImage/" + rName + ""));
                            objModelStudent.Simage = rName;
                        }
                    }
                    else
                    {

                        Error("Student Image not found");
                        return PartialView(customview("_StudentInformation", "Student"), objModelStudent);
                    }
                    decimal result = objBLLStudent.AddStudentInfo(objModelStudent);
                    if (result > 0)
                    {
                        Success("Record Added at" + DateTime.Now);
                        return PartialView(customview("_StudentInformation", "Student"), objModelStudent);
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

        [HttpPost]
        public ActionResult UpdateStudentInfo(ViewModelStudent objModelStudent)
        {
            try
            {
                BindDropdownlist();
                BLLStudent objBLLStudent = new BLLStudent();
                if (ModelState.IsValid)
                {
                    objModelStudent.Status = 1;

                    decimal result = objBLLStudent.UpdateStudent(objModelStudent);
                    if (result > 0)
                    {
                        Success("Update Record at" + DateTime.Now);
                        ModelState.Clear();
                        return View();
                    }
                    else
                    {
                        Error("Data Submition Error, Plz Correct All Fields and Try Again");
                    }


                }
                Error("Plz Fill out All Fields");
                return PartialView(customview("_EditFormStudent", "Student"), objModelStudent);
            }
            catch
            {
                return View();
            }
        }
        public static string SecureRandomString(int length, string allowedChars = "abcdefghij2323klmnoxcasdasdpqrszxct232323uvwxyzABCD23232323EFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            if (length < 0) throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
            if (string.IsNullOrEmpty(allowedChars)) throw new ArgumentException("allowedChars may not be empty.");

            const int byteSize = 0x100;
            var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
            if (byteSize < allowedCharSet.Length) throw new ArgumentException(String.Format("allowedChars may contain no more than {0} characters.", byteSize));

            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                var result = new StringBuilder();
                var buf = new byte[128];
                while (result.Length < length)
                {
                    rng.GetBytes(buf);
                    for (var i = 0; i < buf.Length && result.Length < length; ++i)
                    {
                        var outOfRangeStart = byteSize - (byteSize % allowedCharSet.Length);
                        if (outOfRangeStart <= buf[i]) continue;
                        result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                    }
                }
                return result.ToString();
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
