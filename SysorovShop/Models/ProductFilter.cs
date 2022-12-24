using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysorovShop.Models
{
    public class ProductFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ProductsRepository pr = new ProductsRepository();
            IEnumerable<Category> model1 = pr.GetCategory();
            List<string> CategoryName = new List<string>();

            foreach (var cat in model1)
            {
                CategoryName.Add(cat.Name);
            }
            filterContext.Controller.ViewBag.Categories = CategoryName;
        }
    }
}