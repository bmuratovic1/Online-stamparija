using Online_Stamparija.Models;
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
                var dbPomocnik = new MySqlPomocnik();
                ViewBag.Materijali = dbPomocnik.IzvrsiProceduru<Materijal, Materijal>(Konstante.StoredProcedures.DAJ_MATERIJALE, new Models.Materijal());
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
                    int materijalId = Convert.ToInt32(model.VrstaMaterijala);
                    double potrebnaKolicinaMaterijala = Convert.ToDouble(model.KolicinaMaterijala);

                    var materijal = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", materijalId } });

                    if(materijal.Kolicina < potrebnaKolicinaMaterijala)
                    {
                        TempData["Error"] = "Nedovoljno raspoloživog materijala!";
                        return RedirectToAction("Index", "Home");
                    }
                    materijal.Kolicina -= potrebnaKolicinaMaterijala;

                    dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.IZMJENI_MATERIJAL, materijal);

                    model.VrstaMaterijala = materijal.Naziv;
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
                    var dbPomocnik = new MySqlPomocnik();
                    dbPomocnik.IzvrsiProceduru<Posao>(Konstante.StoredProcedures.IZBRISI_POSAO, new Dictionary<string, object> { { "ID", id } });
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

        class DajPosloveModel { public int poc { get; set; } public int kra { get; set; } }

        [HttpGet]
        public List<Posao> DajPoslove(int pocIndeks, int broj)
        {
            var dbPomocnik = new MySqlPomocnik();
            var poslovi = dbPomocnik.IzvrsiProceduru<DajPosloveModel, Posao>(new SqlUpit("n", "SELECT * FROM poslovi LIMIT @poc, @kra", new List<string>{"poc", "kra"}),
                new DajPosloveModel{ poc = pocIndeks, kra = pocIndeks + broj });
            return poslovi;
        }
    }
}
