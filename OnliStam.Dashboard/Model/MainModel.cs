using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnliStam.Pomocnici;
using System.Collections.ObjectModel;

namespace OnliStam.Dashboard.Model
{
    class MainModel: INotifyPropertyChanged
    {

        #region ***** CONSTANTS ****



        #endregion

        #region ****** FIELDS ******

        IDbPomocnik _dbPomocnik;

        Table _odabranaTabela;

        string _imeBaze;

        bool _dodavanjeNoveTabele;

        #endregion

        #region **** PROPERTIES ****

        /// <summary>
        /// Sve ucitane tabele
        /// </summary>
        public ObservableCollection<Table> Tables { get; set; }

        /// <summary>
        /// Indicira da li se dodaje nova tabela
        /// </summary>
        public bool DodavanjeNoveTabele
        {
            get { return _dodavanjeNoveTabele; }
            set
            {
                if(value != _dodavanjeNoveTabele)
                {
                    //if(value)
                        //_odabranaTabela = new Table { Kolone = new ObservableCollection<Kolona> { new Kolona { Ime = "Neka kolona" } } };
                    _dodavanjeNoveTabele = value;
                    OnPropertyChanged("DodavanjeNoveTabele");
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Trenutno odabrana tabela
        /// </summary>
        public Table SelectedItem
        {
            get { return _odabranaTabela; }
            set
            {
                if(_odabranaTabela != value)
                {
                    _odabranaTabela = value;
                    if(_odabranaTabela.Kolone == null)
                        _odabranaTabela.Kolone = new ObservableCollection<Kolona>();
                    if(!_odabranaTabela.Kolone.Any())
                    {
                        var odg = _dbPomocnik.IzvrsiProceduru(Konstante.SqlProcedure.DAJ_KOLONE, new Dictionary<string, object> { { "schemaName", _imeBaze }, { "tableName", _odabranaTabela.Ime } });

                        if(odg != null && odg.Rows.Count > 0)
                        {
                            for(int i = 0; i < odg.Rows.Count; i++)
                            {
                                _odabranaTabela.Kolone.Add(new Kolona
                                {
                                    Ime = odg.Rows[i]["COLUMN_NAME"].ToString(),
                                    Tip = odg.Rows[i]["DATA_TYPE"].ToString(),
                                    Nullable = odg.Rows[i]["IS_NULLABLE"].ToString() == "YES",
                                    Key = odg.Rows[i]["COLUMN_KEY"].ToString(),
                                    AutoIncrement = odg.Rows[i]["EXTRA"].ToString().ToUpper() == "AUTO_INCREMENT",
                                    Default = odg.Rows[i]["COLUMN_DEFAULT"].ToString()
                                });
                            }
                        }
                    }
                    OnPropertyChanged("SelectedItem");
                }
            }
        }

        #endregion

        #region *** CONSTRUCTORS ***

        public MainModel(string imeBaze)
        {
            _dbPomocnik = new MySqlPomocnik();
            _imeBaze = imeBaze;
            DodavanjeNoveTabele = false;
        }

        #endregion

        #region ****** METHODS *****

        public void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void UcitajTabele()
        {
            Tables = new ObservableCollection<Table>();

            var odg = _dbPomocnik.IzvrsiProceduru(Konstante.SqlProcedure.DAJ_SVE_TABELE, new Dictionary<string, object> { { "schemaName", _imeBaze } });

            if(odg != null && odg.Rows.Count > 0)
            {
                for(int i = 0; i < odg.Rows.Count; i++)
                {
                    Tables.Add(new Table { Ime = odg.Rows[i]["table_name"].ToString() });
                }
            }
            OnPropertyChanged("Tables");
        }

        #endregion
    }
}
