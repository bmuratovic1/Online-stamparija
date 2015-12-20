using OnliStam.Pomocnici;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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

namespace OnliStam.Dashboard.View
{
    /// <summary>
    /// Interaction logic for SqlKveri.xaml
    /// </summary>
    public partial class SqlKveri: UserControl
    {
        public static RoutedCommand MyCommand = new RoutedCommand();

        public SqlKveri()
        {
            InitializeComponent();

            MyCommand.InputGestures.Add(new KeyGesture(Key.R, ModifierKeys.Control));
            txtKveriTekst.Focus();
        }

        private void MyCommandExecuted( object sender, ExecutedRoutedEventArgs e ) {
            Btn_Izvrsi_Click(sender, e);
        }


        public void Btn_Izvrsi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var upit = txtKveriTekst.SelectedText;
                if(string.IsNullOrEmpty(upit))
                    upit = txtKveriTekst.Text;
                var odgovor = new MySqlPomocnik().IzvrsiProceduru(new SqlUpit("", upit, new List<string>()), new Dictionary<string, object>());
                if(odgovor != null)
                {
                    dataGridOdgovor.ItemsSource = odgovor.AsDataView();
                }
            }
            catch(Exception ex)
            {
                dataGridOdgovor.ItemsSource = new[] { new {ErrorMessage = ex.Message }};
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
