using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    /// <summary>
    /// Klasa koja sadrži logiku za upravljanje sa objektima tipa <see cref="Log"/>
    /// </summary>
    public class Zapisnik
    {
        #region Fields

        /// <summary>
        /// Instanca klase <see cref="IDbPomocnik"/>
        /// </summary>
        IDbPomocnik _dbPomocnik;

        #endregion

        #region Konstruktori

        /// <summary>
        /// Kreira novu instancu klase <see cref="Zapisnik"/>
        /// </summary>
        public Zapisnik()
        {
            _dbPomocnik = new MsSqlPomocnik();
        }

        public Zapisnik(IDbPomocnik dbPomocnik)
        {
            _dbPomocnik = dbPomocnik;
        }

        #endregion

        #region Metode


        /// <summary>
        /// Kreira zapis
        /// <example>
        ///     0 - Debug
        ///     1 - Informacija
        ///     2 - Upozorenje
        ///     3 - Greška
        /// </example>
        /// </summary>
        public void Zapisi(Log zapis)
        {
            _dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.DODAJ_LOG, zapis);
        }

        /// <summary>
        /// Kreira zapis
        /// <example>
        ///     0 - Debug
        ///     1 - Informacija
        ///     2 - Upozorenje
        ///     3 - Greška
        /// </example>
        /// </summary>
        /// <param name="sadrzaj"></param>
        /// <param name="tip"></param>
        public void Zapisi(string sadrzaj, int tip = 1)
        {
            if(_dbPomocnik == null)
            {
                var d = DateTime.Now;
                string logTip = "";
                string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "Logs");
                switch(tip) { case 0: logTip = "Debug"; break; case 1:logTip = "Informational"; break; case 2:logTip = "Upozorenje"; break; case 3:logTip = "Greska"; break; };
                if(!System.IO.Directory.Exists(path))
                    System.IO.Directory.CreateDirectory(path);
                System.IO.File.AppendAllText(System.IO.Path.Combine(path, string.Format(@"{0}-{1}-{2}-{3}.txt", d.Year, d.Month, d.Day, logTip)), sadrzaj);
            }

            Dictionary<string, object> parametri = new Dictionary<string, object> {
                {"Sadrzaj", sadrzaj},
                {"Tip", tip},
                {"Datum", DateTime.Now}
            };
            try
            {
                _dbPomocnik.IzvrsiProceduru(Konstante.StoredProcedures.DODAJ_LOG, parametri);
            }
            catch { }
        }

        #endregion
    }
}
