using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnliStam.Pomocnici
{
    public struct Konstante
    {
        public class StoredProcedures
        {
            public static readonly SqlUpit DAJ_KORISNIKA_ID = new SqlUpit("DajKorisnika_ID", "", new List<string> { "@korisnikId" });

            public static readonly SqlUpit DAJ_KORISNIKA_EMAIL = "DajKorisnika_Email";

            public static readonly SqlUpit DAJ_KORISNIKA_UNAME_PASS = new SqlUpit("DajKorisnika",
                @"SELECT * 
	FROM korisnici
	WHERE
		Username = @Username
        AND Aktivan=1",
                                 new List<string> { "Username" });

            public static readonly SqlUpit REGISTRUJ_KORISNIKA = new SqlUpit("RegistrujKorisnika",
                @"INSERT INTO korisnici(
                    Username,
                    Password,
                    Email,
                    Pozicija,
                    Ime,
                    Prezime
                )VALUES(
                    @Username,
                    @Password,
                    @Email,
                    @Pozicija,
                    @Ime,
                    @Prezime
                )",
                new List<string> { "Username", "Password", "Email", "Pozicija", "Ime", "Prezime" });

            public static readonly SqlUpit DAJ_POSAO_ID = new SqlUpit("DajPosao_ID",
@"SELECT *
FROM poslovi
WHERE ID = @ID",
               new List<string> { "ID" });

            public static readonly SqlUpit DODAJ_POSAO = new SqlUpit("UnesiPosao",
@"INSERT INTO poslovi(
    Naziv,
    Opis,
    VrijemeTrajanja,
    VrstaMaterijala,
    KolicinaMaterijala,
    Status
)VALUES(
    @Naziv,
    @Opis,
    @VrijemeTrajanja,
    @VrstaMaterijala,
    @KolicinaMaterijala,
    @Status
);",
                new List<string> { "Naziv", "Opis", "VrijemeTrajanja", "VrstaMaterijala", "KolicinaMaterijala", "Status" });

            public static readonly SqlUpit DAJ_ZAVRSENE_POSLOVE = new SqlUpit("DajZavrsenePoslove",
@"SELECT *
FROM poslovi
WHERE Status = 1",
                  new List<string>());

            public static readonly SqlUpit DAJ_NEZAVRSENE_POSLOVE = new SqlUpit("DajNezavrsenePoslove",
@"SELECT *
FROM poslovi
WHERE Status <> 1",
                  new List<string>());

            public static readonly SqlUpit DODAJ_DTP = "DodajDTP";

            public static readonly SqlUpit POTVRDI_REGISTRACIJU = "PotvrdiRegistraciju";

            public static readonly SqlUpit PROMJENA_LOZINKE = "PromjeniLozinku";

            public static readonly SqlUpit DAJ_LOGOVE = new SqlUpit("DajLogove", "SELECT * FROM Logovi ORDER BY Datum DESC", new List<string>());

            public static readonly SqlUpit DAJ_LOGOVE_TIP =
                new SqlUpit("DajLogove_Tip",
@"SELECT * FROM Logovi
WHERE @Tip = @tip
ORDER BY Datum DESC",
new List<string> { "Tip" });

            public static readonly SqlUpit DAJ_LOG_ZADNjI_MINUTE = new SqlUpit("DajLogZadnjiMinute",
@"SELECT * FROM Logovi
WHERE date_add(Datum, INTERVAL @minute MINUTE) <= < NOW()
ORDER BY Datum DESC",
new List<string> { "Minute" });

            public static readonly SqlUpit DODAJ_LOG = new SqlUpit("DodajLog",
@"INSERT INTO Logovi(
	Sadrzaj,
	Tip,
	Datum
)
VALUES(
	@Sadrzaj,
	@Tip,
	@Datum
)",
                new List<string> {
                    "Sadrzaj",
                    "Tip",
                    "Datum"
                });

            public static readonly SqlUpit ZAHTJEV_NOVA_LOZINKA = "ZahtjevNovaLozinka";
            public static readonly SqlUpit PROMJENI_LOZINKU = "PromijeniLozinku";
            public static readonly SqlUpit DODAJ_REPROMATERIJAL = "DodajRepromaterijal";
            public static readonly SqlUpit DODAJ_VANJSKU_USLUGU = "DodajVanjskuUslugu";
            public static readonly SqlUpit DODAJ_STAMPU = "DodajStampu";
            public static readonly SqlUpit DODAJ_RUCNI_RAD = "DodajRucniRad";
            public static readonly SqlUpit DODAJ_MONTAZU = "DodajMontazu";
            public static readonly SqlUpit DODAJ_KNJIGOVODSTVENU_DORADU = "DodajKnjigovodstvenuDoradu";
            public static readonly SqlUpit DAJ_PUTANjU_SLIKE = "DajSliku";
            public static readonly SqlUpit DODAJ_SLIKU = "DodajSliku";
            public static readonly SqlUpit DAJ_SVE_KORISNIKE = new SqlUpit("DajSveKorisnike",
                "SELECT * FROM korisnici",
                new List<string> { });
            public static readonly SqlUpit BANUJ_KORISNIKA = new SqlUpit("BanujKorisnika",
                @"UPDATE korisnici
                SET Aktivan = 0
                WHERE ID = @ID",
                               new List<string> { "ID" });
            public static readonly SqlUpit ODBANUJ_KORISNIKA = new SqlUpit("OdbanujKorisnika",
                @"UPDATE korisnici
                SET Aktivan = 1
                WHERE ID = @ID",
                               new List<string> { "ID" });
            public static readonly SqlUpit UNAPRIJEDI_KORISNIKA = "UnaprijediKorisnika";
            public static readonly SqlUpit NAZADUJ_KORISNIKA = "UnazadiKorisnika";
            public static readonly SqlUpit DAJ_DTP = "DajDtp";
            public static readonly SqlUpit DAJ_MONAZU = "DajMontazu";
            public static readonly SqlUpit DAJ_STAMPU = "DajStampu";
            public static readonly SqlUpit POTVRDI_POSAO = "PotvrdiPosao";
            public static readonly SqlUpit DAJ_PODATKE_ZA_GRAF = "DajPodatkeZaGraf_1";
            public static readonly SqlUpit DAJ_MATERIJAL_ID = new SqlUpit("DajMaterijal_ID", 
                @"SELECT *
                FROM materijali
                WHERE ID = {ID};",
                                 new List<string>{"ID"});
            public static readonly SqlUpit DAJ_MATERIJALE = new SqlUpit("DajMaterijale",
                @"SELECT * FROM materijali", new List<string>());
            public static SqlUpit DODAJ_MATERIJAL = new SqlUpit("DodajMaterijale",
                @"INSERT INTO materijali(
                    Naziv,
                    Opis,
                    Kolicina
                ) VALUES (
                    @Naziv,
                    @Opis,
                    @Kolicina
                );", new List<string> { "Naziv", "Opis", "Kolicina" });
        }

        public struct EMailTemplates
        {
            public const string REGISTRACIJA_TEMPLATE = @"
Dobro došli na našu stranicu i čestitamo na uspješnoj registraciji.
Da biste potvrdili registraciju, kliknite na link ispod:
http://www.nwt.somee.com/api/Account/PotvrdaRegistracijeJson/{0}?guid={1}

Ako niste Vi zahtjevali registaciju na našoj stranici, molimo Vas da ovu poruku zanemarite.
";
            public const string PROMJENA_LOZINKE_TEMPLATE = @"
Poštovani,
Da biste promijenili vašu lozinku, potrebno je da kliknete na link ispod:

http://www.nwt.somee.com/Home/PromjenaLozinke/{0}?guid={1}

Ako niste Vi zahtjevali promjenu lozinke, molimo Vas da ovu poruku zanemarite.";
        }
    }
}