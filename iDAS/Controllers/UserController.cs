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
    public class UserController : BootStrapBaseLoginController
    {
        BLLSchoolUser objBLLSchoolUser = new BLLSchoolUser();
        ModelSchoolUserAccount objModelSchoolUserAccount = new ModelSchoolUserAccount();
        BLLMerchantPages objBLLMerchantPages = new BLLMerchantPages();
        //
        // GET: /User/Login

        public ActionResult Login()
        {
            objModelSchoolUserAccount = new ModelSchoolUserAccount();
            return View(objModelSchoolUserAccount);
        }

        // POST: /Login/
        [HttpPost]
        public ActionResult Login(string SchoolAccountId, string UserEmail, string UserPassword)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(UserEmail.Trim()))
                {
                    Error("User Email not filled not correctly");
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
                    ModelSchoolUserAccount objSchoolUserAccount = new ModelSchoolUserAccount();
                    DataTable userDetails = new DataTable();
                    userDetails = objBLLSchoolUser.GetSchoolUserDetailsByLogin(SchoolAccountId, UserEmail.Trim(), UserPassword.Trim());
                    if (userDetails.Rows.Count > 0)
                    {
                        objSchoolUserAccount.SchoolAccountId = Convert.ToInt32(userDetails.Rows[0]["SchoolAccountId"]);
                        objSchoolUserAccount.UserAccountId = Convert.ToInt32(userDetails.Rows[0]["UserAccountId"]);
                        objSchoolUserAccount.UserFName = Convert.ToString(userDetails.Rows[0]["UserFName"]);
                        objSchoolUserAccount.UserLName = Convert.ToString(userDetails.Rows[0]["UserLName"]);
                        objSchoolUserAccount.UserEmail = Convert.ToString(userDetails.Rows[0]["UserEmail"]);
                        objSchoolUserAccount.UserProfileImage = Convert.ToString(userDetails.Rows[0]["UserProfileImage"]);
                        objSchoolUserAccount.UseAccountStatus = Convert.ToInt32(userDetails.Rows[0]["UseAccountStatus"]);
                        objSchoolUserAccount.SchoolAccount.SchoolProfileLogo = Convert.ToString(userDetails.Rows[0]["UserProfileImage"]);
                        
                        if (objSchoolUserAccount.UseAccountStatus == 2)
                        {
                            return RedirectToAction("AccountInfo", "User");
                        }
                        else if (objSchoolUserAccount.UseAccountStatus == 4)
                        {
                            return RedirectToAction("AccountBlocked", "User");
                        }
                        else if (objSchoolUserAccount.UseAccountStatus == 3)
                        {
                            Session[DALVariables.SchoolAccountId] = objSchoolUserAccount.SchoolAccountId;
                            Session[DALVariables.UserAccountId] = objSchoolUserAccount.UserAccountId;
                            Session[DALVariables.UserName] = objSchoolUserAccount.UserFName + " " + objSchoolUserAccount.UserLName;
                            Session[DALVariables.UserEmail] = objSchoolUserAccount.UserEmail;
                            Session[DALVariables.SchoolProfileLogo] = objSchoolUserAccount.SchoolAccount.SchoolProfileLogo;
                            Session[DALVariables.ProfileImage] = objSchoolUserAccount.UserProfileImage;
                            Session[DALVariables.AccountType] = 2;

                            return RedirectToAction("Home", "User");
                        }
                        else
                        {
                            return RedirectToAction("AccountPendingVerify", "User");
                        }
                    }
                    Error("No User Found ! , Re-check login details");
                    //ModelState.AddModelError("", "No User Found ! , Re-check login details");
                    return View();

                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "UserController, Login");
                }
            }
            else
            {
                Error("No User Found ! , Re-check login details");
                //ModelState.AddModelError("", "Fill form Correctly; Please correct to continue!");
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
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "UserController, Logout");
            }
            return View();
        }

        //
        // GET: /User/Home

        public ActionResult Home()
        {
            if (Session[DALVariables.UserAccountId] == null)
            {
                return RedirectToAction("Login", "User");
            }
            objBLLMerchantPages = new BLLMerchantPages();
            objModelSchoolUserAccount = new ModelSchoolUserAccount();
            DataTable merchantData = new DataTable();
            merchantData = objBLLMerchantPages.GetMerchantAPIKey(Convert.ToDecimal(Session[DALVariables.UserAccountId]));
            if (merchantData.Rows.Count > 0)
            {
                //objModelSchoolUserAccount.MerchantKey = merchantData.Rows[0]["MerchantKey"].ToString();
                //objModelSchoolUserAccount.MerchantReferenceID = merchantData.Rows[0]["MerchantReferenceID"].ToString();
            }
            return View(objModelSchoolUserAccount);
        }

        //
        // GET: /User/PasswordSetting
        [HttpGet]
        public ActionResult PasswordSetting(string id, string code)
        {
            if (!String.IsNullOrEmpty(id.Trim()) && !String.IsNullOrEmpty(code.Trim()))
            {
                try
                {
                    objBLLSchoolUser = new BLLSchoolUser();
                    DataTable aMerchantUser = new DataTable();
                    aMerchantUser = objBLLSchoolUser.MerchantPasswordRecovery(EncryptDecrypt.Decrypt(id), EncryptDecrypt.Decrypt(code));
                    if (aMerchantUser.Rows.Count > 0)
                    {
                        objModelSchoolUserAccount = new ModelSchoolUserAccount();
                        objModelSchoolUserAccount.UserEmail = aMerchantUser.Rows[0]["UserEmail"].ToString();
                        return View(objModelSchoolUserAccount);
                    }
                    else
                    {
                        Error("Link expired or no user found,Please try again to Recover your password");
                    }
                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "UserController, PasswordSetting");
                }
            }
            else
            {
                Error("Link expired or no user found,Please try again to Recover your password");
            }
            return RedirectToAction("Login", "User");
        }

        //
        // GET: /User/Activation
        [HttpGet]
        public ActionResult Activation(string code)
        {
            if (!String.IsNullOrEmpty(code.Trim()))
            {
                try
                {
                    decimal result = 0;
                    objBLLSchoolUser = new BLLSchoolUser();

                    result = objBLLSchoolUser.MerchantAccountActivate(EncryptDecrypt.Decrypt(code));
                    if (result > 0)
                    {
                        return View();
                    }
                    else
                    {
                        Error("No user found !");
                    }
                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    DALUtility.ErrorLog(ex.Message, "UserController, Activation");
                }
            }
            else
            {
                Error("No user found !");
            }
            return RedirectToAction("Login", "User");
        }

        //
        // GET: /User/AccountInfo
        public ActionResult AccountInfo()
        {
            return View();
        }

        //
        // GET: /User/AccountBlocked
        public ActionResult AccountBlocked()
        {
            return View();
        }

        //
        // GET: /User/AccountPendingVerify
        public ActionResult AccountPendingVerify()
        {
            return View();
        }

        // POST: /User/TransactionHistory
        [HttpGet]
        public ActionResult TransactionHistory()
        {
            if (Session[DALVariables.UserAccountId] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (!String.IsNullOrEmpty("Search Area"))
            {
                try
                {
                    List<ModelTransactionByCreditCard> lstModelTransactionByCreditCard = new List<ModelTransactionByCreditCard>();

                    BLLMerchantPages objBLLMerchantPages = new BLLMerchantPages();
                    lstModelTransactionByCreditCard = objBLLMerchantPages.GetPaymeantTransactionHistory(Convert.ToInt32(Session[DALVariables.UserAccountId].ToString()));
                    if (lstModelTransactionByCreditCard.Count > 0)
                    {
                        return View(lstModelTransactionByCreditCard);
                    }
                    else
                    {
                        return View(lstModelTransactionByCreditCard);
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

    }
}
