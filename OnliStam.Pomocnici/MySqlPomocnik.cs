using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                        foreach(var kvp in parametri.Where(x=>NazivProcedure.Parametri.Any(y => y.ToUpper() == x.Key.ToUpper())))
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
                catch(Exception ex) {
                    new Zapisnik(null).Zapisi(ex.ToString(), 3);
                }
                return odgovor;
            }
        }

        public T IzvrsiProceduru<T>(SqlUpit SqlProcedure, Dictionary<string, object> parametri) where T: new()
        {

            T odgovor = new T();
            var tabela = IzvrsiProceduru(SqlProcedure, parametri);
            if(tabela == null || tabela.HasErrors || tabela.Rows.Count == 0)
                return odgovor;
            var kolone = tabela.Rows[0];
            return RowToModel<T>( kolone);
        }

        private object GetDefault(Type type)
        {
            if(type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
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
            //var properties = model.GetType().GetProperties();
            List<T2> odgovor = new List<T2>();

            var parametri = ModelToDictionary<T1>(model);

            var tbl = IzvrsiProceduru(imeProcedure, parametri);

            foreach(System.Data.DataRow red in tbl.Rows)
            {
                T2 cvor = RowToModel<T2>(red);
                odgovor.Add(cvor);
            }

            return odgovor;
        }

        public System.Data.DataSet IzvrsiStoredProceduru(SqlUpit imeProcedure, Dictionary<string, object> parametri)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, object> ModelToDictionary<T>(T model)
        {
            var odgovor = new Dictionary<string, object>();
            if(model != null)
            {
                var modelProperties = model.GetType().GetProperties();
                foreach(var p in modelProperties)
                {
                    if(p != null)
                        odgovor.Add(p.Name, p.GetValue(model));
                }
            }
            return odgovor;
        }

        private T RowToModel<T>(System.Data.DataRow kolone) where T: new()
        {
            T odgovor = new T();
            var properties = typeof(T).GetProperties();
            foreach(var p in properties)
            {
                if(kolone.Table.Columns.Contains(p.Name))
                {
                    var k = kolone[p.Name];
                    Type p_type = p.PropertyType;

                    if(k != null && !Convert.IsDBNull(k))
                    {
                        try
                        {
                            p.SetValue(odgovor, Convert.ChangeType(k, p_type));
                        }
                        catch
                        {
                            var converter = TypeDescriptor.GetConverter(p_type);
                            object vrijednost;
                            if(converter.CanConvertFrom(k.GetType()))
                                vrijednost = converter.ConvertFrom(k.GetType());
                            else
                                vrijednost = GetDefault(p_type);
                            p.SetValue(odgovor, vrijednost);
                        }
                    }
                }
            }
            return odgovor;
        }

        #endregion
    }
}
