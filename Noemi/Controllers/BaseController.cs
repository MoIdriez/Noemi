using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Configuration;
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
                if (myContext.Session.IsNewSession || Participant.ProloficID == string.Empty)
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
            public static string ColourMode
            {
                get { return WebConfigurationManager.AppSettings["colourMode"]; }
                set
                {
                    MyConfig.AppSettings.Settings["colourMode"].Value = value;
                    MyConfig.Save();
                }
            }
            public static string ColourOrder
            {
                get { return WebConfigurationManager.AppSettings["colourOrder"]; }
                set
                {
                    MyConfig.AppSettings.Settings["colourOrder"].Value = value;
                    MyConfig.Save();
                }
            }
            public static bool ColourOrderIsRandom
            {
                get { return Convert.ToBoolean(WebConfigurationManager.AppSettings["colourOrderIsRandom"]); }
                set
                {
                    MyConfig.AppSettings.Settings["colourOrderIsRandom"].Value = value.ToString();
                    MyConfig.Save();
                }
            }
            public static string ImageOrder
            {
                get { return WebConfigurationManager.AppSettings["imageOrder"]; }
                set
                {
                    MyConfig.AppSettings.Settings["imageOrder"].Value = value;
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
        }

        public class Participant
        {
            public static HttpSessionState Session => System.Web.HttpContext.Current.Session;
            public static string ProloficID
            {
                get
                {
                    return (string) Session["ProloficID"] ?? string.Empty;
                }
                set { Session["ProloficID"] = value; }
            }
            public static string SessionID
            {
                get
                {
                    return (string)Session["SessionID"] ?? string.Empty;
                }
                set { Session["SessionID"] = value; }
            }
            public static bool Consent
            {
                get
                {
                    return Session["Consent"] != null;
                }
                set { Session["SessionID"] = value; }
            }
            public static bool IsAdmin
            {
                get
                {
                    return Session["IsAdmin"] != null;
                }
                set { Session["IsAdmin"] = value; }
            }

            public static List<Trial> Trials
            {
                get { return Session["Trials"] as List<Trial> ?? new List<Trial>(); }
                set { Session["Trials"] = value; }
            }
        }

        public class Trial
        {
            public string Image { get; set; }
            public string Colour { get; set; }
            public int Slider { get; set; }
        }

    }
}