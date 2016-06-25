using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace iDAS.BLL
{
    public class BLLAdminUser
    {

        #region "Insert And Update Admin Web Permission"
        public bool SaveAdminWebFormPermission(int AdminUserNo, Array ArrayOfPermission)
        {
            SqlParameter[] param = new SqlParameter[5];

            //Now Go For One By One Sector
            for (int i = 0; i <= (ArrayOfPermission.Length / 4) - 1; i++)
            {
                param[0] = new SqlParameter("@AdminUserNo", AdminUserNo);
                param[1] = new SqlParameter("@AdminUserWebFormNo", ArrayOfPermission.GetValue(i, 0));
                param[2] = new SqlParameter("@HaveAddPermission", ArrayOfPermission.GetValue(i, 1));
                param[3] = new SqlParameter("@HaveEditPermission", ArrayOfPermission.GetValue(i, 2));
                param[4] = new SqlParameter("@HaveSearchPermission", ArrayOfPermission.GetValue(i, 3));

                //Manage Permission
                DALCommon.ExecuteNonQuery("sp_Admin_UpdateAdminPermission", param);
            }

            return true;
        }
        #endregion

        #region "Get Admin User Details"

        #endregion

        #region "Admin User"
        //new
        public DataTable GetAdminUserDetailsByLogin(string strAdminUserName, string strAdminUserPassword)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@strAdminUserName", strAdminUserName);
            //param[1] = new SqlParameter("@AdminUserPassword", EncryptDecrypt.Encrypt(AdminUserPassword));
            param[1] = new SqlParameter("@strAdminUserPassword", strAdminUserPassword);
            return DALCommon.GetDataUsingDataTable("sp_Admin_CheckAdminLogin", param);
        }

        //new
        public int InsertNewAdminUser(ModelAdminUser objModelAdminUser)
        {
            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@strAdminUserName", objModelAdminUser.UserName);
            param[1] = new SqlParameter("@strAdminUserPassword", objModelAdminUser.UserPassword);
            param[2] = new SqlParameter("@strAdminUserFirstName", objModelAdminUser.UserFirstName);
            param[3] = new SqlParameter("@strAdminUserLastName", objModelAdminUser.UserLastName);
            param[4] = new SqlParameter("@strAdminUserEmail", objModelAdminUser.UserEmail);
            param[5] = new SqlParameter("@strAdminUserMobile", objModelAdminUser.UserMobileNumber);
            param[6] = new SqlParameter("@intAdminUserStatus", objModelAdminUser.UserAccountStatus);
            param[7] = new SqlParameter("@intAdminUserType", objModelAdminUser.UserAccountType);

            return DALCommon.ExecuteNonQuery("[sp_Admin_InsertAdminUser]", param);
        }
        #endregion

        #region "Get Admin User Details"
        public DataTable GetAdminUserDetailsByEmail(string AdminEmail)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@Email", AdminEmail);

            return DALCommon.GetDataUsingDataTable("sp_Admin_GetAdminUserByEmail", param);
        }
        #endregion

        #region "Get Admin User Details"
        public DataTable GetAdminUserDetailsByUserNo(double AdminUserNo)
        {
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@AdminUserNo", AdminUserNo);

            return DALCommon.GetDataUsingDataTable("[sp_Admin_GetAdminDetailsByAdminUserNo]", param);
        }
        #endregion

        #region "Update Password"
        public int UpdateAdminPassword(double AdminUserNo, string Password)
        {
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AdminUserNo", AdminUserNo);
            param[1] = new SqlParameter("@AdminUserPassword", Password);

            return DALCommon.ExecuteNonQuery("[sp_Admin_UpdateAdminUserPassword]", param);
        }
        #endregion


    }
}