using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    public class MySqlPomocnik: IDbPomocnik
    {

        #region ***** CONSTANTS ****

        private readonly string CONNECTION_STRING = "";

        #endregion

        #region ****** FIELDS ******



        #endregion

        #region **** PROPERTIES ****



        #endregion

        #region *** CONSTRUCTORS ***

        public MySqlPomocnik()
        {
            try
            {
                CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            }
            catch(ConfigurationErrorsException ceex)
            {
                //Moguće logovanje izuzetka ...
                throw new Exception("Konfiguracioni fajl ne sadrži connectionString!", ceex);
            }
            catch(Exception)
            {
                throw;
            }
        }

        #endregion

        #region ****** METHODS *****

        /// <summary>
        /// Metoda za izvršavanje procedure na Bazi podataka
        /// </summary>
        /// <param name="NazivProcedure"></param>
        /// <param name="parametri"></param>   
        /// <returns></returns>
        public System.Data.DataTable IzvrsiProceduru(string NazivProcedure, Dictionary<string, object> parametri)
        {
            using(MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(NazivProcedure, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                if(parametri != null)
                    foreach(KeyValuePair<string, object> parametar in parametri)
                    {
                        cmd.Parameters.Add(new MySqlParameter("@" + parametar.Key, parametar.Value));
                    }

                var odgovor = new System.Data.DataTable();
                using(MySqlDataReader rdr = cmd.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        List<object> parametri1 = new List<object>();
                        for(int i = 0; i < rdr.FieldCount; i++)
                        {
                            if(!odgovor.Columns.Contains(rdr.GetName(i)))
                                odgovor.Columns.Add(rdr.GetName(i));
                            parametri1.Add(rdr.GetValue(i));
                        }
                        odgovor.Rows.Add(parametri1.ToArray());
                    }
                }
                return odgovor;
            }
        }

        public T IzvrsiProceduru<T>(string imeProcedure, Dictionary<string, object> parametri) where T: new()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable IzvrsiProceduru<T>(string imeProcedure, T model)
        {
            throw new NotImplementedException();
        }

        public List<T2> IzvrsiProceduru<T1, T2>(string imeProcedure, T1 model) where T2: new()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet IzvrsiStoredProceduru(string imeProcedure, Dictionary<string, object> parametri)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
