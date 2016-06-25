using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace iDAS.Controllers
{
    public class MerchantController : BootstrapBaseController
    {
        BLLSchoolUser objBLLSchoolUser = new BLLSchoolUser();
        ModelMerchantUser objModelMerchantUser = new ModelMerchantUser();

        //
        // GET: /Merchant/

        public ActionResult UpdateMerchantProfile()
        {
            return View();
        }

        //
        // GET: /Merchant/Register
        [HttpPost]
        public ActionResult Register(ModelMerchantUser objModelMerchantUser)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (objModelMerchantUser == null)
                    {
                        return RedirectToAction("Login", "User");
                    }
                    objBLLSchoolUser = new BLLSchoolUser();
                    StringBuilder st = new StringBuilder();
                    decimal result = 0;

                    foreach (string file in Request.Files)
                    {
                        HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                        if (hpf.ContentLength == 0)
                            continue;

                        string savedFileName = Path.Combine(Server.MapPath("~/Content/MerchantLogo"), Path.GetFileName(hpf.FileName));
                        hpf.SaveAs(savedFileName); // Save the file
                        objModelMerchantUser.CompanyLogo = hpf.FileName;
                    }

                    result = objBLLSchoolUser.RegisterNewMerchantUser(objModelMerchantUser);
                    if (result > 0)
                    {

                        Success(string.Format("{0} was successfully registered", objModelMerchantUser.MerchantEmail));

                        return RedirectToAction("Login", "User");
                    }
                    else if (result == -2)
                    {
                        Attention(string.Format("{0} Email alraedy registered. Please use new Email Addess. If forgot password try to recover your account.", objModelMerchantUser.MerchantEmail));

                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        Error(string.Format("Error occured while Registering {0}", objModelMerchantUser.MerchantEmail));
                        return RedirectToAction("Register", "User", objModelMerchantUser);
                    }
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "AddRegister, Merchant");
                }
            }
            else
            {
                Error("Check error of form; Please correct to continue!");
                //ModelState.AddModelError("", "Check error of form; Please correct to continue!");
            }
            return RedirectToAction("Login", "User");
        }

        //
        // POST: /Merchant/ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(string EmailAddessForgot)
        {
            if (!String.IsNullOrEmpty(EmailAddessForgot))
            {
                try
                {
                    objBLLSchoolUser = new BLLSchoolUser();
                    StringBuilder st = new StringBuilder();
                    decimal result = 0;

                    result = objBLLSchoolUser.SendPasswordRecoveryKey(EmailAddessForgot);
                    if (result > 0)
                    {

                        Success(string.Format("Password recovery data send in email to your account. Visit your mail box for futher Processing."));

                        return RedirectToAction("Login", "User");
                    }
                    else if (result == -2)
                    {
                        Error(string.Format("Your {0} is not valid email address. Please enter correct email from which you registered before.", objModelMerchantUser.MerchantEmail));

                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        Error(string.Format("Error occured while Recovering your password {0}", objModelMerchantUser.MerchantEmail));
                        return RedirectToAction("Login", "User");
                    }
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "ForgotPassword, Merchant");
                }
            }
            else
            {
                Error(string.Format("Please enter correct email then Submit for processing."));
            }
            return RedirectToAction("Login", "User");
        }

        // POST: /Merchant/SetupNewPassword
        [HttpPost]
        public ActionResult SetupNewPassword(string MerchantEmail, string MerchantPassword)
        {
            if (!String.IsNullOrEmpty(MerchantEmail.Trim()) && !String.IsNullOrEmpty(MerchantPassword.Trim()))
            {
                try
                {
                    StringBuilder st = new StringBuilder();
                    decimal result = 0;

                    result = objBLLSchoolUser.UpdateMerchantUserPassword(MerchantEmail, MerchantPassword);
                    if (result > 0)
                    {

                        Success(string.Format("Password successfully recovered. Please login with your new password."));

                        return RedirectToAction("Login", "User");
                    }
                    else if (result == -2)
                    {
                        Error(string.Format("Not valid user. Please contact site Administrator for further details", MerchantEmail));

                        return RedirectToAction("Login", "User");
                    }
                    else
                    {
                        Error(string.Format("Error occured while Recovering your password {0}", MerchantEmail));
                        return RedirectToAction("Login", "User");
                    }
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "ForgotPassword, Merchant");
                }
            }
            else
            {
                Error(string.Format("Please fill form correctly then Submit for processing."));
            }
            return RedirectToAction("Login", "User");
        }

        // POST: /Merchant/SubscribeService
        [HttpGet]
        public ActionResult SubscribeService(string ServiceNo)
        {
            if (Session[DALVariables.UserAccountId] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (!String.IsNullOrEmpty(ServiceNo.Trim()))
            {
                try
                {
                    StringBuilder st = new StringBuilder();
                    decimal result = 0;
                    BLLPaymentService objBLLPaymentService = new BLLPaymentService();
                    result = objBLLPaymentService.UpdateMerchantServiceStatus(Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()), Convert.ToInt32(EncryptDecrypt.Decrypt(ServiceNo.Trim())));
                    if (result > 0)
                    {
                        Success(string.Format("Service status updated successfully"));

                        return RedirectToAction("Home", "User");
                    }
                    else if (result == -2)
                    {
                        Error(string.Format("Service status not updated"));

                        return RedirectToAction("Home", "User");
                    }
                    else
                    {
                        Error(string.Format("Error occured while updating service status"));
                        return RedirectToAction("Login", "User");
                    }
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "SubscribeService, Merchant");
                }
            }
            else
            {
                Error(string.Format("Error occured while updating service status."));
            }
            return RedirectToAction("Login", "User");
        }

        //
        // GET: /Merchant/LandingURL
        [HttpPost]
        public ActionResult LandingURL(ModelLandingURL objModelLandingURL)
        {
            if (Session[DALVariables.UserAccountId] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    BLLPaymentService objBLLPaymentService = new BLLPaymentService();
                    StringBuilder st = new StringBuilder();
                    decimal result = 0;
                    objModelLandingURL.MerchantAccountNo = Convert.ToInt32(Session[DALVariables.UserAccountId]);
                    result = objBLLPaymentService.UpdateMerchantCallBackURL(objModelLandingURL);
                    if (result > 0)
                    {
                        Success(string.Format("Response URL successfully updated.Now you can activate your payment service"));

                        return RedirectToAction("Home", "User");
                    }
                    else
                    {
                        Error(string.Format("Error occured while updating record"));
                        return RedirectToAction("Home", "User");
                    }
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "LandingURL, Merchant");
                }
            }
            else
            {
                Error("Check error of form; Please correct to continue!");
                return RedirectToAction("Home", "User");
                //ModelState.AddModelError("", "Check error of form; Please correct to continue!");
            }
            return RedirectToAction("Login", "User");
        }
    }   
}
