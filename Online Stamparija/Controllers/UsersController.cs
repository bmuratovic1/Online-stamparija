using Online_Stamparija.Models;
using Online_Stamparija.Models.MenuItems;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace Online_Stamparija.Controllers
{
    public class UsersController: Controller
    {
        #region +++ FIELDS +++

        IDbPomocnik _dbPomocnik;

        #endregion

        #region +++ CONSTRUCTORS +++

        public UsersController()
        {
            _dbPomocnik = new MySqlPomocnik();
        }

        #endregion

        #region +++ METHODS +++

        // GET: Users
        public ActionResult Index()
        {
            if(LogovaniKorisnik.Instanca.Logovan && LogovaniKorisnik.Instanca.Pozicija == (int)PozicijaEnum.Administrator)
            {
                var korisnici = new List<Korisnik>();
                try
                {
                    korisnici = _dbPomocnik.IzvrsiProceduru<Korisnik, Korisnik>(Konstante.StoredProcedures.DAJ_SVE_KORISNIKE, new Korisnik());

                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }

                TempData["BocnaDugmad"] = new List<MetroItem> { 
                    new Online_Stamparija.Models.MenuItems.MetroItem
                    {
                        LinkUrl = "javascript: pokaziSakrij('prosireaDesnaTraka'); pokaziSakrij('obicnaDesnaTraka')",
                        ImageUrl = "/Images/addUser.B.png",
                        Title="Novi Korisnik",
                        MinimumAllowedPosition = PozicijaEnum.Radnik
                    },
                    new Online_Stamparija.Models.MenuItems.MetroItem
                    {
                        LinkUrl = "#",
                        ImageUrl = "/Images/settings.B.png",
                        Title="Podešavanja",
                        MinimumAllowedPosition = PozicijaEnum.Menadzer
                    }
                };

                return View(korisnici);
            }
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return RedirectToAction("Index", "Home");
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            if(LogovaniKorisnik.Instanca.Logovan && LogovaniKorisnik.Instanca.Pozicija == 1)
            {
                return View("Novi");
            }
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return RedirectToAction("Index", "Home");
        }

        // POST: Users/Create
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

        // GET: Users/Edit/5
        public ActionResult Edit(int id)
        {
            return Redirect("http://4.bp.blogspot.com/-eWEqnCFmvjk/Um4jSIS1aKI/AAAAAAAANMA/djYY2j3k5W4/s1600/under+construction+sign+const.jpg");
            return View();
        }

        // POST: Users/Edit/5
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

        // GET: Users/Ban/5
        public ActionResult Ban(int id)
        {
            if(LogovaniKorisnik.Instanca.Logovan && LogovaniKorisnik.Instanca.Pozicija == 1)
            {
                _dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.BANUJ_KORISNIKA,
                    new Dictionary<string, object> { { "ID", id } });
            }
            return RedirectToAction("Index");
        }

        // GET: Users/Unban/5
        public ActionResult Unban(int id)
        {
            if(LogovaniKorisnik.Instanca.Logovan && LogovaniKorisnik.Instanca.Pozicija == 1)
            {
                try
                {
                    _dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.ODBANUJ_KORISNIKA,
                        new Dictionary<string, object> { { "ID", id } });
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }

        // POST: Users/Delete/5
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


        public ActionResult UserProfile() { return View("UserProfile"); }

        #endregion

    }
}
