using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SysorovShop.Models;

namespace SysorovShop.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            using (PowerToolsEntities db = new PowerToolsEntities())
            {
                return View(db.Users.ToList());
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User account)
        {
            if (ModelState.IsValid)
            {
                using (PowerToolsEntities db = new PowerToolsEntities())
                {
                    var us = db.Users.FirstOrDefault(x => x.Username == account.Username);

                    if (us == null)
                    {
                        db.Users.Add(account);
                        db.SaveChanges();

                        if (us == null)
                        {
                            var usr = db.Users.FirstOrDefault(u => u.Username == account.Username && u.Password == account.Password);

                            Session["UserID"] = usr.Id.ToString();
                            Session["FirstName"] = usr.FirstName.ToString();
                            Session["LastName"] = usr.LastName.ToString();
                            Session["Status"] = usr.StatusID;

                            return RedirectToAction("Index", "Home");
                        }
                    }

                    if (us != null)
                    {
                        ModelState.AddModelError("", "Логин занят!");

                    }

                }

            }
            //if (ModelState.IsValid)
            //{
            //    using (PowerToolsEntities db = new PowerToolsEntities())
            //    {
            //        try
            //        {
            //            var usr = db.Users.Single(u => u.Username != account.Username);

            //            if (usr != null)
            //            {
            //                ModelState.AddModelError("", "Логин занят!");

            //            }

            //        }
            //        catch
            //        {
            //            db.Users.Add(account);
            //            db.SaveChanges();


            //            try
            //            {
            //                var usr = db.Users.Single(u => u.Username == account.Username && u.Password == account.Password);


            //                if (usr != null)
            //                {

            //                    Session["UserID"] = usr.Id.ToString();
            //                    Session["FirstName"] = usr.FirstName.ToString();
            //                    Session["LastName"] = usr.LastName.ToString();
            //                    Session["Status"] = usr.StatusID;



            //                    //Session["StatusID"] = usr.FirstName.ToString();



            //                    return RedirectToAction("Index", "Home");
            //                }
            //            }
            //            catch
            //            {
            //                ModelState.AddModelError("", "Логин занят!");
            //            }


            //        }

            //    }
            //}




            return View();
        }
        //Вход
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            PowerToolsEntities db = new PowerToolsEntities();

            var usr = db.Users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (usr != null)
            {
                Session["UserID"] = usr.Id.ToString();
                Session["FirstName"] = usr.FirstName.ToString();
                Session["LastName"] = usr.LastName.ToString();
                Session["Status"] = usr.StatusID;

                return RedirectToAction("Index", "Home");
            }
            if (user.Username != null && user.Password != null)
            {
                ModelState.AddModelError("", "Неверный логин или пароль!");
                return View();
            }


            //using (PowerToolsEntities db = new PowerToolsEntities())
            //{
            //    try
            //    {
            //        var usr = db.Users.Single(u => u.Username == user.Username && u.Password == user.Password);


            //        if (usr != null)
            //        {

            //            Session["UserID"] = usr.Id.ToString();
            //            Session["FirstName"] = usr.FirstName.ToString();
            //            Session["LastName"] = usr.LastName.ToString();
            //            Session["Status"] = usr.StatusID;


            //            return RedirectToAction("Index", "Home");
            //        }
            //        else
            //        {
            //            ModelState.AddModelError("", "Имя пользователя или пароль неверны!");

            //        }
            //    }
            //    catch
            //    {
            //        ModelState.AddModelError("", "Имя пользователя или пароль неверны!");
            //    }

            //}
            return View();
        }

        public ActionResult LoggedIn()
        {
           

            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }


        }
        public ActionResult AccountExit()
        {
            Session["UserId"] = null;
            Session["FirstName"] = null;
            Session["LastName"] = null;
            Session["Status"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}