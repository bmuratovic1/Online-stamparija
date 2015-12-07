using OnliStam.Pomocnici;
using OnliStam.Pomocnici.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for UCNovaTabela.xaml
    /// </summary>
    public partial class UCNovaTabela: UserControl
    {
        public UCNovaTabela()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var imeBaze = "onlinestamparija";
            IDbUrednik dbUrednik = new MySqlUrednik();
            var tabela = DataContext as Model.Table;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(dbUrednik.DodajTabelu(imeBaze, tabela.Ime));
            foreach(var col in tabela.Kolone)
            {
                stringBuilder.Append(dbUrednik.DodajKolonu(imeBaze, tabela.Ime, col.Ime, col.Tip, col.Key, col.Nullable, col.AutoIncrement).TrimEnd());
            }
            string upit = stringBuilder.ToString().Trim();
            upit = upit.TrimEnd(new char[]{','}).Trim();
            upit += ");";

            try
            {
                new MySqlPomocnik().IzvrsiProceduru(new SqlUpit("", upit, new List<string>()), new Dictionary<string, object>());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
        }
    }
}
