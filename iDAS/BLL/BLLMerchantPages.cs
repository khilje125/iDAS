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
    public class BLLMerchantPages
    {
        public DataTable GetMerchantAPIKey(decimal MerchantAccountNo)
        {
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);

            return DALCommon.GetDataUsingDataTable("[sp_Admin_GetMerchantAPIKey]", param);
        }

        #region "Get Letf Menu List"
        public List<ModelLeftMenu> LeftMenu()
        {
            DataTable dt = new DataTable();
            dt = DALCommon.GetDataByStoredProcedure("[sp_Admin_GetMerchantLeftMenu]");

            List<ModelLeftMenu> LeftMenu = new List<ModelLeftMenu>();
            List<ModelLeftMenu> LeftMenuReturn = new List<ModelLeftMenu>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ModelLeftMenu objModelLeftMenu = new ModelLeftMenu();
                    objModelLeftMenu.Id = Convert.ToInt32(item["MenuId"]);
                    objModelLeftMenu.MenuText = Convert.ToString(item["MenuTitleEn"]);
                    objModelLeftMenu.URL = Convert.ToString(item["MerchantPageURL"]);
                    objModelLeftMenu.CssClass = Convert.ToString(item["CssClass"]);
                    objModelLeftMenu.ParentId = item["ParentId"] != DBNull.Value ? Convert.ToInt32(item["ParentId"]) : (int?)null;
                    objModelLeftMenu.IsActive = Convert.ToBoolean(item["IsActive"]);
                    LeftMenu.Add(objModelLeftMenu);
                }
                LeftMenuReturn = GetMenuTree(LeftMenu, null);
            }
            return LeftMenuReturn;
        }
        private List<ModelLeftMenu> GetMenuTree(List<ModelLeftMenu> list, int? parentId)
        {
            return list.Where(x => x.ParentId == parentId).Select(x => new ModelLeftMenu()
            {
                Id = x.Id,
                MenuText = x.MenuText,
                URL = x.URL,
                CssClass = x.CssClass,
                ParentId = x.ParentId,
                IsActive = x.IsActive,
                List = GetMenuTree(list, x.Id)
            }).ToList();

        }
        #endregion

        public ModelLandingURL GetMerchantResponseURL(decimal MerchantAccountNo)
        {
            DataTable tblMerchantURL = new DataTable();
            ModelLandingURL objModelLandingURL = new ModelLandingURL();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);

            tblMerchantURL = DALCommon.GetDataUsingDataTable("[sp_Admin_GetMerchantResponseUrl]", param);
            if (tblMerchantURL.Rows.Count > 0)
            {
                objModelLandingURL.MerchantAccountNo = Convert.ToInt32(tblMerchantURL.Rows[0]["MerchantAccountNo"]);
                objModelLandingURL.MerchantSuccessURL = Convert.ToString(tblMerchantURL.Rows[0]["CallBackSuccessUrl"]);
                objModelLandingURL.MerchantFailURL = Convert.ToString(tblMerchantURL.Rows[0]["CallBackRejectUrl"]);
            }
            return objModelLandingURL;
        }

        public List<ModelTransactionByCreditCard> GetPaymeantTransactionHistory(decimal MerchantAccountNo)
        {
            List<ModelTransactionByCreditCard> lstModelTransactionByCreditCard = new List<ModelTransactionByCreditCard>();
            DataTable tblMerchantTransactionHistory = new DataTable();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@MerchantAccountNo", MerchantAccountNo);

            tblMerchantTransactionHistory = DALCommon.GetDataUsingDataTable("[sp_Admin_GetMerchantTransactionHistory]", param);
            if (tblMerchantTransactionHistory.Rows.Count > 0)
            {
                foreach (DataRow aTransaction in tblMerchantTransactionHistory.Rows)
                {
                    ModelTransactionByCreditCard objModelTransactionByCreditCard = new ModelTransactionByCreditCard();
                    objModelTransactionByCreditCard.TransactionID = Convert.ToInt32(aTransaction["TransactionID"]);
                    objModelTransactionByCreditCard.UserID = aTransaction["UserID"].ToString();
                    objModelTransactionByCreditCard.ProductID = aTransaction["ProductID"].ToString();
                    objModelTransactionByCreditCard.Amount = Convert.ToDecimal(aTransaction["Amount"]);
                    objModelTransactionByCreditCard.Currency = Convert.ToString(aTransaction["Currency"]);
                    objModelTransactionByCreditCard.Language = Convert.ToString(aTransaction["Language"]);
                    objModelTransactionByCreditCard.TransactionDetails = Convert.ToString(aTransaction["TransactionDetails"]);
                    objModelTransactionByCreditCard.TransactionDate = Convert.ToDateTime(aTransaction["TransactionDate"]);
                    objModelTransactionByCreditCard.Status = Convert.ToInt32(aTransaction["Status"]);
                    objModelTransactionByCreditCard.StatusText = Convert.ToString(aTransaction["StatusText"]);
                    objModelTransactionByCreditCard.PaymentType = Convert.ToString(aTransaction["PaymentType"]);
                    lstModelTransactionByCreditCard.Add(objModelTransactionByCreditCard);
                }
            }
            return lstModelTransactionByCreditCard;
        }
    }
}