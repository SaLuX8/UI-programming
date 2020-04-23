# Specs 

### Tietoa tekijästä
- Nimi: Sami Luoma-aho
- Jyväskylän ammattikorkeakoulu
- Ohjelmistotekniikan muuntokoulutusopiskelija
  
### Sovelluksen yleiskuvaus
Tehdään oma ohjelmisto valmentajalle ja valmennettavalle etävalmennuksen avuksi. Ohjelmalla valmentaja voi antaa valmennettaville treenejä suoritettavaksi. Valmennettava kuittaa tehdyksi ja voi kommentoida.

### Kenelle sovellus on suunnattu, kohdeyleisö
Suunnattu henkilökohtaisille valmentajille ja heidän valmennettavilleen. 

### Käyttöympäristö ja käytetyt teknologiat
Ohjelma toteutetaan C# -kielellä käyttäen WPF-kirjastoa. Tietokanta luodaan MySQL:llä ulkoiseen tietokantaan. Mahdollisuuksien mukaan käytetään apuna ADO.NET Entity Framework:iä.  
Käyttöliittymän ulkoasun muokkaamiseen käytetään apuna XAML Material Design -teemakirjastoa.  

### Tunnistetaan eri käyttäjäroolit, jos niitä on

- Käyttäjärooleja on tässä vaiheessa kaksi, **valmentaja** ja **valmennettava**. 
- **Valmentaja** voi lisätä, poistaa, listata ja päivittää valmennettavia.
- **Valmentaja** voi lisätä, poistaa tai päivittää treenejä valmennettaville.
- **Valmennettava** voi listata eri päivien treenejä, merkitä tehdyksi ja kommentoida päivän treeniä. Myös muut muuttujat ovat mahdollisia.  
- **Valmentaja** voi lukea valmennettavan kommentit ja katsoa onko hän suorittanut treenin. 

- Tässä vaiheessa ei selvyyden vuoksi todennäköisesti toteuteta useamman valmentajan ohjelmistoa.



```plantuml

actor Coach 
actor Athlete
usecase (Add, delete, modify\ntraining programs) as UC1
usecase (List daily programs,\nmark as done, comment) as UC2
usecase (List what\nathlete has done\n+comments) as UC3
usecase (Add, delete, modify, list\nalthletes) as UC4
usecase (Optional:\nAdd link to videos) as UC5
usecase (Optional:\nAdd own feelings,\nbodyweight etc.) as UC6

Coach -left-> UC4
Coach -down-> UC1
Coach -down-> UC3
Coach -down--> UC5

Athlete -down-> UC2
Athlete -down-> UC6

```
- Toiminnoista piiretään UML:n Käyttötapaus-kaavio, kaaviossa esitetään eri roolit ja käyttötapaukset=toiminnot
- Sovelluksen keskeiset käsitteet listataan ja luodaan Käsitemalli, jossa esitetään käsitteet ja niiden väliset suhteet; tästä jalostetaan sitten luokkamalli sovelluksesta, mitä luokkia sovelluksessa on ja niiden tärkeimmät tehtävät ja ominaisuudet sekä luokkien väliset suhteet UML:tä käyttäen. Suunnitelmassa alustava ajatus, loppuraportissa lopullinen rakenne ja perustelut muutoksille PS Muista kertoa ajatuksista, pelkkä kaaviokuvio ei ole riittävä.

- Työnjako, kuinka työnjako aiotaan hoitaa eli vastuut eri tehtäville

## Työaikasuunnitelma
|   vko    |Aika | Kum. aika |            Selite                 |
|:----------|:---:|:--------:|:-----------------------------------|
| 17 | 12 | 12   | Suunnitelma, tietokantaserveri, tietokannan suunnittelu ja luonti |
| 18 | 17 | 29 | Käyttöliittymän suunnittelu, pohja käyttöliittymälle, tietokantayhteys,  |
| 19 | 15 | 40   | Käyttöliittymän rakentamista, perustoiminnot käyttöliittymään |
| 20 | 15 | 55   | Käyttöliittymän ulkoasu, testaus, raportointia|
| 21 | 10 | 65   | Raportointia |


