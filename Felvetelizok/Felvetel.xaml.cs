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
        Diak ujDiak;
        Diak modositandoDiak;
        public Felvetel()
        {
            InitializeComponent();

        }

        public Felvetel(Diak diak) : this()
        {
            ujDiak = diak;
        }

        public Felvetel(Diak diak, bool modosit) : this()
        {
            modositandoDiak = diak;
        }

        private void btnFelvetel_Click(object sender, RoutedEventArgs e)
        {
            string HibaUzenet = "";
            bool vanHiba = false;

            //OM hiba ellenorzes
            if (txtOMAzon.Text == "")
            {
                txtOMAzon.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem adott meg OM azonosítót! \n";
                vanHiba = true;
            }
            else if (txtOMAzon.Text.Length != 11)
            {
                txtOMAzon.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem elég hosszú az OM azonosítót! \n";
                vanHiba = true;
            }


            //nev hiba ellenorzes
            if (txtNev.Text == "")
            {
                txtNev.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem adott meg Nevet! \n";
                vanHiba = true;
            }
            else if (!txtNev.Text.Contains(" "))
            {
                txtNev.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "A névnek legalább két tagból kell állnia! \n";
                vanHiba = true;
            }


            //cim hiba ellenorzes
            if (txtCim.Text == "")
            {
                txtCim.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem adott meg elérési címet! \n";
                vanHiba = true;
            }


            //datum hiba ellenorzes
            if (dpDatum.Text == "")
            {
                dpDatum.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem adott meg Születési dátumot! \n";
                vanHiba = true;
            }


            //email hiba ellenorzes
            if (txtEmail.Text == "")
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Nem adott meg Email címet! \n";
                vanHiba = true;
            }
            else if (txtEmail.Text.Contains(" "))
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Az email címben nem lehet szóköz! \n";
                vanHiba = true;
            }
            else if (!txtEmail.Text.Contains("@"))
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += "Az email címből hiányzik a \"@\" -jel! \n";
                vanHiba = true;
            }
            else
            {
                int hanyvanbenne = 0;
                foreach (char betu in txtEmail.Text) { 
                    if(betu == '@') { hanyvanbenne++; }
                }
                if(hanyvanbenne > 1)
                {
                    txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                    HibaUzenet += "Az email címben csak 1 db \"@\" -jel lehet! \n";
                    vanHiba = true;
                }
            }


            //magyar hiba ellenorzes
            if (int.Parse(txtMagyar.Text) > 50 || int.Parse(txtMagyar.Text) < -1)
            {
                txtMagyar.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += int.Parse(txtMagyar.Text) > 50 ? "A magyar pontszám nem lehet több 50-nél! \n" : "A magyar pontszám nem lehet kevesebb 0-nál! \n";
                vanHiba = true;
            }


            //matek hiba ellenorzes
            if (int.Parse(txtMatek.Text) > 50 || int.Parse(txtMatek.Text) < -1)
            {
                txtMatek.BorderBrush = new SolidColorBrush(Colors.Red);
                HibaUzenet += int.Parse(txtMatek.Text) > 50 ? "A matek pontszám nem lehet több 50-nél! \n" : "A matek pontszám nem lehet kevesebb 0-nál! \n";
                vanHiba = true;
            }

            if(vanHiba)
            {
                MessageBox.Show(HibaUzenet, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ujDiak.OM_Azonosito = txtOMAzon.Text;
                ujDiak.Neve = txtNev.Text;
                ujDiak.Email = txtEmail.Text;
                ujDiak.SzuletesiDatum = DateTime.Parse(dpDatum.Text);
                ujDiak.ErtesitesiCime = txtCim.Text;
                ujDiak.Matematika = int.Parse(txtMatek.Text);
                ujDiak.Magyar = int.Parse(txtMagyar.Text);

                Close();
            }
        }



        private void CsakSzamokBeirasa(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9+]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            string ModositasiHibaUzenet = "";
            bool vanModositasiHiba = false;

            //OM hiba ellenorzes
            if (txtOMAzon.Text == "")
            {
                txtOMAzon.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Nem adott meg OM azonosítót! \n";
                vanModositasiHiba = true;
            }
            else if (txtOMAzon.Text.Length != 11)
            {
                txtOMAzon.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Nem elég hosszú az OM azonosítót! \n";
                vanModositasiHiba = true;
            }


            //nev hiba ellenorzes
            if (txtNev.Text == "")
            {
                txtNev.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Nem adott meg Nevet! \n";
                vanModositasiHiba = true;
            }
            else if (!txtNev.Text.Contains(" "))
            {
                txtNev.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "A névnek legalább két tagból kell állnia! \n";
                vanModositasiHiba = true;
            }


            //cim hiba ellenorzes
            if (txtCim.Text == "")
            {
                txtCim.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Nem adott meg elérési címet! \n";
                vanModositasiHiba = true;
            }


            //datum hiba ellenorzes
            if (dpDatum.Text == "")
            {
                dpDatum.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Nem adott meg Születési dátumot! \n";
                vanModositasiHiba = true;
            }


            //email hiba ellenorzes
            if (txtEmail.Text == "")
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Nem adott meg Email címet! \n";
                vanModositasiHiba = true;
            }
            else if (txtEmail.Text.Contains(" "))
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Az email címben nem lehet szóköz! \n";
                vanModositasiHiba = true;
            }
            else if (!txtEmail.Text.Contains("@"))
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += "Az email címből hiányzik a \"@\" -jel! \n";
                vanModositasiHiba = true;
            }
            else
            {
                int hanyvanbenne = 0;
                foreach (char betu in txtEmail.Text)
                {
                    if (betu == '@') { hanyvanbenne++; }
                }
                if (hanyvanbenne > 1)
                {
                    txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                    ModositasiHibaUzenet += "Az email címben csak 1 db \"@\" -jel lehet! \n";
                    vanModositasiHiba = true;
                }
            }


            //magyar hiba ellenorzes
            if (int.Parse(txtMagyar.Text) > 50 || int.Parse(txtMagyar.Text) < -1)
            {
                txtMagyar.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += int.Parse(txtMagyar.Text) > 50 ? "A magyar pontszám nem lehet több 50-nél! \n" : "A magyar pontszám nem lehet kevesebb 0-nál! \n";
                vanModositasiHiba = true;
            }


            //matek hiba ellenorzes
            if (int.Parse(txtMatek.Text) > 50 || int.Parse(txtMatek.Text) < -1)
            {
                txtMatek.BorderBrush = new SolidColorBrush(Colors.Red);
                ModositasiHibaUzenet += int.Parse(txtMatek.Text) > 50 ? "A matek pontszám nem lehet több 50-nél! \n" : "A matek pontszám nem lehet kevesebb 0-nál! \n";
                vanModositasiHiba = true;
            }

            if (vanModositasiHiba)
            {
                MessageBox.Show(ModositasiHibaUzenet, "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                modositandoDiak.OM_Azonosito = txtOMAzon.Text;
                modositandoDiak.Neve = txtNev.Text;
                modositandoDiak.Email = txtEmail.Text;
                modositandoDiak.SzuletesiDatum = DateTime.Parse(dpDatum.Text);
                modositandoDiak.ErtesitesiCime = txtCim.Text;
                modositandoDiak.Matematika = int.Parse(txtMatek.Text);
                modositandoDiak.Magyar = int.Parse(txtMagyar.Text);

                Close();
            }
        }

        private void BorderVissza(object sender, RoutedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.BorderBrush = new SolidColorBrush(Color.FromRgb(23, 23, 23));
        }
    }
}
