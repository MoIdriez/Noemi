using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Noemi.Controllers
{
    public class TrialController : BaseController
    {
        public ActionResult Index(TrialViewModel model)
        {
            if (model.Index == 0)
                return View(GetModel(1));
            var trial = new Trial
            {
                Image = model.Image,
                Colour = model.Colour,
                Slider = model.Slider
            };
            Participant.Trials.Add(trial);
            return View(GetModel(model.Index++));
        }

        public TrialViewModel GetModel(int index)
        {
            return new TrialViewModel
            {
                Index = index,
                ColourMode = Convert.ToInt32(Config.ColourMode),
                Colours = Convert.ToInt32(Config.ColourMode) == 36 ? Config.Colours36 : Config.Colours9,
                Image = index + ".png",
                Slider = 50
            };
        }


        public class TrialViewModel
        {
            public int Index { get; set; }
            public int ColourMode { get; set; }
            public string Colours { get; set; }
            public string Image { get; set; }
            public string Colour { get; set; }
            public int Slider { get; set; }

        }
    }
}