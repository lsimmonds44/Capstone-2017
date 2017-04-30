using System.Web;
using System.Web.Mvc;

namespace MVCPresentationLayer
{

/// <summary>
/// Ariel Sigo
/// Updated:
/// 2017/04/29
/// Filters through all the config files
/// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Ariel Sigo
        /// Updated:
        /// 2017/04/29
        /// Adds new filters for global filters
        /// </summary>
        /// <param name="filters"></param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
