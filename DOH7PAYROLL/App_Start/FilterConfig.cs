using System.Web;
using System.Web.Mvc;

namespace DOH7PAYROLL
{
   
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

}
