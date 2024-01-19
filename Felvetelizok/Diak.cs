using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felvetelizok
{

    public interface IFelvetelizo
    {

        String OM_Azonosito { get; set; }
        String Neve { get; set; }
        String ErtesitesiCime { get; set; }
        String Email { get; set; }
        DateTime SzuletesiDatum { get; set; }
        int Matematika { get; set; }
        int Magyar { get; set; }

        String CSVSortAdVissza();

        void ModositCSVSorral(String csvString);
    }


    class Diak : IFelvetelizo
    {
        string omAzonosito;
        string nev;
        string email;
        DateTime szuletesiDatum;

        string ertesitesiCim;
        int matekPontszam, magyarPontszam;

        public Diak(string sor)
        {
            string[] splitelt = sor.Split(';');
            OM_Azonosito = splitelt[0];
            Neve = splitelt[1];
            Email = splitelt[2];
            SzuletesiDatum = DateTime.Parse(splitelt[3]);
            ErtesitesiCime = splitelt[4];
            Matematika = splitelt[5] != "NULL" ? Int32.Parse(splitelt[5]) : -1;
            Magyar = splitelt[6] != "NULL" ? Int32.Parse(splitelt[6]) : -1;
        }

        public void ModositCSVSorral(String csvString)
        {
            string[] splitelt = csvString.Split(';');
            OM_Azonosito = splitelt[0];
            Neve = splitelt[1];
            Email = splitelt[2];
            SzuletesiDatum = DateTime.Parse(splitelt[3]);
            ErtesitesiCime = splitelt[4];
            Matematika = splitelt[5] != "NULL" ? Int32.Parse(splitelt[5]) : -1;
            Magyar = splitelt[6] != "NULL" ? Int32.Parse(splitelt[6]) : -1;
        }
      

        public string OM_Azonosito { get => omAzonosito; set => omAzonosito = value; }
        public string Neve { get => nev; set => nev = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szuletesiDatum; set => szuletesiDatum = value; }
        public string ErtesitesiCime { get => ertesitesiCim; set => ertesitesiCim = value; }
        public int Matematika { get => matekPontszam; set => matekPontszam = value; }
        public int Magyar { get => magyarPontszam; set => magyarPontszam = value; }

        public string CSVSortAdVissza()
        {
            return $"{OM_Azonosito};{Neve};{Email};{SzuletesiDatum.ToString("yyyy/MM/dd")};{ErtesitesiCime};{Matematika};{Magyar}";
        }

    }
}
