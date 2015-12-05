using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici.Interfejsi
{
    /// <summary>
    /// Interfejs koji sadrži metode za uređivanje baze
    /// </summary>
    public interface IDbUrednik
    {
        string DodajTabelu(string imeBaze, string imeTabele);

        string PreimenujTabelu(string imeBaze, string staroIme, string novoIme);

        string IzbrisiTabelu(string imeBaze, string imeTabele);


        string DodajKolonu(string imeBaze, string imeTabele, string imeKolone, string tip, string kljuc, bool nulabilno, bool autoIncrement);

        string PreimenijKolonu(string imeBaze, string imeTabele, string staroIme, string novoIme, string tip, string kljuc, bool nulabilno, bool autoIncrement);

        string IzbrisiKolonu(string imeBaze, string imeTabele, string imeKolone);
    }
}
