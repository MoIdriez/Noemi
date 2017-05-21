using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Management;
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

        public ActionResult Consent()
        {
            Session["Welcome"] = true;
            return View();
        }

        public ActionResult Explanation()
        {
            Session["Consent"] = true;
            return View();
        }


        public ActionResult Agreed()
        {
            Session["Explanation"] = true;
            return RedirectToAction("Index", "Trial");
        }
    }
}