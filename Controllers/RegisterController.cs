using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using CNPM.Models;

namespace CNPM.Controllers
{
    public class RegisterController : Controller
    {
        ShopOnlineEntities db = new ShopOnlineEntities();

        // GET: Register
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.birthday = "2000-01-01";
            return View();
        }

        //dang ki tai khoan
        [HttpPost]
        public ActionResult Register(string fullname, DateTime birthday, string gender, string mail, string address, string phone, string password, string password2)
        {
            // kiem tra tinh hop le
            if (string.IsNullOrEmpty(fullname) || (fullname.Trim() == ""))
            {
                SaveForm("", birthday, gender, mail, address, phone);
                ViewBag.Error = "Tên đăng kí không hợp lệ";
                return View("Index");
            }
            // check tuoi
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;

            if (age < 15 || age > 119)
            {
                SaveForm(fullname, birthday, gender, mail, address, phone);
                ViewBag.Error = "Ngày sinh không hợp lệ";
                return View("Index");
            }
            // check gioi tinh
            if (gender == "notchoose")
            {
                SaveForm(fullname, birthday, "", mail, address, phone);
                ViewBag.Error = "Vui lòng chọn giới tính";
                return View("Index");
            }
            //check dia chi
            if (string.IsNullOrEmpty(address) || (address.Trim() == ""))
            {
                SaveForm(fullname, birthday, gender, mail, address, phone);
                ViewBag.Error = "Địa chỉ không hợp lệ";
                return View("Index");
            }
            //check sdt
            if (!IsPhoneNumber(phone))
            {
                SaveForm(fullname, birthday, gender, mail, address, "");
                ViewBag.Error = "Số điện thoại không hợp lệ";
                return View("Index");
            }
            // check mail
            var v = from t in db.accounts
                    where t.Email == mail
                    select t;
            if (v.ToList().Count() != 0)
            {
                SaveForm(fullname, birthday, gender, "", address, phone);
                ViewBag.Error = "Email đã tồn tại";
                return View("Index");
            }
            if (!IsValidEmailAddress(mail))
            {
                SaveForm(fullname, birthday, gender, "", address, phone);
                ViewBag.Error = "Email không hợp lệ";
                return View("Index");
            }
            //check password
            if (password.Length <6)
            {
                SaveForm(fullname, birthday, gender, mail, address, phone);
                ViewBag.Error = "Mật khẩu phải chứa ít nhất 6 kí tự";
                return View("Index");
            }
            if (password != password2)
            {
                SaveForm(fullname, birthday, gender, mail, address, phone);
                ViewBag.Error = "Mật khẩu xác nhận không hợp lệ";
                return View("Index");
            }
            // add vao database
            string pass = MD5Hash(password);
            var acc = new account();
            acc.idCart = mail;
            acc.fullname = fullname;
            acc.birth = birthday;
            acc.gender = gender;
            acc.address = address;
            acc.sdt = phone;
            acc.password = pass;
            acc.Email = mail;

            db.accounts.Add(acc);
            db.SaveChanges();
            return RedirectToAction("Success", "Register");
        }
        public ActionResult Success()
        {
            return View();
        }
        public bool IsValidEmailAddress(string email)
        {
            if (!string.IsNullOrEmpty(email) && new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }
        // save data vào viewbag
        public void SaveForm(string fullname, DateTime birthday, string gender, string mail, string address, string phone)
        {
            ViewBag.fullname = fullname;
            string year = birthday.Year.ToString();
            string Month;
            string Day;
            if (birthday.Month < 10)
            {
                Month = "0" + birthday.Month.ToString();
            }
            else
            {
                Month = birthday.Month.ToString();
            }

            if (birthday.Day < 10)
            {
                Day = "0" + birthday.Day.ToString();
            }
            else
            {
                Day = birthday.Day.ToString();
            }

            string res = year + "-";
            res += Month + "-";
            res += Day;
            ViewBag.birthday = res;
            ViewBag.gender = gender;
            ViewBag.mail = mail;
            ViewBag.address = address;
            ViewBag.phone = phone;
        }

        // check phone number
        public static bool IsPhoneNumber(string number)
        {
            return !string.IsNullOrEmpty(number) && number.All(char.IsDigit);
        }

        //hash mk 
        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
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