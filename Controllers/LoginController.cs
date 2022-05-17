using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CNPM.Models;

namespace CNPM.Controllers
{
    public class LoginController : Controller
    {
        ShopOnlineEntities db = new ShopOnlineEntities();
        [HttpGet]
        public ActionResult Index()
        {
            if (HttpContext.Request.Cookies["idUser"] != null)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(CNPM.Models.account acc)
        {
            string pass = MD5Hash(acc.password);
            var v = from t in db.accounts
                    where t.Email == acc.Email && t.password == pass
                    select t;
            if (v.ToList().Count() == 0)
            {
                ViewBag.errorMsg = "Wrong username or password.";
                return View("Index", acc);
            }
            else
            {
                HttpCookie idUser = new HttpCookie("idUser");
                idUser.Value = acc.Email;
                idUser.Expires = DateTime.Now.AddHours(1);
                Response.Cookies.Add(idUser);

                HttpCookie idCart = new HttpCookie("idCart");
                idCart.Value = acc.Email;
                Response.Cookies.Add(idCart);

                return RedirectToAction("Index", "TrangChu");
            }
        }   

        public ActionResult LogOut()
        {
            HttpCookie idUser = new HttpCookie("idUser");
            idUser.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(idUser);

            HttpCookie idCart = new HttpCookie("idCart");
            idCart.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Add(idCart);
            return RedirectToAction("Index", "Login");
        }
        // hash mk
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }
    }
}
