using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class ParametriAttribute: Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string[] _parametri;

        // This is a positional argument
        public ParametriAttribute(params string[] parametri)
        {
            this._parametri = parametri;

            // TODO: Implement code here
            throw new NotImplementedException();
        }

        public string[] Parametri
        {
            get { return _parametri; }
        }

        // This is a named argument
        public int NamedInt { get; set; }
    }
}
