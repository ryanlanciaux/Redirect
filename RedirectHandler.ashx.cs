using System.Configuration;
using System.Web;
using System.Web.Services;

namespace Redirect 
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class RedirectHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContextBase context)
        {
            var oldUrl = ConfigurationManager.AppSettings["original_url"];
            var newUrl = ConfigurationManager.AppSettings["new_url"];
            ProcessRequest(context, oldUrl, newUrl);
        }

        public void ProcessRequest(HttpContextBase context, string oldUrl, string newUrl)
        {
            var redirectHelper = new RedirectHelper(oldUrl, newUrl);
            redirectHelper.PerformRedirect(context);
        }

        public void ProcessRequest(HttpContext context)
        {
            ProcessRequest(new HttpContextWrapper(context));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
