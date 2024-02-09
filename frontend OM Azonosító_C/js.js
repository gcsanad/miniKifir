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
const TABLAZAT = document.getElementById("tablazat")
const OMTABLAZAT = document.getElementById("OMtablazat")
let magyAtlag;
let matAtlag;
var inputText = document.getElementById("selOmAzonosito");

function Kereses() {
  OMTABLAZAT.innerHTML = ""
  OMTABLAZAT.innerHTML = `<tr>
                          <th id="elso_OM">OM</th>
                          <th id="elso_Nev">Név</th>
                          <th id="elso_Email">Email</th>
                          <th id="elso_Ertesitesi">Értesítési cím</th>
                          <th id="elso_Szuletesi">Születési dátum</th>
                          <th id="elso_Matek">Matek</th>
                          <th id="elso_Magyar">Magyar</th>
                          <th id="elso_Osszesen">Összesen</th>
                          </tr>`

  for (const diak of diakok) {
    if (diak.OM_Azonosito.startsWith(inputText.value) && inputText.value != "") {
      let ujSor = document.createElement("tr")
      ujSor.innerHTML = `<td>${diak.OM_Azonosito}</td>
      <td>${diak.Neve}</td>
      <td>${diak.Email}</td>
      <td>${diak.ErtesitesiCime}</td>
      <td>${diak.SzuletesiDatum.substring(0, 10)}</td>
      <td>${diak.Matematika}</td>
      <td>${diak.Magyar}</td>
      <td>${diak.Matematika + diak.Magyar}</td>`
      ujSor.addEventListener('click', (event) => {

        const TREK = document.querySelectorAll("tr");
        TREK.forEach(element => {
          element.classList = "";
          element.id = "";
        });
        event.target.parentElement.classList.add("kivalasztott")
        event.target.parentElement.id = "lekeres"

        let kivalasztottLekerese = document.getElementById("lekeres");

        if (TABLAZAT.children.length > 1) {
          while (TABLAZAT.children.length > 1) {
            TABLAZAT.removeChild(TABLAZAT.lastChild)
          }
          for (const diak of diakok) {
            if (diak.Magyar + diak.Matematika >= parseInt(kivalasztottLekerese.getElementsByTagName("td")[7]) && diak.OM_Azonosito != kivalasztottLekerese.getElementsByTagName("td")[0]) {
              let ujSor = document.createElement("tr")

              let ujOM = document.createElement("td")
              ujOM.innerText = diak.OM_Azonosito
              ujSor.append(ujOM)
              let ujNev = document.createElement("td")
              ujNev.innerText = diak.Neve
              ujSor.appendChild(ujNev)

              TABLAZAT.appendChild(ujSor)

            }
          }
        }
        else {
          for (const diak of diakok) {
            if (diak.Magyar + diak.Matematika >= parseInt(kivalasztottLekerese.getElementsByTagName("td")[7]) && diak.OM_Azonosito != kivalasztottLekerese.getElementsByTagName("td")[0]) {
              let ujSor = document.createElement("tr")

              let ujOM = document.createElement("td")
              ujOM.innerText = diak.OM_Azonosito
              ujSor.append(ujOM)
              let ujNev = document.createElement("td")
              ujNev.innerText = diak.Neve
              ujSor.appendChild(ujNev)

              TABLAZAT.appendChild(ujSor)

            }
          }
        }
      })
      OMTABLAZAT.appendChild(ujSor)






    }
  }

  //Több vagy egyenlő pontszámú táblázat

}

