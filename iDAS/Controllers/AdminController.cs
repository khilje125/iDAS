using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iDAS.Controllers
{
    public class AdminController : BootstrapBaseController
    {
        BLLAdminUser bllAdminUser = new BLLAdminUser();
        ModelAdminUser objModelAdminUser = new ModelAdminUser();
        //
        // GET: /Admin/Index

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Login

        public ActionResult Login()
        {
            objModelAdminUser = new ModelAdminUser();
            return View(objModelAdminUser);
        }

        // POST: /Login/
        [HttpPost]
        public ActionResult Login(string UserEmail, string UserPassword, string RememberMe)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(UserEmail.Trim()))
                {
                    Error("UserEmail not filled not correctly");
                    //ModelState.AddModelError("", "UserEmail not filled not correctly");
                    return View();
                }
                else if (string.IsNullOrEmpty(UserPassword.Trim()))
                {
                    Error("Must fill password field");
                    //ModelState.AddModelError("", "Must fill password field");
                    return View();
                }

                try
                {
                    ModelAdminUser objAdminUser = new ModelAdminUser();
                    DataTable userDetails = new DataTable();
                    userDetails = bllAdminUser.GetAdminUserDetailsByLogin(UserEmail, UserPassword);
                    if (userDetails.Rows.Count > 0)
                    {
                        objAdminUser.UserAccountNo = Convert.ToInt32(userDetails.Rows[0]["intAdminUserId"]);
                        objAdminUser.UserName = Convert.ToString(userDetails.Rows[0]["strAdminUserName"]);
                        objAdminUser.UserFirstName = Convert.ToString(userDetails.Rows[0]["strAdminUserFirstName"]);
                        objAdminUser.UserLastName = Convert.ToString(userDetails.Rows[0]["strAdminUserLastName"]);
                        objAdminUser.UserEmail = Convert.ToString(userDetails.Rows[0]["strAdminUserEmail"]);
                        objAdminUser.UserProfileImage = Convert.ToString(userDetails.Rows[0]["ProfileLogo"]);

                        Session[DALVariables.UserAccountId] = objAdminUser.UserAccountNo;
                        Session[DALVariables.UserEmail] = objAdminUser.UserEmail;
                        Session[DALVariables.UserName] = objAdminUser.UserFirstName + " " + objAdminUser.UserLastName;
                        Session[DALVariables.ProfileImage] = objAdminUser.UserName;
                        return RedirectToAction("Index", "Admin");
                    }
                    ModelState.AddModelError("", "No User Found ! , Re-check login details");
                    return View();

                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "AdminController, Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Fill form Correctly; Please correct to continue!");
            }
            return View();
        }


        // GET: /Logout/
        public ActionResult Logout()
        {
            try
            {
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("Login", "Admin");
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "AdminController, Logout");
            }
            return View();
        }

        public ActionResult MerchantProfile()
        {
            objModelAdminUser = new ModelAdminUser();
            return View(objModelAdminUser);

        }
        public ActionResult NewPaymentService()
        {
            objModelAdminUser = new ModelAdminUser();
            return View(objModelAdminUser);
        }
    }
}
