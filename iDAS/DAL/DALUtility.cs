using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace iDAS.DAL
{
    public class DALUtility
    {
        #region "Error Log"

        public static void ErrorLog(string ErrorMessage, string ErrorPageName = "", bool WillRedirectErrorPage = false)
        {
            int recCount = 0;
            // SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings("RabtAdminDbCon").ConnectionString);
            string DbCon = Convert.ToString(ConfigurationManager.ConnectionStrings["DASAdminDbCon"]);

            SqlConnection cn = new SqlConnection(DbCon);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_InsertWebsiteSystemError";

            cmd.Parameters.AddWithValue("@ErrorMessage", ErrorMessage);
            //cmd.Parameters.AddWithValue("@ErrorPageName", HttpContext.Current.Request.Url.AbsoluteUri + "," + ErrorPageName);
            cmd.Parameters.AddWithValue("@ErrorPageName", ErrorPageName);

            cmd.Parameters.AddWithValue("@ErrorStatus", WillRedirectErrorPage);

            try
            {
                if ((cn.State == ConnectionState.Closed))
                {
                    cn.Open();
                }

                recCount = cmd.ExecuteNonQuery();


            }
            catch (Exception)
            {
            }
            finally
            {
                cmd = null;
                if ((cn.State == ConnectionState.Open))
                {
                    cn.Close();
                }
            }

            //  HttpContext.Current.Response.Redirect("frmError.asp", False)
        }
        #endregion

        #region "User Log"
        public static int InsertUserLog(string CodeBlock, string ActivityOnPage, decimal UserAccountNo)
        {
            SqlDecimal sqlDecimal = SqlDecimal.Null;

            SqlParameter[] param = new SqlParameter[5];

            if ((UserAccountNo > 0))
            {
                param[0] = new SqlParameter("@UserAccountNo", Convert.ToDecimal(UserAccountNo));
            }
            else
            {
                param[0] = new SqlParameter("@UserAccountNo", sqlDecimal);
            }

            param[1] = new SqlParameter("@UserActivityOnSite", ActivityOnPage);
            // param[2] = new SqlParameter("@SitePageName", HttpContext.Current.Request.Url.AbsoluteUri);
            param[2] = new SqlParameter("@SitePageName", "");
            param[3] = new SqlParameter("@BlockName", CodeBlock);
            param[4] = new SqlParameter("@UserLogByIP", GetIP());

            return DALCommon.ExecuteNonQuery("sp_User_InsertUserLog", param);
        }
        #endregion

        #region "IP Address"
        public static string GetIP()
        {
            string ipAddress = string.Empty;

            //get array of ip address from proxy
            //ipAddress = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR");
            ipAddress = "";

            //if array of ip is not empty
            if ((!string.IsNullOrEmpty(ipAddress)))
            {
                //split from array of ip
                string[] ipRange = ipAddress.Split(',');
                //get the ip address
                ipAddress = ipRange[0];
                //if the proxy ip is empty then
            }
            else
            {
                //get ip address from remote address
                //ipAddress = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR");
                ipAddress = "";
            }

            return ipAddress;
        }
        #endregion

        #region "Generate Email Activation Code"
        public static string GetEmailActivationCode()
        {
        GenerateEmailActivationCode:

            string strGuid = string.Empty;
            try
            {
                //get new activation code
                strGuid = System.Guid.NewGuid().ToString();
                strGuid = strGuid.Replace("-", string.Empty);
                strGuid = strGuid.Substring(0, 10).ToString();

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, "DALUtility.vb, GetEmailActivationCode");
            }

            //Declare Sql Parameter
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ActivationCodeByEmail", EncryptDecrypt.Encrypt(strGuid));

            //Check This Activation Code Is Exists Then
            if ((DALCommon.DataExistsByQuery("SELECT MerchantAccountNo FROM dbo.MerchantAccount WHERE ActivationCodeByEmail LIKE @ActivationCodeByEmail OR PasswordRecoveryKey LIKE @ActivationCodeByEmail", param)))
            {
                goto GenerateEmailActivationCode;
            }
            else
            {
                return strGuid;
            }
        }
        #endregion

        #region "Generate Mobile Activation Code"
        public static string GetMobileActivationCode()
        {
        GenerateMobileActivationCode:

            string strGuid = string.Empty;
            string numbers = null;
            string singleNumberValue = null;
            string strActivationCodeForMobile = string.Empty;

            try
            {
                //declare string builder to get random numbers for 6 digits
                StringBuilder builder = new StringBuilder();
                //declare an object for random
                Random Random = new Random();
                //declare all the numbers
                numbers = "1234567890";

                //Now go for 6 digit numbers
                for (int i = 0; i <= 5; i++)
                {
                    //get the single number
                    singleNumberValue = Convert.ToString(numbers[(Random.Next(0, numbers.Length))]);
                    //append with the previous one
                    builder.Append(singleNumberValue);
                }
                //concatenate all the values 1 String and 5 Digits
                strActivationCodeForMobile = builder.ToString();

            }
            catch (Exception ex)
            {
                ErrorLog(ex.Message, "DALUtility.vb, GetEmailActivationCode");
            }

            //Declare Sql Parameter
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ActivationCodeByMobile", EncryptDecrypt.Encrypt(strActivationCodeForMobile));

            //Check This Activation Code Is Exists Then
            if ((DALCommon.DataExistsByQuery("SELECT UserAccountNo FROM dbo.UserAccount WHERE ActivationCodeByMobile LIKE @ActivationCodeByMobile", param)))
            {
                goto GenerateMobileActivationCode;
            }
            else
            {
                return strActivationCodeForMobile;
            }
        }
        #endregion
        #region "Replace Email String"
        public static string StringReplace(string EmailString, Hashtable hsh)
        {
            if ((hsh != null))
            {
                if (hsh.Count > 0)
                {
                    foreach (string item in hsh.Keys)
                    {
                        EmailString = EmailString.Replace(item, Convert.ToString(hsh[item]));
                        // EmailString = Replace(EmailString, item, Convert.ToString(hsh[item]));
                    }
                }
            }
            return EmailString;
        }
        #endregion

        #region "Email"
        public static int SendEmail(string ToEmail, string fromEmail, string Subject, string Body, string SMTP, string Username, string Password)
        {
            try
            {
                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage(new MailAddress(fromEmail), new MailAddress(ToEmail));
                System.Net.NetworkCredential SmtpUser = new System.Net.NetworkCredential(Username, Password);
                StringBuilder emailHeader = new StringBuilder();
                StringBuilder emailFooter = new StringBuilder();

                //message body and subject property
                mailMessage.Subject = Subject;
                mailMessage.Body = emailHeader.ToString() + Body + emailFooter.ToString();
                mailMessage.IsBodyHtml = true;

                //create smtp client
                SmtpClient smtpMail = new SmtpClient();

                //assign smtp properties
                smtpMail.Host = SMTP;
                smtpMail.Port = 587;
                smtpMail.EnableSsl = true;
                smtpMail.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpMail.UseDefaultCredentials = false;
                smtpMail.Credentials = SmtpUser;

                //send mail
                smtpMail.Send(mailMessage);

                return 1;
            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message + "SendEmail");
                return 0;
            }
        }
        #endregion

        #region "Numeric Null Check"
        public static int isNumericDBNULL(string DbValue)
        {
            int _result = 0;
            if (DBNull.Value.Equals(DbValue.Trim()))
            {
                return _result;
            }
            if (!String.IsNullOrEmpty(DbValue.Trim()))
            {
                return Convert.ToInt32(DbValue.Trim());
            }

            return _result;
        }
        #endregion
    }
}