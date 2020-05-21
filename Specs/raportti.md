# Harjoitustyön raportti

[Asennus](#asennus)
[Tietoja ohjelmasta](#tietoja-ohjelmasta)
[Kuvaruutukaappaukset](#kuvaruutukaappaukset)
[Käyttöohjeet](#käyttöohjeet)
[Ohjelma tarvitsemat ja mukana tulevat resurssit](#ohjelma-tarvitsemat-ja-mukana-tulevat-resurssit)
[Tiedossa olevat ongelmat](#tiedossa-olevat-ongelmat)
[Jatkokehitys](#jatkokehitys)
[Opittua](#opittua)
[Tekijät](#tekijät)
[Ehdotus arvosanaksi](#ehdotus-arvosanaksi)

## Asennus

Ohjelma käyttää Entity Framework versiota 8.0.19 (MySql.Data.EntityFramework -Version 8.0.19), MySqlClient versiota 8.0.19 (MySql.Data -Version 8.0.19) sekä MaterialDesignThemes -teemakirjastoa (MaterialDesignThemes). Ohjelmasta on luotu asennusversio repositoryyn, jonka asentamalla erillisiä pakettien asennuksia ei vaadita.



## Tietoja ohjelmasta

WodCoach -ohjelma on tarkoitettu käytettäväksi valmentajien ja urheilijoiden välisessä valmennussuhteessa. Ohjelmassa valmentaja voi luoda valmennettavalle päivittäisiä treenejä ja urheilija voi katsoa niitä tahollaan. 

Treenin tehtyään urheilija voi merkitä liikkeet tehdyiksi sekä arvostella ja kommentoida niitä. Valmentaja näkee urheilijan tekemät treenit. Urheilijoita ja liikkeitä voidaan ylläpitää ohjelmassa. 

### Toiminnalliset vaatimukset
|Vaatimus| Kuvaus  | Toteutettu| 
|:---|:----------|:---:|
|Urheilijoiden CRUD | Valmentaja voi lisätä, poistaa, listata ja päivittää urheilijoita| Kyllä |  
|Treenien CRUD| Valmentaja voi lisätä, poistaa, listata ja päivittää treenejä urheilijoille | Kyllä|
|Kommenttien listaus treeneittäin| Valmentaja voi lukea valmennettavan kommentit treenistä | Ei |
|Treenin tilan näyttäminen| Valmentaja voi nähdä onko treeni suoritettu | Kyllä |
|Treenien listaus päivittäin ja henkilöittäin| Urheilija voi listata eri päivien treenejä, merkitä tehdyksi ja kommentoida | Kyllä |
|Treenin arvostelu| Urheilija voi arvostella päivän treenin | Kyllä |

### Toiminnalliset vaatimukset / ylitetty
|Vaatimus| Kuvaus  | Toteutettu| 
|:---|:----------|:---:|
|Treenin lisäys | Valmentaja voi määrittää yksittäiselle liikkeelle vaikeustason| Kyllä |

### Ei-toiminnalliset vaatimukset
|Vaatimus| Kuvaus  | Toteutettu| 
|:---|:----------|:---:|
|Poikkeusten käsittely | Poikkeukset ja virheet tulee olla käsitelty, jotta ohjelma ei kaadu| Kyllä |  
|SQL-injektio estetty | SQL-injektio täytyy olla estetty| Kyllä, Entity Framework | 
|Salasanojen kryptaus | Salasanojen tulee olla kryptattu| Kyllä |
|Lähdekoodin kommentointi | Lähdekoodin tulee olla kommentoitu| Kyllä |  


## Kuvaruutukaappaukset
![](../Images/Landing_page_e.PNG)  
![](../Images/Menu_e.PNG)  
![](../Images/Coach_e.PNG)  
![](../Images/Athlete_e.PNG)  
![](../Images/Movements_e.PNG)  
![](../Images/Admin_e.PNG)  

## Käyttöohjeet



## Ohjelma tarvitsemat ja mukana tulevat resurssit
Ohjelma tarvitsee toimiakseen ulkopuolista tietokantaa. WodCoach -ohjelma käyttää omalle palvelimelle luotua tietokantaa (ip: 134.122.91.6), jonka käyttäjätunnus sekä salasana on salattuna ohjelman sisällä app.config -tiedostossa. 
Tietokannassa on valmiiksi harjoitustyön palautushetkellä dataa, eikä sitä tarvitse lisätä. Tietokannan [luontiskripti](../Scripts/WODCoach_Creation_Script.sql). 

### Tietokantakaavio
![tietokantakaavio](../Scripts/WODCoach_DBModel.png)

## Tiedossa olevat ongelmat
**Coach -sivulla ei voi nähdä urheilijan antamaa kommenttia tai arvosanaa treenille.**
* Olion "Rate" ominaisuuksien yhdistäminen samaan datagridiin yhdessä olion "Wod" ominaisuuksien kanssa ei onnistunut huolimatta pitkästä yrittämisestä. On todennäköistä, että toiminto tulisi tehdä jollain muulla tapaa. Tämän vuoksi toiminnallisuus, jossa valmentaja voi lukea urheilijoiden kommentit treenien yhteydestä ei toteutunut.  

**Sivun vaihtumisessa on viivettä.** 
* Tuntemattomasta syystä sivujen latautumisessa on viivettä. Todennäköisesti viive liittyy tietokantahakuun. 

**Datagridin valinnan poistaminen**
* Kun datagridiltä on valinnut objektin, valintaa ei saa pois muutoin kuin esimerkiksi tallentamalla saman objektin uudelleen. Tämä hoituisi uudella napilla. 

## Jatkokehitys
**Käyttäjäkohtainen näkymä** puuttuu tällä hetkellä. Alkuun tulisi luoda sisäänkirjautuminen, jonka jälkeen valmennettava voisi nähdä ainoastaan omat treeninsä, sama voisi päteä myös valmentajaan.  



## Haasteet ja opittua
Oppimista tuli paljon **erityisesti Entity Frameworkin käytön osalta**. EF:llä on sinänsä helppoa toimia tietokannan suuntaan kunhan sen vain ensin opettelee. Harjoitustyössä kuitenkin lähes kaikki, mikä liittyi entity framework:iin ja tiedon hakemiseen tietokannasta, täytyi selvittää erikseen.  

**App.configin "connectionStrings" osion salaaminen** onnistui lopulta helposti suoraan Visual Stuodion kehittäjän komentokehotteesta, vaikka jälleen ohjeen etsimisessä kuluikin aikaa. Samalla opin, kuinka app.config toimii sovellusta buildatessa.  

**MVVM -mallin käyttökin** selvisi pääpiirteittäin lopulta. Aluksi EF tuotti tässäkin päänvaivaa, mutta lopulta luulen ymmärtäneeni kuinka tiedostot kansioihin tulee sijoittaa ja minkä tulee tehdä mitäkin.  

Harjoitustyössä oli tarkoitus harjoitella myös **Material Design -teemakirjaston käyttöä**. Tässä onnistuttiinkin ja teemakirjastoa käytettiin joissain ohjelman komponenteissa. Laajempi käyttö vaatii kuitenkin vielä harjoittelua ja ennen kaikkea teemakirjaston tuntemusta.  

**Sidonnan** käyttö tuli tutuksi ja opin muun muassa kuinka sidonta tehdään useammastakin luokasta.

### Haasteita

Suurin yksittäinen haaste oli yrittää saada **kahden tietokantataulun tietoja yhteen datagridiin**, eikä siinä onnistuttukaan.

Toinen haaste oli kokonaisuuden kannalta **Entity Framework, sen soveltaminen ja sovittaminen MVVM-malliin**. Lopulta soveltaminen onnistui melko hyvin ja sovittaminen MVVM-malliinkin mielestäni pääosin.


## Ehdotus arvosanaksi

Oman käsitykseni mukaan harjoitustyössä käytettiin laajasti kurssilla käsiteltyjä aiheita. Käyttöliittymä on toimiva, joskin kyseinen ohjelma olisi parempi web-pohjaisessa käyttöliittymässä. Valinta oli kuitenkin tietoinen, koska tarkoitus oli jalostaa ideaa ja tehdä myöhemmin vastaaava web-ympäristössä. 

Ohjelmiston tueksi luotiin oma tietokantapalvelin ja erityisesti entity framwork:iä tutkittiin laajasti. Samalla tutustuttiin myös ulkoiseen MaterialDesign -teemakirjastoon. 

Ulkoasun suunnitteluakin tehtiin, tosin myöhemmin suunnitelmaa jouduttiin korjaamaan ja samalla tietenkin tuli oppia. Ulkoasun lopputulos on siedettävä, mutta parantamisen varaa on. 

Kurssin aihealueen ja opittujen asioiden laajuuden sekä harjoitustyöhön käytetyn ajan huomioiden ehdotan arvosanaksi 4,5.


