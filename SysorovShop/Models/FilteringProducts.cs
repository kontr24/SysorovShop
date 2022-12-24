using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysorovShop.Models
{
    public partial class FilteringProducts
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Category { get; set; }
        public SelectList Models { get; set; }
        public SelectList Categories { get; set; }

        public SelectList DiskDiameter { get; set; }

        public SelectList MainsVoltage { get; set; }
        public SelectList NoiceLevel { get; set; }
        public SelectList SortBrandAndPrice { get; set; }
        public SelectList Brands { get; set; }
        public SelectList Powers { get; set; }
        public SelectList DiameterHoles { get; set; }
        public SelectList Revs { get; set; }
        public SelectList MaximumImpacts { get; set; }
        public SelectList Dimensions { get; set; }
        public SelectList Weight { get; set; }


        //
        //public enum Status { Admin = 1, User = 2 }
        //public static int diskDiameter { get; set; }
        public static int mainsVoltage { get; set; }
        //public static int noiseLevel { get; set; }

    }
}