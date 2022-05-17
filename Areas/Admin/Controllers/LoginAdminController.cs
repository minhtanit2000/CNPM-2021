using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPM.Areas.Admin.Controllers
{
    public class LoginAdminController : Controller
    {
        // GET: Admin/LoginAdmin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(string mail, string password)
        {
            if (mail != "admin" && password != "123")
            {
                ViewBag.errorMsg = "Wrong username or password.";
                return View("Index");
            }
            else
            {
                HttpCookie admin = new HttpCookie("admin");
                admin.Value = mail;
                admin.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(admin);

                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult LogOut()
        {
            HttpCookie idUser = new HttpCookie("admin");
            idUser.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(idUser);

            return RedirectToAction("Index", "LoginAdmin");
        }
    }
}