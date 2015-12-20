using Online_Stamparija.Models;
using Online_Stamparija.Models.MenuItems;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Online_Stamparija.Controllers
{
    public class MaterijaliController: Controller
    {
        public ActionResult Index()
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var materijali = new List<Materijal>();
                try
                {
                    var dbPomocnik = new OnliStam.Pomocnici.MySqlPomocnik();
                    var materijal = dbPomocnik.IzvrsiProceduru<Materijal, Materijal>(Konstante.StoredProcedures.DAJ_MATERIJALE, new Models.Materijal());
                    materijali.AddRange(materijal);

                    if(materijali.Any(m => m.Kolicina < 10))
                        StandardniUiElementi.glavnaDugmad.FirstOrDefault(x => x.ImageUrl == "/Images/appbar.brick.png").Upozorenje = true;
                    else
                        StandardniUiElementi.glavnaDugmad.FirstOrDefault(x => x.ImageUrl == "/Images/appbar.brick.png").Upozorenje = false;


                    TempData["BocnaDugmad"] = new List<MetroItem> { 
                    new Online_Stamparija.Models.MenuItems.MetroItem
                    {
                        LinkUrl = "/Materijali/Create",
                        ImageUrl = "/Images/novi.materijal.B.png",
                        Title="Novi Materijal",
                        MinimumAllowedPosition = PozicijaEnum.Menadzer
                    }};
                }
                catch(Exception ex)
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
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                var model = new Materijal();
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    model = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", id } });
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
        [HttpPost]
        public ActionResult Create([System.Web.Http.FromBody] Materijal model)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    var response = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DODAJ_MATERIJAL, model);

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
                var dbPomocnik = new MySqlPomocnik();
                Materijal model = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", id } });
                return View("Uredi", model);
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
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    IDbPomocnik dbPomocnik = new MySqlPomocnik();
                    dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.IZMJENI_MATERIJAL, model);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Index");
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
                var model = new Materijal();
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    model = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", id } });
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
                    dbPomocnik.IzvrsiProceduru<Posao>(Konstante.StoredProcedures.IZBRISI_MATERIJAL, new Dictionary<string, object> { { "ID", id } });
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

        /// <summary>
        /// Predaje ID materijala koji su korišteni za određeni posao
        /// </summary>
        /// <param name="posaoId"></param>
        /// <returns></returns>
        [HttpGet]
        public string DajMaterijaleZaPosao(int posaoId)
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Logovan)
            {
                var response = new List<ActionResult>();
                var dbPomocnik = new MySqlPomocnik();
                var materijali = dbPomocnik.DajKolekciju<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJALE_ZA_POSAO, new Dictionary<string, object> { { "PosaoId", posaoId } });

                var m1 = PartialView("Materijal", materijali.FirstOrDefault());
                var s2 = m1.RenderToString();
                s2 = string.Empty;
                return string.Join(" ", materijali.Select(m => PartialView("~/Views/Shared/Materijal.cshtml", m).RenderToString()).ToList());
            }
            return string.Empty;
        }

        [HttpGet]
        public ActionResult DajMaterijalPartialView(int materijalId)
        {
            try
            {
                var dbPomocnik = new MySqlPomocnik();
                var materijal = dbPomocnik.IzvrsiProceduru<Materijal>(Konstante.StoredProcedures.DAJ_MATERIJAL_ID, new Dictionary<string, object> { { "ID", materijalId } });
                return PartialView("Materijal", materijal);
            }
            catch(Exception ex)
            {
                return PartialView("Error", ex.Message);
            }
        }

        [HttpGet]
        public List<Materijal> DajSve()
        {
            var dbPomocnik = new MySqlPomocnik();
            return dbPomocnik.IzvrsiProceduru<Materijal, Materijal>(Konstante.StoredProcedures.DAJ_MATERIJALE, new Models.Materijal());
        }

        [HttpGet]
        public ActionResult DajSveJSON()
        {

            return Json(DajSve(), JsonRequestBehavior.AllowGet);
        }
    }

    public static class ViewExtension
    {
        public static string RenderToString(this PartialViewResult partialView) {
            var httpContext = HttpContext.Current;

            if(httpContext == null)
                throw new Exception("");
            var controllerName = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();

            var controller = (ControllerBase)ControllerBuilder.Current.GetControllerFactory().CreateController(httpContext.Request.RequestContext, controllerName);

            var controllerContext = new ControllerContext(httpContext.Request.RequestContext, controller);

            var view = ViewEngines.Engines.FindPartialView(controllerContext, partialView.ViewName ).View;

            var sb = new StringBuilder();
            using(var sw = new StringWriter(sb)) {
                using(var htw = new HtmlTextWriter(sw)){
                    view.Render(new ViewContext(controllerContext, view, partialView.ViewData, partialView.TempData, htw), htw);
                }
            }
            return sb.ToString();
        }
    }
}




















