using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Stamparija.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Account/Details/5
        public ActionResult Login(Models.Korisnik model)
        {
            try
            {
                Models.LogovaniKorisnik.Instanca.Login(model.UserName, model.Password);
            }
            catch(Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout(Models.Korisnik model)
        {
            Models.LogovaniKorisnik.Instanca.LogOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Account/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Account/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Account/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
