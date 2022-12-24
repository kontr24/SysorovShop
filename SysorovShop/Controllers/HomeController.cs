using SysorovShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;


namespace SysorovShop.Controllers
{


    public class HomeController : Controller
    {

        public FileContentResult GetImage(int Id)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            Product product = db.Products.FirstOrDefault(g => g.Id == Id);

            if (product != null)
            {
                return File(product.ImageData, product.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Home
        public ActionResult Index(string category = null, string sortOrder = null, string searchString = null, string parameters = null, int? maxPrice=null, int? minPrice = null,
            int? categor = null, string models = null, int? diskDiameter = null, int? mainsVoltage = null, int? noiceLevel = null, string sortBrandAndPrice =null, 
            string brand = null, int? power = null, double? diameterHole =null, int? revs = null, double? weight =null, double? maximumImpact = null, string dimensions = null)
        {
            ProductsRepository pr = new ProductsRepository(); //Сделать через репозиторий 
 
            PowerToolsEntities db = new PowerToolsEntities();

            ViewBag.category = category;


            IEnumerable<Product> products = db.Products.Include(p => p.Category);

            if (!String.IsNullOrEmpty(category))
            {
                products = pr.GetProducts(category);
            }

            //сортировка
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["PriceSort"] = sortOrder == "Price" ? "price_desc" : "Price";
            if (sortOrder != null)
            {
                products = pr.SortProducts(sortOrder, category);
            }

            if (sortBrandAndPrice == "по названию бренда(Я-а,Z-A)")
            {
                products = products.OrderByDescending(s => s.Brand);
            }


            if (sortBrandAndPrice == "по названию бренда (А-я,A-Z)")
            {
                products = products.OrderBy(s => s.Brand);
            }


            if (sortBrandAndPrice == "по убыванию цены")
            {
                products = products.OrderByDescending(s => s.Price);
            }

            if (sortBrandAndPrice == "по возрастанию цены")
            {
                products = products.OrderBy(s => s.Price);
            }


            ViewBag.sortOrder = sortOrder;
            //сортировка

            //поиск 
            if (!String.IsNullOrEmpty(searchString))
            {
                products = db.Products.Where(s => s.Model.ToUpper().Contains(searchString.ToUpper())
                                       || s.Brand.ToUpper().Contains(searchString.ToUpper()));
            }
            //поиск

            if (minPrice.HasValue)
            {
                products = products.Where(x => x.Price >= minPrice);
                //Session["minPrice"] = minPrice.ToString();
        

            }

            if (maxPrice.HasValue)
            {
                products = products.Where(x => x.Price <= maxPrice);
                //Session["maxPrice"] = maxPrice.ToString();
            }

            //фильтрация
            if (categor != null && categor != 0)
            {
                products = products.Where(p => p.CategoryId == categor);

                //FilteringProducts.diskDiameter = products.Where(p => p.CategoryId == categor).Select(p => (int)p.DiskDiameter).FirstOrDefault();

                //FilteringProducts.mainsVoltage = products.Where(p => p.CategoryId == categor).Select(p => (int)p.MainsVoltage).FirstOrDefault();
                //FilteringProducts.noiseLevel = products.Where(p => p.CategoryId == categor).Select(p => (int)p.NoiseLevel).FirstOrDefault();
                //ViewBag.diskDiameter = FilteringProducts.diskDiameter;
                //ViewBag.mainsVoltage = FilteringProducts.mainsVoltage;
                //ViewBag.noiseLevel = FilteringProducts.noiseLevel;
            }
 

            ViewBag.categor = categor;
 
            if (!String.IsNullOrEmpty(brand) && !brand.Equals("Все"))
            {
                products = products.Where(p => p.Brand == brand);

            }

            if (power != null && power != 0)
            {
                products = products.Where(p => p.Power == power);
            }
            if (diskDiameter != null && diskDiameter != 0)
            {
                products = products.Where(p => p.DiskDiameter == diskDiameter);
            }
            if (diameterHole != null && diameterHole != 0)
            {
                products = products.Where(p => p.DiameterHole == diameterHole);
            }


            if (revs != null && revs != 0)
            {
                products = products.Where(p => p.Revs == revs);
            }
            if (mainsVoltage != null && mainsVoltage != 0)
            {
                products = products.Where(p => p.MainsVoltage == mainsVoltage);
            }
            if (weight != null && weight != 0)
            {
                products = products.Where(p => p.Weight == weight);
            }


            if (maximumImpact != null && maximumImpact != 0)
            {
                products = products.Where(p => p.MaximumImpact == maximumImpact);
            }
            if (noiceLevel != null && noiceLevel != 0)
            {
                products = products.Where(p => p.NoiseLevel == noiceLevel);
            }
            if (!String.IsNullOrEmpty(dimensions) && !dimensions.Equals("Все"))
            {
                products = products.Where(p => p.Dimensions == dimensions);

            }
            //фильтрация

            List<Category> categories = db.Categories.ToList();
            categories.Insert(0, new Category { Name = "Все", Id = 0 });

            List<Product> product = db.Products.ToList();
            product.Insert(0, new Product { Brand = "Все", Id = 0 });


            FilteringProducts plvm = new FilteringProducts
            {
                Products = products.ToList(),
                Categories = new SelectList(categories, "Id", "Name"),
                Brands = new SelectList(product, "Brand", "Brand"),
                DiameterHoles = new SelectList
                    (new List<string>()
                    {
                        "Все",
                        "22.2"
                   
                    }),
                Powers = new SelectList
                    (new List<string>()
                    {
                        "Все",
                        "600",
                        "680",
                        "730",
                        "755",
                        "900",
                        
                    }),

                DiskDiameter = new SelectList
                    (new List<string>()
                    {
                        "Все",
                        "115",
                        "125"
                    }),

                Revs = new SelectList
                    (new List<string>()
                    {
                        "Все",
                        "500",
                        "2600",
                        "2800",
                        "3000",
                        "12000"
                    }),
                MainsVoltage = new SelectList(new List<string>()
                {
                    "Все",
                    "220",
                    "230"
                }),
                Weight = new SelectList(new List<string>()
                {
                    "Все",
                    "1.72",
                    "1.8",
                    "3",
                    "3.2",
                    "6.3"
                }),
                MaximumImpacts = new SelectList(new List<string>()
                {
                    "Все",
                    "1.2",
                    "1.5",
                    "1.8",
                    "2",
                    "2.2"
                }),
                Dimensions = new SelectList(new List<string>()
                {
                    "Все",
                    "270x360x100",
                    "360x130x480",
                    "370x130x340",
                    "426x108x426",
                    "480х460х310"
                }),

                NoiceLevel = new SelectList(new List<string>()
                {
                    "Все",
                    "90",
                    "95"
                }),

                SortBrandAndPrice = new SelectList(new List<string>()
                {
                    "Все",
                    "по убыванию цены",
                    "по возрастанию цены",
                    "по названию бренда(Я-а,Z-A)",
                    "по названию бренда (А-я,A-Z)"

                }),

            };

            return View(plvm);
        }


        public ActionResult Product(int id = 0)
        {
            ProductsRepository pr = new ProductsRepository();
            if (id != 0)
            {
                Product model = new Product();
                //ProductsRepository pr = new ProductsRepository();
                model = pr.GetProductById(id);

                return View(model);
            }
            else
                return RedirectToAction("Index");
        }



        public ActionResult LogOn()
        {
            Session["Test"] = "Сергей";
            int counter = Convert.ToInt32(Session["Counter"]);
            Session["Counter"] = ++counter;
            return RedirectToAction("Index");
        }
        public ActionResult LogOut()
        {
            Session["Test"] = null;
            if (Session["Counter"] != null)
            {
                int counter = Convert.ToInt32(Session["Counter"]);
                Session["Counter"] = --counter;
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}