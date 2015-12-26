using OnliStam.Dashboard.Model;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Dashboard
{
    public class Servisi
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

        public void UporediBaze(string connectionString)
        {
            Table t = new Table();
            //t.UporediTabele();
        }

        List<Table> DajTabele(string connectionString1, string connectionString2)
        {
            var dbPomocnik = new MySqlPomocnik();

            var c1 = new ConnectionString(connectionString1);
            var c2 = new ConnectionString(connectionString2);

            var tabele1 = DajTabele(c1.Database, dbPomocnik);
            var tabele2 = DajTabele(c2.Database, dbPomocnik);

            return null;
        }

        private static List<Table> DajTabele(string database, IDbPomocnik dbPomocnik)
        {
            var odgovor = new List<Table>();
            var odg = dbPomocnik.IzvrsiProceduru(Konstante.SqlProcedure.DAJ_SVE_TABELE, new Dictionary<string, object> { { "schemaName", database } });
            if(odg != null && odg.Rows.Count > 0)
            {
                for(int i = 0; i < odg.Rows.Count; i++)
                {
                    odgovor.Add(new Table { Ime = odg.Rows[i]["table_name"].ToString() });
                }
            }
            return odgovor;
        }

        #endregion
    }

    public static class Extensions
    {
        public static bool UporediSaTabelom(this Table t1, Table t2)
        {
            foreach(var kolona in t1.Kolone)
            {
                var k2 = t2.Kolone.FirstOrDefault(k => k.Ime == kolona.Ime);
                if(k2 == null || !kolona.UporediSaKolonom(k2))
                    return false;
            }
            return true;
        }

        public static bool UporediSaKolonom(this Kolona k1, Kolona k2)
        {
            return k1.Tip == k2.Tip;
        }
    }
}
