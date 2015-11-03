using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    /// <summary>
    /// Klasa koja sadrži podatke i metode za rad sa bazom podataka
    /// </summary>
    public class MsSqlPomocnik : OnliStam.Pomocnici.IDbPomocnik
    {

        #region ***** CONSTANTS ****

        // private readonly string CONNECTION_STRING = @"Data Source=.\SQLSERVER;Initial Catalog=NWT;Integrated Security=True";
        // private readonly string CONNECTION_STRING = @"Data Source=.\SQLEXPRESS;Initial Catalog=NWT;Integrated Security=True";
        // private readonly string CONNECTION_STRING = @"workstation id=nwtbaza.mssql.somee.com;packet size=4096;user id=HarisJavoras_SQLLogin_1;pwd=7dtdicwprj;data source=nwtbaza.mssql.somee.com;persist security info=False;initial catalog=nwtbaza";
        private readonly string CONNECTION_STRING = string.Empty;

        #endregion

        #region *** Fields ***

        #endregion

        #region *** Konstruktori ***

        public MsSqlPomocnik()
        {
            try
            {
                CONNECTION_STRING = ConfigurationManager.ConnectionStrings["MsSqlConnectionString"].ConnectionString;
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

        #region *** Metode ***

        /// <summary>
        /// Metoda za izvršavanje procedure na Bazi podataka
        /// </summary>
        /// <param name="NazivProcedure"></param>
        /// <param name="parametri"></param>   
        /// <returns></returns>
        public DataTable IzvrsiProceduru(SqlUpit NazivProcedure, Dictionary<string, object> parametri)
        {
            using(SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(NazivProcedure, conn);

                cmd.CommandType = CommandType.StoredProcedure;

                if(parametri != null)
                    foreach(KeyValuePair<string, object> parametar in parametri)
                    {
                        cmd.Parameters.Add(new SqlParameter("@" + parametar.Key, parametar.Value));
                    }

                DataTable odgovor = new DataTable();
                using(SqlDataReader rdr = cmd.ExecuteReader())
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

        /// <summary>
        /// Metoda proceduru na Bazi a kao parametre prosljeđuje vrijednosti propertija primljenog modela
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="imeProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable IzvrsiProceduru<T>(SqlUpit imeProcedure, T model)
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

        /// <summary>
        /// Metoda koja kreira objekat od rezultata izvršene procedure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="imeProcedure"></param>
        /// <param name="parametri"></param>
        /// <returns></returns>
        public T IzvrsiProceduru<T>(SqlUpit imeProcedure, Dictionary<string, object> parametri) where T: new()
        {
            T odgovor = new T();
            var tabela = IzvrsiProceduru(imeProcedure, parametri);
            if(tabela == null || tabela.HasErrors || tabela.Rows.Count == 0)
                return odgovor;
            var kolone = tabela.Rows[0];
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

        private object GetDefault(Type type)
        {
            if(type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="imeProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<T2> IzvrsiProceduru<T1, T2>(SqlUpit imeProcedure, T1 model) where T2: new()
        {
            //var properties = model.GetType().GetProperties();
            List<T2> odgovor = new List<T2>();

            using(SqlConnection conn = new SqlConnection(CONNECTION_STRING))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(imeProcedure, conn);
                //SqlCommand cmd2 = new SqlCommand(imeProcedure, conn);

                cmd.CommandType = CommandType.StoredProcedure;
                if(model != null)
                {
                    SqlCommandBuilder.DeriveParameters(cmd);
                    var modelProperties = model.GetType().GetProperties();
                    foreach(SqlParameter p in cmd.Parameters)
                    {
                        var properti = modelProperties.FirstOrDefault(y => y.Name == p.ParameterName.Substring(1));
                        if(properti != null)
                            cmd.Parameters[p.ParameterName].Value = properti.GetValue(model);
                    }
                }
                var odgovor_properties = typeof(T2).GetProperties();
                using(SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while(rdr.Read())
                    {
                        T2 cvor = new T2();
                        for(int i = 0; i < rdr.FieldCount; i++)
                        {
                            var ovajProperty = odgovor_properties.FirstOrDefault(x => x.Name == (rdr.GetName(i)));
                            var ovajValue = rdr.GetValue(i);
                            if(ovajProperty != null && ovajValue != null && ovajValue != DBNull.Value)
                            {
                                //ovajProperty.SetValue(cvor, Convert.ChangeType(ovajValue, ovajProperty.PropertyType));

                                try
                                {
                                    ovajProperty.SetValue(cvor, Convert.ChangeType(ovajValue, ovajProperty.PropertyType));
                                }
                                catch
                                {
                                    var converter = TypeDescriptor.GetConverter(ovajProperty.PropertyType);
                                    object vrijednost;
                                    if(converter.CanConvertFrom(ovajValue.GetType()))
                                        vrijednost = converter.ConvertFrom(ovajValue.GetType());
                                    else
                                        vrijednost = GetDefault(ovajProperty.PropertyType);
                                    ovajProperty.SetValue(cvor, vrijednost);
                                }

                                //parametri1.Add(rdr.GetValue(i));
                            }
                        }
                        odgovor.Add(cvor);
                    }
                }
                return odgovor;
            }
        }

        public DataSet IzvrsiStoredProceduru(SqlUpit imeProcedure, Dictionary<string, object> parametri)
        {
            using(System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(CONNECTION_STRING))
            {
                using(System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.CommandText = imeProcedure;
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    if(parametri != null)
                        foreach(KeyValuePair<string, object> parametar in parametri)
                        {
                            cmd.Parameters.Add(new SqlParameter("@" + parametar.Key, parametar.Value));
                        }

                    conn.Open();

                    System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);

                    DataSet ds = new DataSet();
                    adapter.Fill(ds);

                    conn.Close();
                    return ds;
                }
            }
        }

        #endregion

    }
}
