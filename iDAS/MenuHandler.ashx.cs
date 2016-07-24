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
    /// Summary description for MenuHandler
    /// </summary>
    public class MenuHandler : IHttpHandler
    {

        BLLAdminPages objBLLAdminPages = new BLLAdminPages();
        public void ProcessRequest(HttpContext context)
        {
            List<ModelLeftMenu> LeftMenu = new List<ModelLeftMenu>();
            LeftMenu = objBLLAdminPages.LeftMenu();
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