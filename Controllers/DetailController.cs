using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;

namespace CNPM.Controllers
{
    public class DetailController : Controller
    {
        ShopOnlineEntities db = new ShopOnlineEntities();
        // GET: Detail
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getDescription(long id, string meta)
        {
            var v = from t in db.products
                    where t.id == id && t.meta == meta && t.hide == true
                    orderby t.orderBy ascending
                    select t;
            return View(v.ToList());
        }

    }
}