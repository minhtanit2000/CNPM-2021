using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;

namespace CNPM.Controllers
{
    public class ProductController : Controller
    {
        ShopOnlineEntities db = new ShopOnlineEntities();
        // GET: Product
        [Route("cua-hang")]
        public ActionResult Index()
        {
            return View();
        }
        // lay san pham
        public ActionResult getProduct(string price)
        {
            ViewBag.meta = "cua-hang";
            if (price == "low") {
                var a = from t in db.products
                        orderby t.price ascending
                        select t;
                return PartialView(a.ToList());
            }
            if (price == "high")
            {
                var b = from t in db.products
                        orderby t.price descending
                        select t;
                return PartialView(b.ToList());
            }
            var v = from t in db.products
                    where t.hide == true
                    orderby t.orderBy ascending
                    select t;
            return PartialView(v.ToList());
        }

        // tim kiem san pham 
        [HttpPost]
        public ActionResult getProduct_(string query)
        {
            var v = db.products.Where(x => x.name.Contains(query)).ToList();
            return PartialView("_SearchResultsPartial", v.ToList());
        }

        public ActionResult getCategory()
        {
            var v = from t in db.categories
                    orderby t.orderBy ascending
                    select t;
            return PartialView(v.ToList());
        }
        // san pham theo loai
        public ActionResult getProductFollowMeta(string meta)
        {
            string url = Request.Url.ToString();
            Uri myUri = new Uri(url);
            string host = myUri.Authority;

            var v = from t in db.products
                    where t.meta == meta
                    orderby t.orderBy ascending
                    select t;
            return PartialView(v.ToList());
        }
        
        public ActionResult Index(string meta)
        {
            if (meta == "lowprice")
            {
                return null;
            }
            var v = from t in db.products
                    where t.meta == meta
                    select t;
            return PartialView(v.ToList());
        }

        //btn search
        [HttpGet]
        public ActionResult Search()
        {
            return PartialView("_SearchFormPartial");
        }

        public ActionResult GetLowPrice()
        {
            return View();
        }
        public ActionResult GetHighPrice()
        {
            return View();
        }
    }
}