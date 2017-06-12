using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.SessionState;
using Noemi.Models;

namespace Noemi.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var myContext = System.Web.HttpContext.Current;

            // check if session is supported
            if (myContext.Session != null)
            {
                // check if a new session id was generated
                if (myContext.Session.IsNewSession || Participant.ProloficId == string.Empty)
                {
                    filterContext.Result = ((BaseController) filterContext.Controller).RedirectToAction("Index", "Home");
                }
                else if (filterContext.RequestContext.RouteData.GetRequiredString("controller") != "Consent" &&
                         !Participant.Consent)
                {
                    filterContext.Result = ((BaseController) filterContext.Controller).RedirectToAction("Index",
                        "Consent");
                }
            }
            base.OnAuthorization(filterContext);
        }

        public class Config
        {
            public enum Orientation
            {
                Default,
                Random,
                FlipHorizontal,
                FlipVertical,
                FlipDouble
            }

            private static readonly Configuration MyConfig =
                WebConfigurationManager.OpenWebConfiguration("~");

            public static Orientation ColourOrientation
                =>
                    (Orientation)
                        Enum.Parse(typeof (Orientation), WebConfigurationManager.AppSettings["colourOrientation"]);

            public static bool ColourOrderIsRandom
            {
                get { return Convert.ToBoolean(WebConfigurationManager.AppSettings["colourOrderIsRandom"]); }
                set
                {
                    MyConfig.AppSettings.Settings["colourOrderIsRandom"].Value = value.ToString();
                    MyConfig.Save();
                }
            }

            public static bool ImageOrderIsRandom
            {
                get { return Convert.ToBoolean(WebConfigurationManager.AppSettings["imageOrderIsRandom"]); }
                set
                {
                    MyConfig.AppSettings.Settings["imageOrderIsRandom"].Value = value.ToString();
                    MyConfig.Save();
                }
            }

            public static int ImageIterations
            {
                get { return Convert.ToInt32(WebConfigurationManager.AppSettings["imageIterations"]); }
                set
                {
                    MyConfig.AppSettings.Settings["imageIterations"].Value = value.ToString();
                    MyConfig.Save();
                }
            }

            public static int ColourMode
            {
                get { return Convert.ToInt32(WebConfigurationManager.AppSettings["colourMode"]); }
                set
                {
                    MyConfig.AppSettings.Settings["colourMode"].Value = value.ToString();
                    MyConfig.Save();
                }
            }

            public static string Colours4
            {
                get { return WebConfigurationManager.AppSettings["colours4"]; }
                set
                {
                    MyConfig.AppSettings.Settings["colours4"].Value = value;
                    MyConfig.Save();
                }
            }

            public static string Colours9
            {
                get { return WebConfigurationManager.AppSettings["colours9"]; }
                set
                {
                    MyConfig.AppSettings.Settings["colours9"].Value = value;
                    MyConfig.Save();
                }
            }

            public static string Colours36
            {
                get { return WebConfigurationManager.AppSettings["colours36"]; }
                set
                {
                    MyConfig.AppSettings.Settings["colours36"].Value = value;
                    MyConfig.Save();
                }
            }

            public static string ExternalLink
            {
                get { return WebConfigurationManager.AppSettings["externalLink"]; }
                set
                {
                    MyConfig.AppSettings.Settings["externalLink"].Value = value;
                    MyConfig.Save();
                }
            }
        }

        public class Participant
        {
            public static HttpSessionState Session => System.Web.HttpContext.Current.Session;

            public static string ProloficId
            {
                get { return (string) Session["ProloficID"] ?? string.Empty; }
                set { Session["ProloficID"] = value; }
            }

            public static string SessionId
            {
                get { return (string) Session["SessionID"] ?? string.Empty; }
                set { Session["SessionID"] = value; }
            }

            public static bool Consent
                => Session["Welcome"] != null && Session["Consent"] != null && Session["Explanation"] != null;

            public static bool IsAdmin
            {
                get { return Session["IsAdmin"] != null; }
                set { Session["IsAdmin"] = value; }
            }

            public static List<string> Images
            {
                get
                {
                    if (Session["Images"] == null)
                        Session["Images"] = GenerateImages();
                    return Session["Images"] as List<string>;
                }
                set
                {
                    Session["Trials"] = value;
                }
            }


            public static List<Trial> Trials
            {
                get { return Session["Trials"] as List<Trial> ?? new List<Trial>(); }
                set { Session["Trials"] = value; }
            }

            private static List<string> GenerateImages()
            {
                var images = new List<string>();


                var directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Images"));
                var files = directory.GetFiles().Where(f => f.Name.Contains(".png")).Select(f => f.Name).ToList();
                var rnd = new Random();
                for (var i = 0; i < Config.ImageIterations; i++)
                {
                    images.AddRange(Config.ImageOrderIsRandom ? files.OrderBy(f => rnd.Next()).ToList() : files);
                }

                return images;
            }

            public static void Save()
            {
                using (var db = new NoemiContext())
                {
                    db.Participants.Add(new Models.Participant
                    {
                        ProloficID = ProloficId,
                        SessionID = SessionId,
                        ColourOrientation = Config.ColourOrientation.ToString(),
                        ColourOrderIsRandom = Config.ColourOrderIsRandom,
                        ImageIterations = Config.ImageIterations,
                        ImageOrderIsRandom = Config.ImageOrderIsRandom,
                        ColourMode = Config.ColourMode,
                        Colours4 = Config.Colours4,
                        Colours9 = Config.Colours9,
                        Colours36 = Config.Colours36,
                        Trials = Trials.Select(t => new Models.Trial
                        {
                            TrialIndex = t.Index,
                            TimeColour = t.TimeColour,
                            TimeNext = t.TimeNext,
                            Image = t.Image,
                            Colour = t.Colour,
                            Slider = t.Slider
                        }).ToList()
                    });
                    db.SaveChanges();
                    Session.Abandon();
                }
            }
        }

        public class Trial
        {
            public int Index { get; set; }
            public int TimeColour { get; set; }
            public int TimeNext { get; set; }
            public string Image { get; set; }
            public string Colour { get; set; }
            public int Slider { get; set; }
        }
    }
}