
using System.Web;


namespace Redirect
{
    public class RedirectHelper
    {
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }

        public RedirectHelper()
        {
            
        }

        public RedirectHelper(string oldUrl, string newUrl)
        {
            OldUrl = oldUrl;
            NewUrl = newUrl;
        }

        public bool PerformRedirect(HttpContext context)
        {
            return PerformRedirect(new HttpContextWrapper(context)); 
        }

        public bool PerformRedirect(HttpContextBase context)
        {
            HttpContextBase cb; 

            var url = context.Request.Url.ToString().ToLower();

            if (url.Contains(OldUrl.ToLower()))
            {
                context.Response.Status = "301 Moved Permanently";
                context.Response.AddHeader("Location", url.Replace(OldUrl, NewUrl));
                return true;
            }

            return false;
        }
    }
}
