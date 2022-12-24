using SysorovShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysorovShop.App_Start
{
    public class FilterConfig
    {
         public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ProductFilter()); // Add this line
            filters.Add(new HandleErrorAttribute());
        }
    }
}