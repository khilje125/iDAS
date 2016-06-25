using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace iDAS.BLL
{
    public class BLLSchoolUser
    {


        #region "School User"

        public DataTable GetSchoolUserDetailsByLogin(string strSchoolAccountId, string strUserEmail, string strUserPassword)
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@SchoolAccountId", strSchoolAccountId);
            param[1] = new SqlParameter("@UserEmail", strUserEmail);
            param[2] = new SqlParameter("@UserPassword", EncryptDecrypt.Encrypt(strUserPassword));
            return DALCommon.GetDataUsingDataTable("[sp_Admin_CheckUserAccountLogin]", param);
        }

        public decimal RegisterNewMerchantUser(ModelMerchantUser objModelMerchantUser)
        {
            decimal result = 0;
            string getActivationCode = DALUtility.GetEmailActivationCode();
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@MerchantNameEN", objModelMerchantUser.MerchantNameEN);
            param[1] = new SqlParameter("@MerchantEmail", objModelMerchantUser.MerchantEmail);
            param[2] = new SqlParameter("@MerchantPassword", EncryptDecrypt.Encrypt(objModelMerchantUser.MerchantPassword));
            param[3] = new SqlParameter("@strAdminUserMobile", objModelMerchantUser.MerchantMobileNumber);
            param[4] = new SqlParameter("@CompanyNameEN", objModelMerchantUser.CompanyNameEN);
            param[5] = new SqlParameter("@sCompanyLogo", objModelMerchantUser.CompanyLogo);
            param[6] = new SqlParameter("@AccountStatus", 1);
            param[7] = new SqlParameter("@ActivationCodeByEmail", getActivationCode);

            result = DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_InsertMerchantUser]", param);
            if (result > 0)
            {
                string MerchantKey = EncryptDecrypt.Encrypt(DALUtility.GetEmailActivationCode());
                string MerchantReferenceID = EncryptDecrypt.GenerateHashCode(result + MerchantKey + System.DateTime.Now);
                if (UpdateMercharAPIKeys(result, MerchantKey, MerchantReferenceID) > 0)
                {
                    SendActivationCode(getActivationCode, objModelMerchantUser.MerchantEmail);
                }
            }
            return result;
        }

        private decimal UpdateMercharAPIKeys(decimal MerchantAccountNo, string MerchantKey, string MerchantReferenceId)
        {
            decimal result = 0;
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);
            param[1] = new SqlParameter("@MerchantKey", MerchantKey);
            param[2] = new SqlParameter("@MerchantReferenceID", MerchantReferenceId);

            result = DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_UpdateMerchantAPIKeys]", param);
            return result;
        }

        public decimal SendPasswordRecoveryKey(string MerchantEmailID)
        {
            decimal result = 0;
            string getActivationCode = DALUtility.GetEmailActivationCode();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@MerchantEmail", MerchantEmailID);
            param[1] = new SqlParameter("@PasswordRecoveryKey", getActivationCode);

            result = DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_UpdateMerchantFogotPasswordKey]", param);
            if (result > 0)
            {
                SendPasswordRecoveryKey(getActivationCode, MerchantEmailID);
            }
            return result;
        }

        public DataTable MerchantPasswordRecovery(string MerchantEmail, string PasswordKey)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@MerchantEmail", MerchantEmail);
            param[1] = new SqlParameter("@PasswordRecoveryKey", PasswordKey);

            return DALCommon.GetDataUsingDataTable("[sp_Admin_GetMerchantByPasswordKey]", param);
        }
        //MerchantAccountActivate

        public decimal MerchantAccountActivate(string activationCode)
        {
            string getActivationCode = DALUtility.GetEmailActivationCode();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ActivationCodeByEmail", activationCode);

            return DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_UpdateMerchantUserActivate]", param);
        }

        public decimal UpdateMerchantUserPassword(string MerchantEmail, string NewPassword)
        {
            string getActivationCode = DALUtility.GetEmailActivationCode();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@MerchantEmail", MerchantEmail);
            param[1] = new SqlParameter("@MerchantPassword", EncryptDecrypt.Encrypt(NewPassword));

            return DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_UpdateMerchantNewPassword]", param);
        }

        #endregion

        public void SendActivationCode(string activationCode, string MerchantEmail)
        {
            StringBuilder aBulider = new StringBuilder();
            aBulider.Append("<html>");
            aBulider.Append("<head></haed>");
            aBulider.Append("<body>");
            aBulider.Append("<h1>");
            aBulider.Append("Payment Service !");
            aBulider.Append("</h1>");
            aBulider.Append("<br />");
            aBulider.Append("<p>");
            aBulider.Append("Your Account activation code is : <strong>" + EncryptDecrypt.Encrypt(activationCode) + "</strong>");
            aBulider.Append("</p>");
            aBulider.Append("<br />");
            aBulider.Append("<p>");
            aBulider.Append("<strong><a href='http://PaymentService.codemader.com/User/Activation?code=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(activationCode)) + "'> Click Here to Activate</a></strong>");
            aBulider.Append("</p>");
            aBulider.Append("<br />");
            aBulider.Append("</body>");
            aBulider.Append("</html>");
            DALUtility.SendEmail(MerchantEmail, ConfigurationManager.AppSettings["FromEmail"].ToString(), "Activation Code", aBulider.ToString(), ConfigurationManager.AppSettings["GmailSMTP"].ToString(), ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
        }

        public void SendPasswordRecoveryKey(string activationCode, string MerchantEmail)
        {
            StringBuilder aBulider = new StringBuilder();
            aBulider.Append("<html>");
            aBulider.Append("<head></haed>");
            aBulider.Append("<body>");
            aBulider.Append("<h1>");
            aBulider.Append("Payment Service !");
            aBulider.Append("</h1>");
            aBulider.Append("<br />");
            aBulider.Append("<p>");
            aBulider.Append("Your are requested to Recover you password <br/> Click given blow link to reset your password.");
            aBulider.Append("</p>");
            aBulider.Append("<br />");
            aBulider.Append("<p>");
            aBulider.Append("<strong><a href='http://PaymentService.codemader.com/User/PasswordSetting?id=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(MerchantEmail)) + "&code=" + HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(activationCode)) + "'> Click Here to Activate</a></strong>");
            aBulider.Append("</p>");
            aBulider.Append("<br />");
            aBulider.Append("</body>");
            aBulider.Append("</html>");
            DALUtility.SendEmail(MerchantEmail, ConfigurationManager.AppSettings["FromEmail"].ToString(), "Activation Code", aBulider.ToString(), ConfigurationManager.AppSettings["GmailSMTP"].ToString(), ConfigurationManager.AppSettings["FromEmail"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
        }
    }
}