using System.Web;
using System.Web.Mvc;

namespace IHK_Probe_Projekt
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
