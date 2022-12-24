using SysorovShop.Abstract;
using SysorovShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;

namespace SysorovShop.Controllers
{
    public class AdminController : Controller
    {
        ProductsRepository repository;

        public AdminController()
        {
        }
        //продукт
        public ViewResult Index(string category = null)
        {
            ProductsRepository pr = new ProductsRepository();
            IEnumerable<Product> products = pr.GetProducts(category);
       
            ViewBag.category = category;

            return View(products);
        }

        public ViewResult Edit(int id)
        {
            ProductsRepository pr = new ProductsRepository();
            PowerToolsEntities db = new PowerToolsEntities();

            Product products = null;
            products = db.Products.FirstOrDefault(b => b.Id == id);

            return View(products);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository pr = new ProductsRepository();
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                pr.SaveProduct(product);
                TempData["message"] = string.Format("Изменение информации о продукте \"{0}\" сохранены!", product.Brand + " " + product.Model);
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ViewResult Insert()
        {
            PowerToolsEntities db = new PowerToolsEntities();
            List<Category> categories = db.Categories.ToList();
            categories.Insert(0, new Category { Name = "----------", Id = 0 });

            Product product = new Product
            {
                Categories = new SelectList(categories, "Id", "Name"),

            };

            return View(product);
        }

        [HttpPost]
        public ActionResult Insert(Product product, HttpPostedFileBase image = null)
        {
            if (image != null)
            {
                product.ImageMimeType = image.ContentType;
                product.ImageData = new byte[image.ContentLength];
                image.InputStream.Read(product.ImageData, 0, image.ContentLength);
            }

            ProductsRepository pr = new ProductsRepository();
            TempData["message"] = string.Format("Вы добавили продукт \"{0}\"!", product.Brand + " " + product.Model);
            pr.SaveProduct(product);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteProduct(int? id)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }
        [HttpPost]
        public ActionResult DeleteProduct(Product prod, int? id)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            Product product = new Product();

            try
            {

                //if (ModelState.IsValid)
                //{
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                product = db.Products.Find(id);
                if (product == null)
                {
                    return HttpNotFound();
                }
                TempData["message"] = string.Format("Вы удалили товар \"{0}\"!", product.Brand + " " + product.Model);
                db.Products.Remove(product);
                db.SaveChanges();


                return RedirectToAction("Index");
                //}
                //    return View(product);

            }
            catch
            {
                return View();
            }

        }

        //пользователь
        public ViewResult ListUsers()
        {
            PowerToolsEntities db = new PowerToolsEntities();

            IEnumerable<User> users = db.Users;

            return View(users);
        }


        public ViewResult EditUser(int id)
        {
            ProductsRepository pr = new ProductsRepository();
            PowerToolsEntities db = new PowerToolsEntities();
            User user = null;
            user = db.Users.FirstOrDefault(b => b.Id == id);
            return View(user);

        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {


            if (ModelState.IsValid)
            {
                ProductsRepository pr = new ProductsRepository();

                pr.SaveUser(user);
                TempData["message"] = string.Format("Данные пользователя \"{0}\" успешно сохранены!", user.FirstName + " " + user.LastName);
                return RedirectToAction("ListUsers");
            }

            return View(user);
        }

        public ViewResult InsertUser(string statusid)
        {
            PowerToolsEntities db = new PowerToolsEntities();
 
            List<User> status = db.Users.ToList();

            User us = new User
            {
                Status = new SelectList(status, "Id", "StatusID"),

            };
            return View(us);
        }

        [HttpPost]
        public ActionResult InsertUser(User user)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            var usr = db.Users.FirstOrDefault(u => u.Username == user.Username);
            if (usr == null)
            {
                ProductsRepository pr = new ProductsRepository();
                pr.SaveUser(user);
                return RedirectToAction("ListUsers");
            }
            else { 
                ModelState.AddModelError("", "Этот логин уже занят!");   
            }
            List<User> status = db.Users.ToList();
            User us = new User
            {
                Status = new SelectList(status, "Id", "StatusID"),

            };
            return View(us);


            //var us = db.Users.FirstOrDefault(x => x.Username == user.Username);
            //if (us == null)
            //{
            //    ProductsRepository pr = new ProductsRepository();
            //    TempData["message"] = string.Format("Вы добавили нового пользователя \"{0}\"!", user.FirstName + " " + user.LastName);
            //    pr.SaveUser(user);
            //    return RedirectToAction("ListUsers");
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Логин занят!");
            //    TempData["message"] = string.Format("Пользователь \"{0}\" не добавлен, логин совпадает с уже зарегистрированным пользователем!", user.FirstName + " " + user.LastName);
            //}
            //return RedirectToAction("InsertUser");



            //return RedirectToAction("InsertUser");
            ////ProductsRepository pr = new ProductsRepository();
            //TempData["message"] = string.Format("Вы добавили нового пользователя \"{0}\"", user.FirstName + " " + user.LastName);
            //pr.SaveUser(user);
            //return RedirectToAction("ListUsers");

        }




        public ActionResult DeleteUser(int? id)
        {
            PowerToolsEntities db = new PowerToolsEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [HttpPost]
        public ActionResult DeleteUser(User us, int? id)
        {
            PowerToolsEntities db = new PowerToolsEntities();

            User user = new User();

            try
            {
                //if (ModelState.IsValid)
                //{
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user = db.Users.Find(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                db.Users.Remove(user);
                db.SaveChanges();
                TempData["message"] = string.Format("Вы удалили пользователя \"{0}\"!", user.FirstName + " " + user.LastName);
                return RedirectToAction("ListUsers");

                //}
                //return View(user);

            }
            catch
            {
                return View();
            }

        }

    }
}