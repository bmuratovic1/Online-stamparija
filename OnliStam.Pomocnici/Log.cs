using System;

namespace OnliStam.Pomocnici
{
    public class Log
    {
        /// <summary>
        /// Sadržaj Log informacije
        /// </summary>
        public string Sadrzaj { get; set; }

        /// <summary>
        /// Datum nastanka Log informacije
        /// </summary>
        public DateTime Datum { get; set; }

        /// <summary>
        /// Tip log informacije
        /// <example>
        ///     0 - Debug
        ///     1 - Informacija
        ///     2 - Upozorenje
        ///     3 - Greška
        /// </example>
        /// </summary>
        public int Tip { get; set; }
    }
}