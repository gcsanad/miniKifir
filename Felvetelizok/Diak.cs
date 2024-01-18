using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felvetelizok
{
    class Diak
    {
        string omAzonosito;
        string nev;
        string email;
        string szuletesiDatum;
        string ertesitesiCim;
        int matekPontszam, magyarPontszam;

        public Diak(string sor)
        {
            string[] splitelt = sor.Split(';');
            omAzonosito = splitelt[0];
            nev = splitelt[1];
            email = splitelt[2];
            szuletesiDatum = splitelt[3];
            ertesitesiCim = splitelt[4];
            matekPontszam = splitelt[5] != "NULL" ? Int32.Parse(splitelt[5]) : -1;
            magyarPontszam = splitelt[6] != "NULL" ? Int32.Parse(splitelt[6]) : -1;
        }

        public string OMazonosito { get => omAzonosito; }
        public string Nev { get => nev; }
        public string Email { get => email; }
        public string SzuletesiDatum { get => szuletesiDatum; }
        public string ErtesitesiCim { get => ertesitesiCim; }
        public int MatekPontszam { get => matekPontszam; }
        public int MagyarPontszam { get => magyarPontszam; }

        public override string ToString()
        {
            return $"{omAzonosito};{nev};{email};{szuletesiDatum};{ertesitesiCim};{matekPontszam};{magyarPontszam}";
        }
    }
}
