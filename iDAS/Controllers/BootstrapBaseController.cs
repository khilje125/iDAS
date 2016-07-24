using iDAS.Common;
using iDAS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;


namespace iDAS.Controllers
{
    public class BootstrapBaseController : Controller 
    {
        public void Attention(string message)
        {
            TempData.Add(Alerts.ATTENTION, message);
        }

        public void Success(string message)
        {
            TempData.Add(Alerts.SUCCESS, message);
        }

        public void Information(string message)
        {
            TempData.Add(Alerts.INFORMATION, message);
        }

        public void Error(string message)
        {
            TempData.Add(Alerts.ERROR, message);
        }

        public void SessionCheck(object session)
        {
             if (session == null)
            
               RedirectToAction("Login", "User");

            }
        }
    }

