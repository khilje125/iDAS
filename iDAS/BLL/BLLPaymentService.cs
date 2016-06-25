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
    public class BLLPaymentService
    {
        public List<ModelPaymentService> MerchantPaymentService(decimal MerchantAccountNo)
        {
            List<ModelPaymentService> lstModelPaymentService = new List<ModelPaymentService>();
            bool callBackURLStatus = false;
            DataTable callBackResponse = new DataTable();
            SqlParameter[] paramCallBack = new SqlParameter[1];

            paramCallBack[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);

            callBackResponse = DALCommon.GetDataUsingDataTable("[sp_Admin_GetCallBackURLStatus]", paramCallBack);
            if (callBackResponse.Rows.Count > 0)
            {
                callBackURLStatus = Convert.ToBoolean(callBackResponse.Rows[0]["CallBackUrlStatus"]);
            }

            DataTable serviceTable = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);

            serviceTable = DALCommon.GetDataUsingDataTable("[sp_Admin_GetMerchantServiceList]", param);
            if (serviceTable.Rows.Count > 0)
            {
                foreach (DataRow item in serviceTable.Rows)
                {

                    ModelPaymentService objModelPaymentService = new ModelPaymentService();
                    objModelPaymentService.srtPaymentServiceTypeID = EncryptDecrypt.Encrypt(item["PaymentServiceTypeID"].ToString());
                    objModelPaymentService.ServiceLogo = Convert.ToString(item["ServiceLogo"]);
                    objModelPaymentService.IsMerchantSubscribe = Convert.ToBoolean(item["IsMerchantSubscribe"]);
                    objModelPaymentService.PaymentServiceTypeName = Convert.ToString(item["PaymentServiceTypeName"]);
                    objModelPaymentService.intMerchantStatus = Convert.ToInt32(item["Status"]);
                    objModelPaymentService.IsMerchantCallBackURLNotNull = callBackURLStatus;
                    lstModelPaymentService.Add(objModelPaymentService);
                }
            }
            return lstModelPaymentService;
        }

        //UpdateMerchantServiceStatus

        public decimal UpdateMerchantServiceStatus(decimal MerchantAccountNo, int ServiceId)
        {
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);
            param[1] = new SqlParameter("@PaymentServiceTypeID", ServiceId);

            return DALCommon.ExecuteNonQueryReturnIdentity("[sp_Admin_UpdateMerchantServiceStatus]", param);
        }

        public decimal UpdateMerchantCallBackURL(ModelLandingURL objModelLandingURL)
        {
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@MerchantAccountNo", objModelLandingURL.MerchantAccountNo);
            param[1] = new SqlParameter("@CallBackSuccessUrl", objModelLandingURL.MerchantSuccessURL);
            param[2] = new SqlParameter("@CallBackRejectUrl", objModelLandingURL.MerchantFailURL);

            return DALCommon.ExecuteNonQuery("[sp_Admin_UpdateMerchantResponseURL]", param);
        }

    }
}