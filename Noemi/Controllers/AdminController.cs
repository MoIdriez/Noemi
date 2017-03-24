using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Noemi.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string password)
        {
            if (password != "Mo is awesome") 
                return RedirectToAction("Index");
            Participant.IsAdmin = true;
            return RedirectToAction("Settings");
        }

        public ActionResult Settings()
        {
            if (!Participant.IsAdmin)
                return RedirectToAction("Index");
            return View(GetModel());
        }

        [HttpPost]
        public ActionResult Settings(SettingsViewModel model)
        {
            Config.ColourOrder = model.ColourMode;
            Config.ColourOrder = model.ColourOrder;
            Config.ColourOrderIsRandom = model.ColourOrderIsRandom;
            Config.ImageOrder = model.ImageOrder;
            Config.ImageOrderIsRandom = model.ImageOrderIsRandom;
            Config.Colours9 = model.Colours9;
            Config.Colours36 = model.Colours36;
            Config.ColourOrder = model.ColourMode;
            return View(GetModel());
        }

        public SettingsViewModel GetModel()
        {
            return new SettingsViewModel
            {
                ColourMode = Config.ColourMode,
                ColourOrder = Config.ColourOrder,
                ColourOrderIsRandom = Config.ColourOrderIsRandom,
                ImageOrder = Config.ImageOrder,
                ImageOrderIsRandom = Config.ImageOrderIsRandom,
                Colours9 = Config.Colours9,
                Colours36 = Config.Colours36
            };
        }


        public class SettingsViewModel
        {
            public string ColourMode { get; set; }
            public string ColourOrder { get; set; }
            public bool ColourOrderIsRandom { get; set; }
            public string ImageOrder { get; set; }
            public bool ImageOrderIsRandom { get; set; }
            public string Colours9 { get; set; }
            public string Colours36 { get; set; }
        }
    }
}