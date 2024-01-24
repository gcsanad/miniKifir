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
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace Felvetelizok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<Diak> diakok = new ObservableCollection<Diak>();
        internal int valasztottIndex;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV fájl (*.csv)|*.csv| Szöveges fájl (*.txt) | *.txt";
            if (ofd.ShowDialog() == true)
            {
                if (diakok.Count == 0)
                {
                    foreach (string sor in File.ReadAllLines(ofd.FileName).Skip(1))
                    {
                        diakok.Add(new Diak(sor));
                    }
                }
                else
                {
                    var Result = MessageBox.Show("Már van importálva tábla! \n Lecserélni vagy hozzárendelni szeretnéd? \n (igen = csere | nem = hozzáad)", "Figyelem!", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {
                        diakok.Clear();
                        foreach (string sor in File.ReadAllLines(ofd.FileName).Skip(1))
                        {
                            diakok.Add(new Diak(sor));
                        }
                    }
                    else if (Result == MessageBoxResult.No)
                    {
                        foreach (string sor in File.ReadAllLines(ofd.FileName).Skip(1))
                        {
                            diakok.Add(new Diak(sor));
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            dgFelvetelizok.ItemsSource = diakok;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "csv";
            sfd.Filter = "CSV fájl (*.csv) | *.csv | Szöveges fájl (*.json) | *.json";
            sfd.Title = "Add meg a fájl nevét";

            if (sfd.ShowDialog() == true)
            {

                var ut = System.IO.Path.GetExtension(sfd.FileName);
                if (ut.ToLower() == ".json")
                {

                    var opciok = new JsonSerializerOptions();
                    opciok.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    opciok.WriteIndented = true;
                    string adatokSorai = JsonSerializer.Serialize(diakok, opciok);
                    var lista = new List<string>();
                    lista.Add(adatokSorai);
                    File.WriteAllLines(sfd.FileName, lista);
                }
                else
                {
                    File.WriteAllLines(sfd.FileName, diakok.Select(x => x.CSVSortAdVissza()));
                }



            }
            MessageBox.Show("sikeres mentés");
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedIndex > 0)
            {
                diakok.RemoveAt(dgFelvetelizok.SelectedIndex);
                MessageBox.Show("Sikeres törlés");
            }
            else
            {
                MessageBox.Show("Nincs kijelölve elem");
            }
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            Felvetel felvetel = new Felvetel();
            felvetel.Title = "Felvétel";
            felvetel.btnFelvetel.Visibility = Visibility.Visible;
            felvetel.ShowDialog();
            
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "yyyy/MM/dd";
        }

        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            Felvetel modosit = new Felvetel();
            Diak valasztottDiak = (Diak)dgFelvetelizok.SelectedItem;
            modosit.btnModosit.Visibility = Visibility.Visible;
            modosit.Title = "Adat módosítása";
            valasztottIndex = dgFelvetelizok.SelectedIndex;
            modosit.txtCim.Text = valasztottDiak.ErtesitesiCime;
            modosit.txtEmail.Text = valasztottDiak.Email;
            modosit.txtMagyar.Text = valasztottDiak.Magyar.ToString();
            modosit.txtMatek.Text = valasztottDiak.Matematika.ToString();
            modosit.txtNev.Text = valasztottDiak.Neve;
            modosit.txtOMAzon.Text = valasztottDiak.OM_Azonosito;
            modosit.txtOMAzon.IsEnabled = false;
            modosit.dpDatum.Text = valasztottDiak.SzuletesiDatum.ToString();
            modosit.ShowDialog();

        }
    }
}
