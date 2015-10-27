using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    public class Konstante
    {
        public static readonly string DAJ_KORISNIKA_ID = "DajKorisnika_ID";
        public static readonly string DAJ_KORISNIKA_EMAIL = "DajKorisnika_Email";
        public static readonly string DAJ_KORISNIKA_UNAME_PASS = "DajKorisnika";
        public static readonly string REGISTRUJ_KORISNIKA = "RegistrujKorisnika";
        public static readonly string DAJ_POSAO_ID = "DajPosao_ID";
        public static readonly string DODAJ_POSAO = "UnesiPosao";
        public static readonly string DAJ_ZAVRSENE_POSLOVE = "DajZavrsenePoslove";
        public static readonly string DAJ_NEZAVRSENE_POSLOVE = "DajNezavrsenePoslove";
        public static readonly string DODAJ_DTP = "DodajDTP";
        public static readonly string POTVRDI_REGISTRACIJU = "PotvrdiRegistraciju";
        public static readonly string PROMJENA_LOZINKE = "PromjeniLozinku";
        public static readonly string DAJ_LOGOVE = "DajLogove";
        public static readonly string DAJ_LOGOVE_TIP = "DajLogove_Tip";
        public static readonly string DAJ_LOG_ZADNjI_MINUTE = "DajLogZadnjiMinute";
        public static readonly string DODAJ_LOG = "DodajLog";
        public static readonly string ZAHTJEV_NOVA_LOZINKA = "ZahtjevNovaLozinka";
        public static readonly string PROMJENI_LOZINKU = "PromijeniLozinku";
        public static readonly string DODAJ_REPROMATERIJAL = "DodajRepromaterijal";
        public static readonly string DODAJ_VANJSKU_USLUGU = "DodajVanjskuUslugu";
        public static readonly string DODAJ_STAMPU = "DodajStampu";
        public static readonly string DODAJ_RUCNI_RAD = "DodajRucniRad";
        public static readonly string DODAJ_MONTAZU = "DodajMontazu";
        public static readonly string DODAJ_KNJIGOVODSTVENU_DORADU = "DodajKnjigovodstvenuDoradu";
        public static readonly string DAJ_PUTANjU_SLIKE = "DajSliku";
        public static readonly string DODAJ_SLIKU = "DodajSliku";
        public static readonly string DAJ_SVE_KORISNIKE = "DajSveKorisnike";
        public static readonly string BANUJ_KORISNIKA = "BanujKorisnika";
        public static readonly string ODBANUJ_KORISNIKA = "OdbanujKorisnika";
        public static readonly string UNAPRIJEDI_KORISNIKA = "UnaprijediKorisnika";
        public static readonly string NAZADUJ_KORISNIKA = "UnazadiKorisnika";
        public static readonly string DAJ_DTP = "DajDtp";
        public static readonly string DAJ_MONAZU = "DajMontazu";
        public static readonly string DAJ_STAMPU = "DajStampu";
        public static readonly string POTVRDI_POSAO = "PotvrdiPosao";
        public static readonly string DAJ_PODATKE_ZA_GRAF = "DajPodatkeZaGraf_1";

        public static readonly string REGISTRACIJA_TEMPLATE = @"
Dobro došli na našu stranicu i čestitamo na uspješnoj registraciji.
Da biste potvrdili registraciju, kliknite na link ispod:
http://www.nwt.somee.com/api/Account/PotvrdaRegistracijeJson/{0}?guid={1}

Ako niste Vi zahtjevali registaciju na našoj stranici, molimo Vas da ovu poruku zanemarite.
";
        public static readonly string PROMJENA_LOZINKE_TEMPLATE = @"
Poštovani,
Da biste promijenili vašu lozinku, potrebno je da kliknete na link ispod:

http://www.nwt.somee.com/Home/PromjenaLozinke/{0}?guid={1}

Ako niste Vi zahtjevali promjenu lozinke, molimo Vas da ovu poruku zanemarite.";
    }
}
