using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPM.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            return View();
        }
    }
}