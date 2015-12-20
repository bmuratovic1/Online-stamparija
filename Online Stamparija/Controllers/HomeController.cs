using Online_Stamparija.Models;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Stamparija.Controllers
{
    public class HomeController: Controller
    {
        public ActionResult Index()
        {
            if(Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 1 || Online_Stamparija.Models.LogovaniKorisnik.Instanca.Pozicija == 2)
            {
                try
                {
                    var dbPomocnik = new MySqlPomocnik();
                    var materijali = dbPomocnik.IzvrsiProceduru<Materijal, Materijal>(Konstante.StoredProcedures.DAJ_MATERIJALE, new Materijal());
                    if(materijali.Any(m => m.Kolicina < 10))
                    {
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.AppendLine("Sljedeći materijali će uskoro biti istrošeni:");
                        foreach(var m in materijali.Where(m1 => m1.Kolicina < 10))
                            sb.AppendFormat("\t{0} : {1}{2}", m.Naziv, m.Kolicina, Environment.NewLine);
                        if(TempData["Error"] != null)
                            TempData["Error"] += "\n\n";
                        TempData["Error"] += sb.ToString();
                        StandardniUiElementi.glavnaDugmad.FirstOrDefault(x => x.ImageUrl == "/Images/appbar.brick.png").Upozorenje = true;
                    }
                    else
                    {
                        StandardniUiElementi.glavnaDugmad.FirstOrDefault(x => x.ImageUrl == "/Images/appbar.brick.png").Upozorenje = false;
                    }
                }
                catch(Exception ex)
                {
                    TempData["Error"] = ex.Message;
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ForgotPass()
        {
            ViewBag.Message = "Enter your e-mail address:";
            return View();
        }
    }
}