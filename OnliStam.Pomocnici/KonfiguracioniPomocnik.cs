using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    public static class KonfiguracioniPomocnik
    {

        #region ***** CONSTANTS ****



        #endregion

        #region ****** FIELDS ******



        #endregion

        #region **** PROPERTIES ****



        #endregion

        #region *** CONSTRUCTORS ***



        #endregion

        #region ****** METHODS *****

        public static string DajSqlKonekciju(string imeKonekcije)
        {
            return ConfigurationManager.ConnectionStrings[imeKonekcije].ConnectionString;
        }

        public static List<ConnectionString> DajBaze()
        {
            var odgovor = new List<ConnectionString>();
            foreach(ConnectionStringSettings cs in ConfigurationManager.ConnectionStrings)
            {
                odgovor.Add(new ConnectionString(cs.ConnectionString));
            }
            return odgovor;
        }

        #endregion
    }
}
