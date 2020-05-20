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

Entity Framework versiota 8.0.19 (Install-Package MySql.Data.EntityFramework -Version 8.0.19), MySqlClient versiota 8.0.19 (Install-Package MySql.Data -Version 8.0.19 sekä MaterialDesignThemes -teemakirjastoa (Install-Package MaterialDesignThemes). Ohjelmasta on luotu asennusversio repositoryyn. 



## Tietoja ohjelmasta

### Toiminnalliset vaatimukset
|Vaatimus| Kuvaus  | Toteutettu| 
|:---|:----------|:---:|
|Valmennettavien CRUD | Valmentaja voi lisätä, poistaa, listata ja päivittää valmennettavia| Kyllä |  
|Treenien CRUD| Valmentaja voi lisätä, poistaa, listata ja päivittää treenejä valmennettaville | Kyllä|
|Kommenttien listaus treeneittäin| Valmentaja voi lukea valmennettavan kommentit treenistä | Ei |
|Treenin tilan näyttäminen| Valmentaja voi nähdä onko treeni suoritettu | Kyllä |
|Treenien listaus päivittäin ja henkilöittäin| Valmennettava voi listata eri päivien treenejä, merkitä tehdyksi ja kommentoida | Kyllä |
|Treenin arvostelu| Valmennettava voi arvostella päivän treenin | Kyllä |

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

## Käyttöohjeet

## Ohjelma tarvitsemat ja mukana tulevat resurssit
Ohjelma tarvitsee toimiakseen ulkopuolista tietokantaa. WodCoach -ohjelma käyttää omalle palvelimelle luotua tietokantaa (ip: 134.122.91.6), jonka käyttäjätunnus sekä salasana on salattuna ohjelman sisällä app.config -tiedostossa. 
Tietokannassa on valmiiksi harjoitustyön palautushetkellä dataa, eikä sitä tarvitse lisätä. Tietokannan [luontiskripti](../Scripts/WODCoach_Creation_Script.sql) ja [tietokantakaavio](../Scripts/WODCoach_DBModel.png)

## Tiedossa olevat ongelmat
Olion "Rate" ominaisuuksien yhdistäminen samaan datagridiin yhdessä olion "Wod" ominaisuuksien kanssa ei onnistunut huolimatta pitkästä yrittämisestä. On todennäköistä, että toiminto tulisi tehdä jollain muulla tapaa. 

## Jatkokehitys

## Opittua
Oppimista tuli paljon. Entity Frameworkin käyttö on sinänsä helppoa tietokannan suuntaan kunhan sen vain ensin opettelee. Lähes kaikki, mikä liittyi entity framework:iin ja tiedon hakemiseen tietokannasta, täytyi selvittää erikseen. 

App.configin "connectionStrings" osion salaaminen onnistui lopulta helposti suoraan Visual Stuodion kehittäjän komentokehotteesta, kunhan vain ohje löytyi. Samalla opin, kuinka app.config toimii sovellusta buildatessa. 

MVVM -mallin käyttökin selvisi pääpiirteittäin lopulta. Aluksi EF tuotti tässäkin päänvaivaa, mutta lopulta luulen ymmärtäneeni kuinka tiedostot kansioihin tulee sijoittaa ja minkä tulee tehdä mitäkin. 


## Tekijät

## Ehdotus arvosanaksi
