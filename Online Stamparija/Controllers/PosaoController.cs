﻿using Online_Stamparija.Models;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Online_Stamparija.Controllers
{
    public class PosaoController: Controller
    {
        // GET: Posao
        public ActionResult Index()
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var poslovi = new List<Posao>();
                try
                {
                    var dbPomocnik = new OnliStam.Pomocnici.MySqlPomocnik();
                    var poslovi_1 = dbPomocnik.IzvrsiProceduru<Posao, Posao>(Konstante.StoredProcedures.DAJ_NEZAVRSENE_POSLOVE, new Models.Posao());
                    var poslovi_2 = dbPomocnik.IzvrsiProceduru<Posao, Posao>(Konstante.StoredProcedures.DAJ_ZAVRSENE_POSLOVE, new Posao());
                    poslovi.AddRange(poslovi_1);
                    poslovi.AddRange(poslovi_2);
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return View(poslovi);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Posao/Details/5
        public ActionResult Details(int id)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var model = new Posao();
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    model = dbPomocnik.IzvrsiProceduru<Posao>(Konstante.StoredProcedures.DAJ_POSAO_ID, new Dictionary<string, object> { { "ID", id } });
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return View("Detalji", model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Posao/Create
        public ActionResult Create()
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                return View("Novi");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Posao/Create
        [HttpPost]
        public ActionResult Create([System.Web.Http.FromBody] Posao model)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    var response = dbPomocnik.IzvrsiProceduru<Posao>(Konstante.StoredProcedures.DODAJ_POSAO, model);

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Posao/Edit/5
        public ActionResult Edit(int id)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                return View("Uredi");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Posao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Posao model)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    TempData["Error"] = "Operacija nije podrzana!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // GET: Posao/Delete/5
        public ActionResult Delete(int id)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var model = new Posao();
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    model = dbPomocnik.IzvrsiProceduru<Posao>(Konstante.StoredProcedures.DAJ_POSAO_ID, new Dictionary<string, object> { { "ID", id } });
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return View("Obrisi", model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // POST: Posao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    // TODO: Add delete logic here
                    TempData["Error"] = "Operacija nije podrzana!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
