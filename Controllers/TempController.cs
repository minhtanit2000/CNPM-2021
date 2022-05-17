using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;

namespace CNPM.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        ShopOnlineEntities db = new ShopOnlineEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getMenu()
        {
            var v = from t in db.menus
                    where t.hide == true
                    orderby t.orderBy ascending
                    select t;
            return PartialView(v.ToList());
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Modal(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult Thank()
        {
            if (HttpContext.Request.Cookies["idCart"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            else
            {
                HttpCookie idCart = HttpContext.Request.Cookies.Get("idCart");
                string id = idCart.Value;
                var v = from t in db.carts
                        where t.idCart == id
                        select t;
                foreach (var i in v.ToList())
                {

                    cart sp = db.carts.Single(m => m.id == i.id);
                    db.carts.Remove(sp);
                    db.SaveChanges();
                }
                return View();
            }
        }
    }
}