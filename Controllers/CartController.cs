using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;


namespace CNPM.Controllers
{
    public class CartController : Controller
    {
        ShopOnlineEntities db = new ShopOnlineEntities();
        // GET: Cart
        public ActionResult Index()
        {
            HttpCookie idUser = HttpContext.Request.Cookies.Get("idUser");
            HttpCookie idCart = HttpContext.Request.Cookies.Get("idCart");

            if (HttpContext.Request.Cookies["idUser"] == null)
            {
                return RedirectToAction("Index", "Login");
            } else
            {
                string id = idCart.Value;
                var v = from t in db.carts
                        where t.idCart == id
                        select t;
                if (v.ToList().Count() > 0)
                {
                    return View(v.ToList());
                }
                else
                {
                    return RedirectToAction("Index", "TrangChu");
                }
            }
        }

        // xoa san pham
        [HttpPost]
        public ActionResult DeleteProduct(int idCart)
        {
            var v = from t in db.carts
                    where t.id == idCart
                    select t;

            string a = v.FirstOrDefault().idCart;
            cart sp = db.carts.Single(i => i.id == idCart);
            db.carts.Remove(sp);
            db.SaveChanges();

            return RedirectToAction("Index","Cart", new { idCart = a});
        }
        // tang giam so luong san pham 
        [HttpPost]
        public ActionResult ChangeQuantityProduct(string submit, int idProduct)
        {
            var a = from b in db.carts
                    where b.id == idProduct
                    select b;
            int idSP = a.FirstOrDefault().id;
            cart f = db.carts.FirstOrDefault(x => x.id == idSP);
            int n = f.quantity.Value;
            if (submit == "plus") { 
                n += 1;
            }
            else { 
                if (n>1)
                {
                    n -= 1;
                }
            }
            f.quantity = n;
            db.SaveChanges();
            return RedirectToAction("Index", "Cart");
        }
        // an nut mua san pham hay add to cart
        [HttpPost]
        public ActionResult BuyProduct(string submit, int productQuanity,string productSize, string productColor, int idProduct)
        {
            var v = from t in db.products
                    where t.id == idProduct
                    select t;

            string img = v.FirstOrDefault().img;
            string name = v.FirstOrDefault().name;
            float price = (int)v.FirstOrDefault().price;

            HttpCookie idUser = HttpContext.Request.Cookies.Get("idUser");
            
            //nut buy
            if (submit == "buy")
            {
                if (HttpContext.Request.Cookies["idUser"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                return RedirectToAction("Thank","Temp");
             // nut add cart
            } else
            {
                if (HttpContext.Request.Cookies["idUser"] == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                //check xem sp da co trong gio hang
                var a = from b in db.carts
                        where b.idCart == idUser.Value && b.nameProduct == name && b.size == productSize && b.color == productColor
                        select b;
                // chua co trong gio hhang
                if (!a.Any())
                {
                    cart cartObj = new cart();
                    cartObj.idCart = idUser.Value;
                    cartObj.image = img;
                    cartObj.nameProduct = name;
                    cartObj.quantity = productQuanity;
                    cartObj.size = productSize;
                    cartObj.color = productColor;
                    cartObj.dateCreate = DateTime.Now;
                    cartObj.price = (int)price;

                    db.carts.Add(cartObj);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cart");
                // co san pham trong gio hang r 
                } else
                {
                    int idSP = a.FirstOrDefault().id;
                    cart f = db.carts.FirstOrDefault(x => x.id == idSP);
                    int n = f.quantity.Value;
                    n += productQuanity;
                    f.quantity = n;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Cart");
                }
            }
        }
    }
}