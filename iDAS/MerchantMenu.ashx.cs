using iDAS.BLL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace iDAS
{
    /// <summary>
    /// Summary description for MerchantMenu
    /// </summary>
    public class MerchantMenu : IHttpHandler
    {
        BLLMerchantPages objBLLMerchantPages = new BLLMerchantPages();
        public void ProcessRequest(HttpContext context)
        {
            List<ModelLeftMenu> LeftMenu = new List<ModelLeftMenu>();
            LeftMenu = objBLLMerchantPages.LeftMenu();
            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(LeftMenu));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}