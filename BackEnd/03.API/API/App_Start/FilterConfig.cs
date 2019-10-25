using System.Web;
using System.Web.Mvc;

namespace API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new Cor());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
