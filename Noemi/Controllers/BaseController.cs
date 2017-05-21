using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Noemi.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            HttpContext myContext = System.Web.HttpContext.Current;

            // check if session is supported
            if (myContext.Session != null)
            {
                // check if a new session id was generated
                if (myContext.Session.IsNewSession || Participant.ProloficId == string.Empty)
                {
                    filterContext.Result = ((BaseController)filterContext.Controller).RedirectToAction("Index", "Home");
                } else if (filterContext.RequestContext.RouteData.GetRequiredString("controller") != "Consent" && !Participant.Consent)
                {
                    filterContext.Result = ((BaseController)filterContext.Controller).RedirectToAction("Index", "Consent");
                }
            }
            base.OnAuthorization(filterContext);
        }

        public class Config
        {
            private static readonly System.Configuration.Configuration MyConfig =
                WebConfigurationManager.OpenWebConfiguration("~");
            
            public enum Orientation
            {
                Default, Random, FlipHorizontal, FlipVertical, FlipDouble
            }

            public static Orientation ColourOrientation => (Orientation) Enum.Parse(typeof(Orientation), WebConfigurationManager.AppSettings["colourOrientation"]);

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
            public static int ImageIteration
            {
                get { return Convert.ToInt32(WebConfigurationManager.AppSettings["imageIterations"]); }
                set
                {
                    MyConfig.AppSettings.Settings["imageIterations"].Value = value.ToString();
                    MyConfig.Save();
                }
            }
            public static string ColourMode
            {
                get { return WebConfigurationManager.AppSettings["colourMode"]; }
                set
                {
                    MyConfig.AppSettings.Settings["colourMode"].Value = value;
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
                get
                {
                    return (string) Session["ProloficID"] ?? string.Empty;
                }
                set { Session["ProloficID"] = value; }
            }
            public static string SessionId
            {
                get
                {
                    return (string)Session["SessionID"] ?? string.Empty;
                }
                set { Session["SessionID"] = value; }
            }
            public static bool Consent => Session["Welcome"] != null && Session["Consent"] != null && Session["Explanation"] != null;

            public static bool IsAdmin
            {
                get
                {
                    return Session["IsAdmin"] != null;
                }
                set { Session["IsAdmin"] = value; }
            }

            public static List<string> Images => Session["Images"] as List<string> ?? GenerateImages();

            private static List<string> GenerateImages()
            {
                var images = new List<string>();


                DirectoryInfo directory = new DirectoryInfo(HostingEnvironment.MapPath(@"~\Images"));
                var files = directory.GetFiles().Select(f => f.Name).ToList();
                for (int i = 0; i < Config.ImageIteration; i++)
                {
                    Random rnd = new Random();
                    images.AddRange(Config.ImageOrderIsRandom ? files.OrderBy(f => rnd.Next()).ToList() : files);
                }

                return images;
            }


            public static List<Trial> Trials
            {
                get { return Session["Trials"] as List<Trial> ?? new List<Trial>(); }
                set { Session["Trials"] = value; }
            }
        }

        public class Trial
        {
            public double TimeColour { get; set; }
            public double TimeNext { get; set; }
            public string Image { get; set; }
            public string Colour { get; set; }
            public int Slider { get; set; }
        }

    }
}