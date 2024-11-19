using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BoysCoffe.Controllers
{
    public class SecureController : Controller
    {
        // GET: Secure
        public ActionResult Unauthorized()
        {
            return View();
        }
    }
}