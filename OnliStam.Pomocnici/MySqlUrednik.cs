using OnliStam.Pomocnici.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    public class MySqlUrednik: IDbUrednik
    {

        #region ***** CONSTANTS ****

        public readonly string CONNECTION_STRING = string.Empty;

        #endregion

        #region ****** FIELDS ******

        StringBuilder _stringBuilder;

        IDbPomocnik _dbPomocnik;

        #endregion

        #region **** PROPERTIES ****



        #endregion

        #region *** CONSTRUCTORS ***

        public MySqlUrednik()
        {
            CONNECTION_STRING = KonfiguracioniPomocnik.DajSqlKonekciju("MySqlConnectionString");

            _dbPomocnik = new MySqlPomocnik();
        }

        #endregion

        #region ****** METHODS *****

        /// <summary>
        /// Metoda koja dodaje novu tabelu sa prosljeđenim imenom u prosljeđenu bazu
        /// </summary>
        /// <param name="imeBaze">Ime baze u kojoj će biti nova tabela</param>
        /// <param name="imeTabele">Ime tabele koju treba kreirati</param>
        public string DodajTabelu(string imeBaze, string imeTabele)
        {
            _stringBuilder = new StringBuilder();
            _stringBuilder.Append(string.Format("CREATE TABLE {0}.{1} (", imeBaze, imeTabele));
            
            return  _stringBuilder.ToString();
        }

        public string PreimenujTabelu(string imeBaze, string staroIme, string novoIme)
        {
            throw new NotImplementedException();
        }

        public string IzbrisiTabelu(string imeBaze, string imeTabele)
        {
            throw new NotImplementedException();
        }

        public string DodajKolonu(string imeBaze, string imeTabele, string imeKolone, string tip, string kljuc, bool nulabilno, bool autoIncrement)
        {
            _stringBuilder = new StringBuilder();

            _stringBuilder.AppendFormat("{0} ", imeKolone);
            _stringBuilder.AppendFormat("{0} ", tip);
            if(!nulabilno)
                _stringBuilder.Append("NOT NULL ");
            if(autoIncrement)
                _stringBuilder.Append("AUTO_INCREMENT ");
            _stringBuilder.AppendLine(",");
            if(kljuc == "PRI")
                _stringBuilder.AppendFormat("\tPRIMARY KEY ({0}),{1}", imeKolone, Environment.NewLine);
            if(kljuc == "UNI")
                _stringBuilder.AppendFormat("\tUNIQUE KEY {0} ({0}),{1}", imeKolone, Environment.NewLine);

            return _stringBuilder.ToString();
        }

        public string PreimenijKolonu(string imeBaze, string imeTabele, string staroIme, string novoIme, string tip, string kljuc, bool nulabilno, bool autoIncrement)
        {
            throw new NotImplementedException();
        }

        public string IzbrisiKolonu(string imeBaze, string imeTabele, string imeKolone)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
