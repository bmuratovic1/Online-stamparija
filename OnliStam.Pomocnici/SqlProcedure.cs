using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    public struct SqlUpit
    {
        #region --- Properties ---

        public string ImeProcedure { get; private set; }

        public string TijeloProcedure { get; private set; }

        public List<string> Parametri { get; set; }

        #endregion

        #region --- Constructors ---

        public SqlUpit(string imeProcedure, string tijeloProcedure, List<string> parametri) : this()
        {
            ImeProcedure = imeProcedure;
            TijeloProcedure = tijeloProcedure;
            Parametri = new List<string>(parametri.ToArray());
        }

        #endregion

        #region --- Methods ---

        #endregion

        #region --- Operators ---

        public static implicit operator string(SqlUpit proc)
        {
            return proc.ImeProcedure;
        }
        public static implicit operator SqlUpit(string imeProcedure)
        {
            return new SqlUpit(imeProcedure, string.Empty, new List<string>());
        }

        #endregion
    }
}
