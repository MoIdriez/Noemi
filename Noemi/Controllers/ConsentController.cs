using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Noemi.Controllers
{
    public class ConsentController : BaseController
    {
        // GET: Consent
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Agreed()
        {
            Session["Consent"] = true;
            return RedirectToAction("Index", "Trial");
        }
    }
}