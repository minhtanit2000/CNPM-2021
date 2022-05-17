using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CNPM.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        [Route("lien-he")]
        public ActionResult Index()
        {
            return View();
        }
    }
}