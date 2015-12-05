using Online_Stamparija.Models;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Stamparija.Controllers
{
    public class MaterijaliController:Controller
    {
        public ActionResult Index()
        {
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var materijali = new List<Materijal>();
                try
                {
                    var dbPomocnik = new OnliStam.Pomocnici.MySqlPomocnik();
                    var materijal= dbPomocnik.IzvrsiProceduru<Materijal,Materijal>(Konstante.StoredProcedures.DAJ_MATERIJALE,new Models.Materijal());
                    materijali.AddRange(materijal);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
                return View(materijali);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult Details(int id)
        {
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var model = new Materijal();
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    model = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", id } });
                }
                catch (Exception ex)
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
        public ActionResult Create()
        {
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                return View("Novi");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Create([System.Web.Http.FromBody] Materijal model)
        {
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    var response = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DODAJ_MATERIJAL, model);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
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
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
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
        public ActionResult Edit(int id, Materijal model)
        {
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
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
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var model = new Materijal();
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    model = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", id } });
                }
                catch (Exception ex)
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
            if (Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
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