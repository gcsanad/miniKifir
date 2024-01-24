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


            /*
            string HibaUzenet = "";
            bool vanHiba = false;
            if(txtOMAzon.Text.Length != 11)
            {
                txtOMAzon.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem elég hosszú az OM azonosytó! \n";
                vanHiba = true;
            }

            if (!txtNev.Text.Contains(" "))
            {
                txtNev.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "A névnek legalább két tagból kell állnia! \n";
                vanHiba = true;
            }

            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.EndsWith(".com") || !txtEmail.Text.EndsWith(".hu"))
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Az email nem helye formátumban van! \n";
                vanHiba = true;
            }

            if (int.Parse(txtMagyar.Text) > 50 || int.Parse(txtMagyar.Text) < -1)
            {
                txtMagyar.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += int.Parse(txtMagyar.Text) > 50 ? "A magyar pontszám nem lehet több 50-nél! \n" : "A magyar pontszám nem lehet kevesebb 0-nál! \n";
                vanHiba = true;
            }

            if (int.Parse(txtMatek.Text) > 50 || int.Parse(txtMatek.Text) < -1)
            {
                txtMatek.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += int.Parse(txtMatek.Text) > 50 ? "A matek pontszám nem lehet több 50-nél! \n" : "A matek pontszám nem lehet kevesebb 0-nál! \n";
                vanHiba = true;
            }

            if(vanHiba)
            {
                MessageBox.Show(HibaUzenet);
            }
            else
            {
                //konstrukto hivas ize
            }
            */



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
             this.Close();
            }
        }
        private void CsakSzamokBeirasa(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]");
            e.Handled = regex.IsMatch(e.Text);
        }



        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            var masikAblak = (Application.Current.MainWindow as MainWindow);
            Diak modositandoDiak = masikAblak.diakok[masikAblak.valasztottIndex];
            modositandoDiak.OM_Azonosito = txtOMAzon.Text;
            modositandoDiak.Neve = txtNev.Text;
            modositandoDiak.ErtesitesiCime = txtCim.Text;
            modositandoDiak.Email = txtEmail.Text;
            modositandoDiak.SzuletesiDatum = Convert.ToDateTime(dpDatum.SelectedDate);
            modositandoDiak.Magyar = Convert.ToInt32(txtMagyar.Text);
            modositandoDiak.Matematika = Convert.ToInt32(txtMatek.Text);
            masikAblak.diakok[masikAblak.valasztottIndex] = modositandoDiak;
            masikAblak.dgFelvetelizok.Items.Refresh();
            this.Close();
        }

        private void BorderVissza(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.BorderBrush = new SolidColorBrush(Color.FromRgb(23, 23, 23));
        }
    }
}
