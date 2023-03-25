using MangaWorld_admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MangaWorld_admin.Controllers
{
    public class HomeController : Controller
    {
        private DBcontext db = new DBcontext();
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult Login()
        {
            return View();
        }

        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult LoginStart(string AdminName, string AdminPassword)
        {

            if (String.IsNullOrEmpty(AdminPassword))
            {
                return RedirectToAction("Login");
            }
            else
            {
                TempData["LoginResult"] = "";
                var CurrentAdmin = db.Admins.Where(a => a.AdminName == AdminName && a.AdminPassword == AdminPassword); ;
                if (CurrentAdmin.FirstOrDefault() != null)
                {
                    Session.Timeout = 180;
                    Session["AdminId"] = CurrentAdmin.FirstOrDefault().AdminId;
                    Session["AdminName"] = CurrentAdmin.FirstOrDefault().AdminName;

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
        }
        [OutputCache(NoStore = true, Duration = 0)]
        public ActionResult LogOut()
        {
            SessionCheck.AutoLogOut();
            return RedirectToAction("Login");
        }
    }
}