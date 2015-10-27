using System;

namespace OnliStam.Pomocnici
{
    /// <summary>
    /// Interfejs za pomoćnika baze podataka
    /// </summary>
    public interface IDbPomocnik
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="NazivProcedure"></param>
        /// <param name="parametri"></param>
        /// <returns></returns>
        System.Data.DataTable IzvrsiProceduru(string NazivProcedure, System.Collections.Generic.Dictionary<string, object> parametri);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="imeProcedure"></param>
        /// <param name="parametri"></param>
        /// <returns></returns>
        T IzvrsiProceduru<T>(string imeProcedure, System.Collections.Generic.Dictionary<string, object> parametri) where T: new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="imeProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        System.Data.DataTable IzvrsiProceduru<T>(string imeProcedure, T model);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="imeProcedure"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        System.Collections.Generic.List<T2> IzvrsiProceduru<T1, T2>(string imeProcedure, T1 model) where T2: new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imeProcedure"></param>
        /// <param name="parametri"></param>
        /// <returns></returns>
        System.Data.DataSet IzvrsiStoredProceduru(string imeProcedure, System.Collections.Generic.Dictionary<string, object> parametri);
    }
}
