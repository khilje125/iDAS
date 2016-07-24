using iDAS.DAL;
using iDAS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace iDAS.BLL
{
    public class BLLAdminPages
    {

        #region "Get Letf Menu List"
        public List<ModelLeftMenu> LeftMenu()
        {
            DataTable dt = new DataTable();
            dt = DALCommon.GetDataByStoredProcedure("[sp_Admin_GetLeftMenu]");

            List<ModelLeftMenu> LeftMenu = new List<ModelLeftMenu>();
            List<ModelLeftMenu> LeftMenuReturn = new List<ModelLeftMenu>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow item in dt.Rows)
                {
                    ModelLeftMenu objModelLeftMenu = new ModelLeftMenu();
                    objModelLeftMenu.Id = Convert.ToInt32(item["intMenuId"]);
                    objModelLeftMenu.MenuText = Convert.ToString(item["strMenuTitleEn"]);
                    objModelLeftMenu.URL = Convert.ToString(item["strAdminPageURL"]);
                    objModelLeftMenu.CssClass = Convert.ToString(item["strCssClass"]);
                    objModelLeftMenu.ParentId = item["intParentId"] != DBNull.Value ? Convert.ToInt32(item["intParentId"]) : (int?)null;
                    objModelLeftMenu.IsActive = Convert.ToBoolean(item["bllIsActive"]);
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
    }
}