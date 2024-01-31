let diakok = [
    {
      "OM_Azonosito": "01234567890",
      "Neve": "Török Lilla",
      "Email": "torok.lilla@email.com",
      "SzuletesiDatum": "1997-06-14T00:00:00",
      "ErtesitesiCime": "Hódmezővásárhely, Kossuth Lajos tér 5.",
      "Matematika": 38,
      "Magyar": 50
    },
    {
      "OM_Azonosito": "12345678901",
      "Neve": "Szőke Balázs",
      "Email": "szoke.balazs@email.com",
      "SzuletesiDatum": "1998-01-29T00:00:00",
      "ErtesitesiCime": "Zalaegerszeg, Szabó utca 23.",
      "Matematika": 44,
      "Magyar": 50
    },
    {
      "OM_Azonosito": "23456789012",
      "Neve": "Balogh Attila",
      "Email": "balogh.attila@email.com",
      "SzuletesiDatum": "1992-04-08T00:00:00",
      "ErtesitesiCime": "Veszprém, Ady Endre utca 14.",
      "Matematika": 43,
      "Magyar": 48
    },
    {
      "OM_Azonosito": "34567890123",
      "Neve": "Király Eszter",
      "Email": "kiraly.eszter@email.com",
      "SzuletesiDatum": "1994-09-17T00:00:00",
      "ErtesitesiCime": "Székesfehérvár, Szabadság tér 7.",
      "Matematika": 50,
      "Magyar": -1
    },
    {
      "OM_Azonosito": "45678901234",
      "Neve": "Takács Ferenc",
      "Email": "takacs.ferenc@email.com",
      "SzuletesiDatum": "1996-12-25T00:00:00",
      "ErtesitesiCime": "Szolnok, Kossuth utca 10.",
      "Matematika": 37,
      "Magyar": 42
    },
    {
      "OM_Azonosito": "56789012345",
      "Neve": "Fehér Anita",
      "Email": "feher.anita@email.com",
      "SzuletesiDatum": "1990-02-03T00:00:00",
      "ErtesitesiCime": "Tatabánya, Bajnai út 18.",
      "Matematika": -1,
      "Magyar": 49
    },
    {
      "OM_Azonosito": "67890123456",
      "Neve": "Mészáros Tamás",
      "Email": "meszaros.tamas@email.com",
      "SzuletesiDatum": "1993-05-20T00:00:00",
      "ErtesitesiCime": "Salgótarján, Rákóczi út 25.",
      "Matematika": 50,
      "Magyar": 35
    },
    {
      "OM_Azonosito": "78901234567",
      "Neve": "Csonka Zoltán",
      "Email": "csonka.zoltan@email.com",
      "SzuletesiDatum": "1995-08-12T00:00:00",
      "ErtesitesiCime": "Esztergom, Dózsa György út 9.",
      "Matematika": 45,
      "Magyar": 50
    },
    {
      "OM_Azonosito": "89012345678",
      "Neve": "Lakatos Katalin",
      "Email": "lakatos.katalin@email.com",
      "SzuletesiDatum": "1988-11-27T00:00:00",
      "ErtesitesiCime": "Keszthely, Fő tér 12.",
      "Matematika": 49,
      "Magyar": 46
    },
    {
      "OM_Azonosito": "90123456789",
      "Neve": "Simon Gergő",
      "Email": "simon.gergo@email.com",
      "SzuletesiDatum": "2010-06-25T00:00:00",
      "ErtesitesiCime": "Szeksárd, Béla út 7.",
      "Matematika": 50,
      "Magyar": 40
    },
    {
      "OM_Azonosito": "43225435452",
      "Neve": "Molnár Noémi Judit",
      "Email": "molnar.noemi.judit@gmail.com",
      "SzuletesiDatum": "2005-10-07T00:00:00",
      "ErtesitesiCime": "Nagyvárad, József Attila utca 69.",
      "Matematika": 50,
      "Magyar": 50
    }
  ]

var comboBox = document.getElementById("selOmAzonosito");
for (const azon of diakok) {
    var opcio = document.createElement("option");
    opcio.value = azon.OM_Azonosito;
    opcio.text = azon.OM_Azonosito;
    comboBox.appendChild(opcio)
}
function Kereses(){
    let vanTalalat = false
    for (const diak of diakok) {
        if (diak.OM_Azonosito == comboBox.value) {
            
            document.getElementById("kiiratas").innerHTML = `<span>OM Azonosítója:</span> ${diak.OM_Azonosito}<br><span>Diák neve:</span> ${diak.Neve}<br><span>Email címe:</span> ${diak.Email}<br><span>Születési dátuma:</span> ${diak.SzuletesiDatum.substring(0,10)}<br><span>Értesítési címe:</span> ${diak.ErtesitesiCime}<br><span>Matematika pontszáma:</span> ${diak.Matematika}<br><span>Magyar pontszáma:</span> ${diak.Magyar}`
            console.log("Mukodik")
            vanTalalat = true
            break
        }
    }
    if (vanTalalat == false) {
        alert('Nincsen ilyen OM Azonosítóval rendelkező Diák!')
    }
    
}
