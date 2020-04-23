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
- **Valmentaja** voi lisätä, poistaa tai päivittää treenejä (WOD) valmennettaville.
- **Valmennettava** voi listata eri päivien treenejä, merkitä tehdyksi ja kommentoida päivän treeniä. Myös muut muuttujat ovat mahdollisia.  
- **Valmennettava** voi arvostella päivän treenin.
- **Valmentaja** voi lukea valmennettavan kommentit ja katsoa onko hän suorittanut treenin. 


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

```plantuml
entity Coach {
    * idCoach : int
    * fullName : string
    telNumber : string
    * created : string
    * updated : string
    --
    
}
entity Athlete {
    * idAthlete : int
    * fullName : string
    telNumber : string
    * created : string
    * updated : string
    --
}
entity WOD {
    * idWod : int
    * movementName : string
    repsCount : int
    roundCount : int
    level : float
    date : string
    done : bool
    comment : string
    * created : string
    * updated : string
    --
}
entity Rate {
    * athlete_id : int
    * wod_id : int
    rating : float
    comment : string
    * created : string
    * updated : string
    --
    CountAverage() : float
}

Athlete }o--o| Coach
WOD }o-left-o| Athlete
WOD ||--o{ Rating

```
Valmentaja (Coach) voi luoda useita valmennettavia (Athlete), joille valmentaja voi luoda kullekin useita päivän treenejä (WOD), mukaan lukien toisto ja kierrosmäärät. Valmennettava voi asettaa kommentteja ja haastavuustason kullekin treenille. Hän myös merkitsee onko tehnyt treenin. Lisäksi valmennettava voi antaa arvosteluita treeneille.


## Työaikasuunnitelma
|   vko    |Aika | Kum. aika |            Selite                 |
|:----------|:---:|:--------:|:-----------------------------------|
| 17 | 12 | 12   | Suunnitelma, tietokantaserveri, tietokannan suunnittelu ja luonti |
| 18 | 17 | 29 | Käyttöliittymän suunnittelu, pohja käyttöliittymälle, tietokantayhteys,  |
| 19 | 15 | 40   | Käyttöliittymän rakentamista, perustoiminnot käyttöliittymään |
| 20 | 15 | 55   | Käyttöliittymän ulkoasu, testaus, raportointia|
| 21 | 10 | 65   | Raportointia |


