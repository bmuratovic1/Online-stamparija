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
        public System.Data.DataTable IzvrsiProceduru(SqlUpit NazivProcedure, Dictionary<string, object> parametri)
        {
            using(MySqlConnection conn = new MySqlConnection(CONNECTION_STRING))
            {
                var odgovor = new System.Data.DataTable();
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(NazivProcedure.TijeloProcedure, conn);
                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Prepare();

                    if(parametri != null)
                        foreach(var kvp in parametri)
                        {
                            cmd.Parameters.Add(new MySqlParameter("@" + kvp.Key, kvp.Value));
                        }

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
                }
                catch { }
                return odgovor;
            }
        }

        public T IzvrsiProceduru<T>(SqlUpit SqlProcedure, Dictionary<string, object> parametri) where T: new()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataTable IzvrsiProceduru<T>(SqlUpit imeProcedure, T model)
        {
            var properties = model.GetType().GetProperties();
            Dictionary<string, object> parametri = new Dictionary<string, object>();
            foreach(var p in properties)
            {
                var p_vrijednost = p.GetValue(model);
                if(p_vrijednost != null)
                    parametri.Add(p.Name, p_vrijednost);
            }
            return IzvrsiProceduru(imeProcedure, parametri);
        }

        public List<T2> IzvrsiProceduru<T1, T2>(SqlUpit imeProcedure, T1 model) where T2: new()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet IzvrsiStoredProceduru(SqlUpit imeProcedure, Dictionary<string, object> parametri)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
