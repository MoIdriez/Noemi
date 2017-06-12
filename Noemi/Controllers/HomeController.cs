using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Noemi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string prolific_pid = "", string session_id = "")
        {
            BaseController.Participant.Trials = new List<BaseController.Trial>();
            if (prolific_pid == string.Empty)
                return View();
            Session["ProloficID"] = prolific_pid;
            Session["SessionID"] = session_id;
            return RedirectToAction("Index", "Consent");
        }
    }
}