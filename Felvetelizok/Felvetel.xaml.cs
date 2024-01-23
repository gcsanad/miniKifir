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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Configuration;

namespace Felvetelizok
{
    /// <summary>
    /// Interaction logic for Felvetel.xaml
    /// </summary>
    public partial class Felvetel : Window
    {
        public Felvetel()
        {
            InitializeComponent();

        }

        private void btnFelvetel_Click(object sender, RoutedEventArgs e)
        {

            Diak ujDiak;

            if (Convert.ToInt32(txtMatek.Text) > 50 || Convert.ToInt32(txtMagyar.Text) > 50 || Convert.ToInt32(txtMatek.Text) < 0 || Convert.ToInt32(txtMagyar.Text) < 0)
            {
                MessageBox.Show("Helytelenül van megadva a Magyar vagy a Matematika eredmény!");
            }
            else if (txtOMAzon.Text.Length < 11 || txtOMAzon.Text.Length > 11)
            {
                MessageBox.Show("Helytelenül van megadva az OM Azonosító!\nEllenőrizze, hogy 11 karakteres e!");
            }
            else if (txtMatek.Text == "" || txtMagyar.Text == "")
            {
                ujDiak = new Diak($"{txtOMAzon.Text};{txtNev.Text};{txtEmail.Text};{dpDatum.SelectedDate};{txtCim.Text};-1;-1");
                (Application.Current.MainWindow as MainWindow).diakok.Add(ujDiak);
            }
            else
            {
            ujDiak = new Diak($"{txtOMAzon.Text};{txtNev.Text};{txtEmail.Text};{dpDatum.SelectedDate};{txtCim.Text};{txtMatek.Text};{txtMagyar.Text}");
            (Application.Current.MainWindow as MainWindow).diakok.Add(ujDiak);
            }
        }
        private void CsakSzamokBeirasa(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
