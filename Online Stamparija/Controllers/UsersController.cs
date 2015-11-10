using Online_Stamparija.Models;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
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
            if(LogovaniKorisnik.Instanca.UserName != null && LogovaniKorisnik.Instanca.Pozicija == "Admin")
            {
                var korisnici = _dbPomocnik.IzvrsiProceduru<Korisnik, Korisnik>(Konstante.StoredProcedures.DAJ_SVE_KORISNIKE, new Korisnik());
                //var korisnici = new List<Korisnik> {
                //    new  Korisnik{ UserName="Ajla", Password="******", Email="ajla@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Berina", Password="******", Email="berina@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Dzenana", Password="******", Email="dzenana@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Haris", Password="******", Email="haris@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Kerim", Password="******", Email="kerim@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Maja", Password="******", Email="maja@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Snjezana", Password="******", Email="snjezana@onlineStamparija.com"},
                //    new  Korisnik{ UserName="Rijad", Password="******", Email="rijad@onlineStamparija.com"}
                //};

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
            return Redirect("http://4.bp.blogspot.com/-eWEqnCFmvjk/Um4jSIS1aKI/AAAAAAAANMA/djYY2j3k5W4/s1600/under+construction+sign+const.jpg");
            return View();
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

        // GET: Users/Delete/5
        public ActionResult Delete(int id)
        {
            return Redirect("http://4.bp.blogspot.com/-eWEqnCFmvjk/Um4jSIS1aKI/AAAAAAAANMA/djYY2j3k5W4/s1600/under+construction+sign+const.jpg");
            return View();
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

        #endregion

    }
}
