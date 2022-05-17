using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CNPM.Models;

namespace CNPM.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private ShopOnlineEntities db = new ShopOnlineEntities();

        // GET: Admin/Products
        public ActionResult Index()
        {
            return View(db.products.ToList());
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "id,name,price,img,img1,img2,img3,description,meta,size,color,hide,orderBy,datebegin,categoryid")] product product, HttpPostedFileBase img, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3)
        {
            string path = "";
            string filename = "";
            string path1 = "";
            string filename1 = "";
            string path2 = "";
            string filename2 = "";
            string path3 = "";
            string filename3 = "";
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename);
                    img.SaveAs(path);
                    product.img = filename;
                }
                else
                {
                    product.img = "logo.png";
                }
                if (img1 != null)
                {
                    filename1 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img1.FileName;
                    path1 = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename1);
                    img1.SaveAs(path1);
                    product.img1 = filename1;
                }
                else
                {
                    product.img1 = "logo.png";
                }
                if (img2 != null)
                {
                    filename2 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path2 = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename2);
                    img2.SaveAs(path2);
                    product.img2 = filename2;
                }
                else
                {
                    product.img2 = "logo.png";
                }
                if (img3 != null)
                {
                    filename3 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path3 = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename3);
                    img3.SaveAs(path3);
                    product.img3 = filename3;
                }
                else
                {
                    product.img3 = "logo.png";
                }
                product.datebegin = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                product.meta = Functions.ConvertToUnSign(product.meta); 
                product.orderBy = getMaxOrder(product.categoryid);
                db.products.Add(product);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "id,name,price,img,img1,img2,img3,description,meta,size,color,hide,orderBy,datebegin,categoryid")] product product, HttpPostedFileBase img, HttpPostedFileBase img1, HttpPostedFileBase img2, HttpPostedFileBase img3)
        {
            string path = "";
            string filename = "";
            string path1 = "";
            string filename1 = "";
            string path2 = "";
            string filename2 = "";
            string path3 = "";
            string filename3 = "";
            product temp = db.products.Find(product.id);
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename);
                    img.SaveAs(path);
                    temp.img = filename;  
                }
                if (img1 != null)
                {
                    filename1 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img1.FileName;
                    path1 = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename1);
                    img1.SaveAs(path1);
                    temp.img1 = filename1;
                }
                if (img2 != null)
                {
                    filename2 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img2.FileName;
                    path2 = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename2);
                    img2.SaveAs(path2);
                    temp.img2 = filename2;
                }
                if (img3 != null)
                {
                    filename3 = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img3.FileName;
                    path3 = Path.Combine(Server.MapPath("~/Content/img/Upload/product"), filename3);
                    img3.SaveAs(path3);
                    temp.img3 = filename3;
                }
                temp.name = product.name;
                temp.price = product.price;
                temp.description = product.description;
                temp.meta = Functions.ConvertToUnSign(product.meta); 
                temp.color = product.color;
                temp.size = product.size;
                temp.hide = product.hide;
                temp.orderBy = product.orderBy;
                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Index", "LoginAdmin");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            product product = db.products.Find(id);
            db.products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public int getMaxOrder(long? CategoryId)
        {
            if (CategoryId == null)
                return 1;
            return db.products.Where(x => x.categoryid == CategoryId).Count();
        }
    }
    
    public static class Functions
    {
        public static string ConvertToUnSign(string text)
        {
            for (int i = 33; i < 48; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 58; i < 65; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }

            for (int i = 91; i < 97; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            for (int i = 123; i < 127; i++)
            {
                text = text.Replace(((char)i).ToString(), "");
            }
            text = text.Replace(" ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        // As the text the: "<span class='glyphicon glyphicon-plus'></span>" can be entered
        public static MvcHtmlString NoEncodeActionLink(this HtmlHelper htmlHelper,
                                             string text, string title, string action,
                                             string controller,
                                             object routeValues = null,
                                             object htmlAttributes = null)
        {
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            TagBuilder builder = new TagBuilder("a");
            builder.InnerHtml = text;
            builder.Attributes["title"] = title;
            builder.Attributes["href"] = urlHelper.Action(action, controller, routeValues);
            builder.MergeAttributes(new RouteValueDictionary(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes)));

            return MvcHtmlString.Create(builder.ToString());
        }
    }
}
