using SysorovShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SysorovShop.Abstract
{
    
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; set; }
    }
}