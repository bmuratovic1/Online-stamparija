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
        public string Email {
            get; // { return _instanca.Email; }
            set; // { _instanca.Email = value; }
        }

        public string Password {
            get; // { return _instanca.Password; }
            set; // { _instanca.Password = value; }
        }

        public int Pozicija {
            get; // { return _instanca.Pozicija; }
            set; // { _instanca.Pozicija = value; }
        }

        public static LogovaniKorisnik Instanca
        {
            get
            {
                lock(_brava)
                {
                    var TRAJANjE_SESIJE = 1;
                    HttpContext.Current.Session.Timeout = TRAJANjE_SESIJE;

                    if(HttpContext.Current.Session["sesija"] == null)
                    {
                        _instanca = new Models.LogovaniKorisnik();
                        HttpContext.Current.Session.Add("sesija", _instanca);
                    }

                    if(HttpContext.Current.Session["loginPokusaji"] == null)
                        HttpContext.Current.Session.Add("loginPokusaji", 0);
                    return _instanca;
                }
            }
        }

        private int LoginPokusaji
        {
            get
            {
                return (int)(HttpContext.Current.Session["loginPokusaji"]);
            }
            set
            {
                HttpContext.Current.Session.Add("loginPokusaji", value);
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
            int MAKS_LOGIN_POKUSAJ = 5; // iy config
            if(LoginPokusaji > MAKS_LOGIN_POKUSAJ)
            {
                throw new UnauthorizedAccessException("Nemoj džaba!");
            }
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
                    LoginPokusaji++;
                    throw new ArgumentException("Pogresna sifra!");
                }
            }
            else
            {
                LoginPokusaji++;
                throw new ArgumentException("Pogresni podaci!");
            }

        }

        internal void LogOut()
        {
            _instanca = new LogovaniKorisnik();
        }

        #endregion

        public bool Logovan
        {
            get
            {
                return !string.IsNullOrEmpty(UserName);
            }
        }
    }
}