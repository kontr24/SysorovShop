using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SysorovShop.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }

        void SaveProduct(Product product);
    }

    public class ProductsRepository

    {
        IEnumerable<Product> _product { get; }

        IEnumerable<Product> _products;
        IEnumerable<Category> _category;

        public IEnumerable<Product> GetProducts(string category = null)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            IEnumerable<Product> model = db.Products;

            if (category != null)
            {

                model = db.Products.Include(i => i.Category).Where(w => w.Category.Name == category);
                return model;
            }

            return model;
        }
        public IEnumerable<Product> SortProducts(string sortOrder = null, string category = null)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            IEnumerable<Product> model = db.Products;
            if (category != null)
            {
                switch (sortOrder)
                {
                    case "name_desc":
                        model = db.Products.OrderByDescending(s => s.Brand).Include(i => i.Category).Where(w => w.Category.Name == category);
                        break;
                    case "Price":
                        model = db.Products.OrderBy(s => s.Price).Include(i => i.Category).Where(w => w.Category.Name == category);
                        break;
                    case "price_desc":
                        model = db.Products.OrderByDescending(s => s.Price).Include(i => i.Category).Where(w => w.Category.Name == category);
                        break;
                    default:
                        model = db.Products.OrderBy(s => s.Brand).Include(i => i.Category).Where(w => w.Category.Name == category);
                        break;

                }

            }
            else
            {
                switch (sortOrder)
                {
                    case "name_desc":
                        model = db.Products.OrderByDescending(s => s.Brand);
                        break;
                    case "Price":
                        model = db.Products.OrderBy(s => s.Price);
                        break;
                    case "price_desc":
                        model = db.Products.OrderByDescending(s => s.Price);
                        break;
                    default:
                        model = db.Products.OrderBy(s => s.Brand);
                        break;

                }
            }

            return model;
        }

        //
        //public IEnumerable<Product> FilteringProducts(string brand = null, int? parameters = null)
        //{
        //    //PowerToolsEntities db = new PowerToolsEntities();
        //    //IQueryable<Product> products = db.Products.Include(p => p.Category);
        //    ////if (brand != null)
        //    ////{
        //    ////    products = products.Where(p => p.Brand == brand);

        //    ////}
        //    //if (!String.IsNullOrEmpty(brand) && !brand.Equals("Все"))
        //    //{
        //    //    products = products.Where(p => p.Brand == brand);

        //    //}

        //    //if (parameters != null && parameters != 0)
        //    //{
        //    //    parameters = products.Where(p => p.CategoryId == parameters);
        //    //}


        //    //List<Category> categories = db.Categories.ToList();
        //    //// устанавливаем начальный элемент, который позволит выбрать всех
        //    //categories.Insert(0, new Category { Name = "Все", Id = 0 });

        //    //Product plvm = new Product
        //    //{
        //    //    Products = products.ToList(),
        //    //    Categories = new SelectList(parameters, "Id", "Name"),
        //    //    Brands = new SelectList(new List<string>()
        //    //{
        //    //    "Все",
        //    //    "Нападающий",
        //    //    "Полузащитник",
        //    //    "Защитник",
        //    //    "Вратарь"
        //    //})
        //    //};


        //    //FilteringProducts plv = new FilteringProducts
        //    //{
        //    //    Products = products.ToList(),
        //    //    Brands = new SelectList(new List<string>()),
        //    //    Parameters = new SelectList(new List<string>()
        //    //    {
        //    //        "Все",

        //    //    })

        //    return plv;
        //    //};

        //}//


        public IEnumerable<Category> GetCategory()
        {

            PowerToolsEntities db = new PowerToolsEntities();
            IEnumerable<Category> model1 = db.Categories;

            return model1;
        }



        public ProductsRepository()
        {
            //ProductContext db = new ProductContext();

            //List<Product> product = new List<Product>();

            //        product.Add(new Product(1, "Dorkel", "DRW-630", "Займет достойное место в домашнем хозяйстве умелого мастера", "Dorkel_DRW-630_.jpg", 1399, "Болгарка"
            //           , 630, 115, 22.2, null, null, null, null, null, null));
            //        product.Add(new Product(2, "Калибр", "МШУ-115/755", "Работа будет идти быстрее и проще", "Калиб_ МШУ-115.jpg", 1499, "Болгарка"
            //      , 755, 115, 22.2, null, null, null, null, null, null));
            //        product.Add(new Product(3, "HiKOKI", "G13SS2", "Профессиональный прибор с щеточным двигателем, действующий от общей электросети", "HiKOKI_G13SS2.jpg", 1899, "Болгарка"
            //     , 600, 125, 22.2, null, null, null, null, null, null));
            //        product.Add(new Product(4, "Hammer", "USM900D", "С помощью этого устройства производится обдирка, шлифовка и полировка различных поверхностей, зачистка сварочных швов и очистка металлоконструкций от коррозии", "USM900D.jpg", 3099, "Болгарка"
            //     , 900, 125, 22.2, null, null, null, null, null, null));
            //        product.Add(new Product(5, "Hyundai", "G 750-125", "Позволяет выполнять резку, шлифовку и очистку различных поверхностей при помощи 125-миллиметровых дисков с посадочным диаметром 22.2 мм", "Hyundai _G_750-125.jpg", 3099, "Болгарка"
            // , 680, 125, 22.2, null, null, null, null, null, null));



            //        product.Add(new Product(6, "Makita", "HP1630K", "Очень удобный аппарат", "Makita_HP1630K.jpg", 9699, "Дрель"
            //            , null, null, null, 12000, 220, 3.2, null, null, null));
            //        product.Add(new Product(7, "Rebir", "IE 1206-16", "Может использоваться для сверления различных материалов", "Rebir_IE 1206-16_.jpg", 11699, "Дрель"
            // , null, null, null, 500, 230, 6.3, null, null, null));
            //        product.Add(new Product(8, "DEKO", "DKID600W", "Незаменимый помощник для сверления отверстий в различных материалах", "DEKO_DKID600W.jpg", 1199, "Дрель"
            //, null, null, null, 2800, 230, 1.8, null, null, null));
            //        product.Add(new Product(9, "Endever", "Spectre-2020", "Порадует вас надежностью и значительным ресурсом ключевого патрона", "Endever_Spectre-2020.jpg", 1499, "Дрель"
            //, null, null, null, 2600, 220, 3, null, null, null));
            //        product.Add(new Product(10, "FinePower", "FPD500", "Подходит для сверления не только металла, керамики, древесины, пластика и других не требующих ударного воздействия материалов, но и бетона",
            //            "FinePower_FPD500.jpg", 1499, "Дрель", null, null, null, 3000, 220, 1.72, null, null, null));


            //        product.Add(new Product(11, "Bosch", "GBH 180-LI Professional", "Универсальный помощник для сверления и долбления отверстий в бетоне, камне, кирпиче и других материалах",
            //            "Bosch_GBH_180-LI_Professional.jpg", 13499, "Перфоратор", null, null, null, null, null, null, 2, 90, "360x130x480 мм"));
            //        product.Add(new Product(12, "RedVerg", "Basic RH2-22", "Компактный и легкий инструмент, предназначенный для сверления отверстий в твердых материалах",
            //           "RedVerg_Basic_RH2-22.jpg", 2450, "Перфоратор", null, null, null, null, null, null, 1.5, 95, "270x360x100 мм"));
            //        product.Add(new Product(13, "FinePower", "Basic RHB0120", "Электроинструмент предназначен для перфорации в бетоне, кирпиче и камне, а также для легких долбежных работ",
            //           "FinePower_Basic_RHB0120.jpg", 2499, "Перфоратор", null, null, null, null, null, null, 1.2, 95, "370x130x340 мм"));
            //        product.Add(new Product(14, "Вихрь", "П-750", "Подходит для любых бытовых работ",
            //"Вихрь_П-750.jpg", 3399, "Перфоратор", null, null, null, null, null, null, 1.8, 99, "480х460х310 мм"));
            //        product.Add(new Product(15, "Ставр", "ПЭГ-650М", "Для долбления, сверления и сверления с ударом в таких поверхностях как дерево, бетон, металл",
            //"Ставр_ПЭГ-650М_.jpg", 3299, "Перфоратор", null, null, null, null, null, null, 2.2, 92, "426x108x426 мм"));

            //_products = db;
        }

        public Product GetProductById(int id)
        {
            PowerToolsEntities db = new PowerToolsEntities();

            Product product = null;

            product = db.Products.First(row => row.Id == id);
            //product = _products.First(row => row.Id == 1);
            //product = _products.Where(row => row.Categorie.Equals("Болгарка"));

            //product = _products.Where(row => row.Categorie.Equals("Болгарка")).OrderBy(row => row.Id == id);
            //product = _products.Select(p => p.Categorie="Болгарка");

            return product;
        }


        public dynamic ViewBagCategories(List<string> CategoryName)
        {
            ProductsRepository pr = new ProductsRepository();
            IEnumerable<Category> model1 = pr.GetCategory();
            CategoryName = new List<string>();

            foreach (var cat in model1)
            {
                CategoryName.Add(cat.Name);


            }
            return CategoryName;
        }



        public void SaveProduct(Product product)
        {
            PowerToolsEntities db = new PowerToolsEntities();

            if (product.Id == 0 && product.ImageData != null && product.CategoryId != 0)
            {
                db.Products.Add(product);

            }

            else
            {
                Product dbEntry = db.Products.Find(product.Id);
                if (dbEntry != null && product.CategoryId != 0)
                {
                    dbEntry.Brand = product.Brand;
                    dbEntry.Model = product.Model;
                    dbEntry.Description = product.Description;
                    //dbEntry.Image = product.Image;
                    dbEntry.Price = product.Price;
                    dbEntry.CategoryId = product.CategoryId;

                    dbEntry.Power = product.Power;
                    dbEntry.DiskDiameter = product.DiskDiameter;
                    dbEntry.DiameterHole = product.DiameterHole;

                    dbEntry.Revs = product.Revs;
                    dbEntry.MainsVoltage = product.MainsVoltage;
                    dbEntry.Weight = product.Weight;

                    dbEntry.MaximumImpact = product.MaximumImpact;
                    dbEntry.NoiseLevel = product.NoiseLevel;
                    dbEntry.Dimensions = product.Dimensions;


                    if (dbEntry.ImageData == null)
                    {
                        dbEntry.ImageData = product.ImageData;
                        dbEntry.ImageMimeType = product.ImageMimeType;
                    }


                }

            }

            db.SaveChanges();

        }


        public void SaveUser(User user)
        {
            PowerToolsEntities db = new PowerToolsEntities();

            if (user.Id == 0)
            {
                db.Users.Add(user);

            }

            else
            {
                User dbEntry = db.Users.Find(user.Id);
                if (dbEntry != null)
                {
                    dbEntry.FirstName = user.FirstName;
                    dbEntry.LastName = user.LastName;
                    dbEntry.Email = user.Email;
                    dbEntry.Username = user.Username;
                    dbEntry.Password = user.Password;
                    dbEntry.ConfirmPassword = user.ConfirmPassword;
                    dbEntry.StatusID = user.StatusID;


                }

            }
            db.SaveChanges();

        }

    }

}