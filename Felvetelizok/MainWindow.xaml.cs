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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Felvetelizok
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<Diak> diakok = new ObservableCollection<Diak>();
        internal int valasztottIndex;
        private readonly string sqlKapcsolat = "datasource=127.0.0.1;port=3306;username=root;password=;database=minikozfelvir;";
        private MySqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "CSV fájl (*.csv)|*.csv| JSON fájl (*.json) | *.json";
            if (ofd.ShowDialog() == true)
            {
                var ut = System.IO.Path.GetExtension(ofd.FileName);
                if (diakok.Count == 0)
                {
                    jsonVagyCsv(ut, ofd.FileName);
                }
                else
                {
                    var Result = MessageBox.Show("Már van importálva tábla! \n Lecserélni vagy hozzárendelni szeretnéd? \n (igen = csere | nem = hozzáad)", "Figyelem!", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

                    if (Result == MessageBoxResult.Yes)
                    {
                        diakok.Clear();
                        jsonVagyCsv(ut, ofd.FileName);
                    }
                    else if (Result == MessageBoxResult.No)
                    {
                        jsonVagyCsv(ut, ofd.FileName);
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
                    MessageBox.Show("Sikeres mentés!");
                }
                else
                {
                    File.WriteAllLines(sfd.FileName, diakok.Select(x => x.CSVSortAdVissza()));
                    MessageBox.Show("Sikeres mentés!");
                }



            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedIndex > 0)
            {
                
                while (dgFelvetelizok.SelectedItems.Count != 0)
                {
                diakok.RemoveAt(dgFelvetelizok.SelectedIndex);
                }
                MessageBox.Show("Sikeres törlés");
                dgFelvetelizok.Items.Refresh();
            }
            else
            {
                MessageBox.Show("Nincs kijelölve elem");
            }
            
            
        }

        private void btnFelvesz_Click(object sender, RoutedEventArgs e)
        {
            Diak ujDiak = new Diak();
            Felvetel felvetel = new Felvetel(ujDiak);

            felvetel.Title = "Felvétel";
            felvetel.btnFelvetel.Visibility = Visibility.Visible;
            felvetel.ShowDialog();

            diakok.Add(ujDiak);      
        }

        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            if (dgFelvetelizok.SelectedItems.Count > 1)
            {
                MessageBox.Show("Csak egy diák legyen kiválasztva a táblázatban!");
            }
            else
            {
                Diak valasztottDiak = (Diak)dgFelvetelizok.SelectedItem;
                valasztottIndex = dgFelvetelizok.SelectedIndex;

                Felvetel modosit = new Felvetel(valasztottDiak, true);


                modosit.btnModosit.Visibility = Visibility.Visible;
                modosit.Title = "Adat módosítása";
            
                modosit.txtCim.Text = valasztottDiak.ErtesitesiCime;
                modosit.txtEmail.Text = valasztottDiak.Email;
                modosit.txtMagyar.Text = valasztottDiak.Magyar.ToString();
                modosit.txtMatek.Text = valasztottDiak.Matematika.ToString();
                modosit.txtNev.Text = valasztottDiak.Neve;
                modosit.txtOMAzon.Text = valasztottDiak.OM_Azonosito;
                modosit.txtOMAzon.IsEnabled = false;
                modosit.dpDatum.Text = valasztottDiak.SzuletesiDatum.ToString();
                modosit.ShowDialog();

                dgFelvetelizok.Items.Refresh();

            }

        }

        private void dgFelvetelizok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFelvetelizok.SelectedIndex != -1)
            {
                btnModosit.IsEnabled = true;
                btnTorles.IsEnabled = true;
            }
        }

        private void jsonVagyCsv(string ut, string fajlNeve)
        {
            if (ut.ToLower() == ".csv")
            {

                foreach (string sor in File.ReadAllLines(fajlNeve).Skip(1))
                {
                    diakok.Add(new Diak(sor));
                }
            }
            else if (ut.ToLower() == ".json")
            {

                string beolvas = File.ReadAllText(fajlNeve);

                var lista = JsonSerializer.Deserialize<List<Diak>>(beolvas);
                foreach (var item in lista)
                {
                    diakok.Add(item);
                }
            }
        }

        private void btnSqlImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dgFelvetelizok.ItemsSource = "";
                connection = new MySqlConnection(sqlKapcsolat);
                connection.Open();

                string lekerdezesString = "SELECT OM_azonosito,Nev,Email,Szuletesi_datum,Ertesitesi_cim,matek_eredmeny,magyar_eredmeny FROM felvetelizok";
                MySqlCommand lekerdezes = new MySqlCommand(lekerdezesString, connection);
                lekerdezes.CommandTimeout = 60;
                MySqlDataReader reader = lekerdezes.ExecuteReader();
                if (reader.HasRows)
                {
                    
                    while (reader.Read())
                    {
                        diakok.Add(new Diak(reader));
                    }
                    dgFelvetelizok.ItemsSource = diakok;
                }
                else
                {
                    MessageBox.Show("Nincs sor az SQL-ben!");
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void btnSqlExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                connection = new MySqlConnection(sqlKapcsolat);
                connection.Open();
                string torlesString = $"DELETE FROM felvetelizok";
                MySqlCommand torlesLekerdezes = new MySqlCommand(torlesString, connection);
                torlesLekerdezes.ExecuteNonQuery();


                foreach (var diak in diakok.ToList())
                {
                    string lekerdezesString = $"INSERT INTO felvetelizok (OM_azonosito,Nev,Email,Szuletesi_datum,Ertesitesi_cim,matek_eredmeny,magyar_eredmeny) VALUES ('{diak.OM_Azonosito}', '{diak.Neve}', '{diak.Email}', '{diak.SzuletesiDatum.Year}-{diak.SzuletesiDatum.Month}-{diak.SzuletesiDatum.Day}', '{diak.ErtesitesiCime}', '{diak.Matematika}', '{diak.Magyar}')";
                    MySqlCommand lekerdezes = new MySqlCommand(lekerdezesString, connection);
                    lekerdezes.ExecuteNonQuery();
                }
                MessageBox.Show("Működik!");
            }



            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                throw;
            }
        }
    }
}
