using PCMS.Models;
using PCMS_Library;
using PCMS_Library.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PCMS.Controllers
{
    public class AccountController : Controller
    {
        pcms_BAL objBAL = new pcms_BAL();
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                ErrorLog objError = new ErrorLog();
                objError.LogError(ex);
                ViewBag.ErrorMsg = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(pcms_admin _admin)
        {
            try
            {
                var userKey = objBAL.getAdminCredential(_admin);
                if (userKey.ADMIN_ID != 0)
                {
                    Session["userKey"] = userKey.ADMIN_ID.ToString();
                    return RedirectToAction("Index", "Home", null);
                }
                else
                {
                    ViewBag.Error = "Invalid credentials";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ErrorLog objError = new ErrorLog();
                objError.LogError(ex);
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}
