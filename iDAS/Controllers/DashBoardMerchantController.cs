using iDAS.BLL;
using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace iDAS.Controllers
{
    public class DashBoardMerchantController : BootstrapBaseController
    {

        BLLPaymentService objBLLPaymentService = new BLLPaymentService();
        BLLMerchantPages objBLLMerchantPages = new BLLMerchantPages();

        // GET: /PaymentServiceList/
        public PartialViewResult PaymentServiceList()
        {
            try
            {
                List<ModelPaymentService> lstModelPaymentService = new List<ModelPaymentService>();
                objBLLPaymentService = new BLLPaymentService();
                lstModelPaymentService = objBLLPaymentService.MerchantPaymentService(Convert.ToDecimal(Session[DALVariables.UserAccountId]));
                return PartialView(customview("_PaymentServiceList", "User"), lstModelPaymentService);

            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "PaymentServiceList, DashBoardMerchant");
            }
            return PartialView();
        }

        // GET: /PaymentServiceList/
        public PartialViewResult MerchantResponseURL()
        {
            try
            {
                ModelLandingURL objModelLandingURL = new ModelLandingURL();
                objBLLMerchantPages = new BLLMerchantPages();
                objModelLandingURL = objBLLMerchantPages.GetMerchantResponseURL(Convert.ToDecimal(Session[DALVariables.UserAccountId]));
                return PartialView(customview("_MerchantResponseURL", "User"), objModelLandingURL);

            }
            catch (Exception ex)
            {
                DALUtility.ErrorLog(ex.Message, "MerchantResponseURL, DashBoardMerchant");
            }
            return PartialView();
        }

        //
        // Custom View Controller
        public string customview(string view, string controller)
        {
            if (string.IsNullOrEmpty(controller))
                controller = Request.RequestContext.RouteData.Values["Controller"].ToString();
            return String.Format("~/Views/Shared/{0}/{1}.cshtml", controller, view);
        }

    }
}
