using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Stamparija.Models
{
    /// <summary>
    /// Čuva podatke o trenutno logovanom korisniku
    /// </summary>
    public class LogovaniKorisnik
    {
        #region --- Fields ---

        /// <summary>
        /// Instanca logovanok korisnika
        /// </summary>
        static LogovaniKorisnik _instanca;

        /// <summary>
        /// brava za višenitne pristupe
        /// </summary>
        static object _brava = new object();

        #endregion

        #region --- Properties ---

        /// <summary>
        /// Korisničko ime prijavljenog korisnika
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Email prijavljenog korisnika
        /// </summary>
        public string Email { get; set; }

        public string Password { get; set; }

        public string Pozicija { get; set; }

        public static LogovaniKorisnik Instanca
        {
            get
            {
                lock(_brava)
                {
                    if(HttpContext.Current.Session["sesija"] == null)
                        HttpContext.Current.Session.Add("sesija", new Models.LogovaniKorisnik());
                    return HttpContext.Current.Session["sesija"] as LogovaniKorisnik;
                }
            }
        }

        #endregion

        #region --- Konstruktori ---

        LogovaniKorisnik()
        {

        }

        #endregion

        #region --- Metode ---

        /// <summary>
        /// Metoda za provjeru korisničkih podataka
        /// </summary>
        /// <param name="userName">Korisničko ime</param>
        /// <param name="password">Lozinka</param>
        internal void Login(string userName, string password)
        {
            OnliStam.Pomocnici.IDbPomocnik db = new OnliStam.Pomocnici.MySqlPomocnik();
            Zapisnik zapisnik = new Zapisnik(db);
            //zapisnik.Zapisi("Pokušaj logina sa username = " + userName + " i lozinka = " + password, 1);

            //var dbResponse = db.IzvrsiProceduru(Konstante.StoredProcedures.DAJ_KORISNIKA_UNAME_PASS.SaParametrima{UserName= userName, Password = password }, new Korisnik());
            var korisnik = db.IzvrsiProceduru<Korisnik>(Konstante.StoredProcedures.DAJ_KORISNIKA_UNAME_PASS, new Dictionary<string, object> { { "UserName", userName } });
            if(korisnik != null && !string.IsNullOrEmpty(korisnik.UserName))
            {
                if(KriptoPomocnik.VerifyMd5Hash(password, korisnik.Password))
                {
                    UserName = userName;
                    Email = korisnik.Email;
                    Pozicija = korisnik.Pozicija;
                }
                else
                {
                    throw new ArgumentException("Pogresna sifra!");
                }
            }
            else
            {
                throw new ArgumentException("Pogresni podaci!");
            }

        }

        internal void LogOut()
        {
            UserName = null;
            Email = null;
        }

        #endregion
    }
}