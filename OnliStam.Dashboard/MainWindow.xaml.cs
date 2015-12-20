using OnliStam.Dashboard.Model;
using OnliStam.Dashboard.View;
using OnliStam.Pomocnici;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnliStam.Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow: Window, INotifyPropertyChanged
    {
        public static RoutedCommand MyCommand = new RoutedCommand();

        /// <summary>
        /// 
        /// </summary>
        MainModel _glavniModel;

        public MainWindow()
        {
            InitializeComponent();

            _glavniModel = new MainModel("");
            this.DataContext = _glavniModel;

            foreach(var db in KonfiguracioniPomocnik.DajBaze())
                cmbSqlServer.Items.Add(db);

            MyCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Btn_SqlKveri_Click(sender, e);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _glavniModel.UcitajTabele();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] != null)
                _glavniModel.SelectedItem = e.AddedItems[0] as Model.Table;
        }

        private void cmbSqlServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var connStr = (e.AddedItems[0] as ConnectionString);
                MySqlPomocnik.CONNECTION_STRING = connStr.FullString;
                _glavniModel = new MainModel(connStr.Database);
                this.DataContext = _glavniModel;
                _glavniModel.UcitajTabele();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _glavniModel.DodavanjeNoveTabele = true;
            UcNovaTabela.DataContext = new OnliStam.Dashboard.Model.Table
            {
                Kolone = new System.Collections.ObjectModel.ObservableCollection<Kolona>(),
                Ime = "Neka tabela"
            }; // _glavniModel.SelectedItem;
        }


        private void Btn_SqlKveri_Click(object sender, RoutedEventArgs e)
        {
            new Window { Content = new SqlKveri() }.ShowDialog();
        }
    }
}
