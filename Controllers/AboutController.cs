using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPM.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        [Route("ve-chung-toi")]

        public ActionResult Index()
        {
            return View();
        }
    }
}