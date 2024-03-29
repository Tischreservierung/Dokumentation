# Systembeschreibung - Tischreservierung
Autor: Sebastian Witzeneder  
Version: 1.1

## Inhaltsverzeichnis
- [Technologien](#technologien)
- [Deployment](#deployment)
- [ERD](#erd)
- [Funktionalität](#funktionalität)
- [Wichtigsten Funktionen](#wichtigsten-funktionen)

## Technologien

![](Technologie-Diagram.png)

### Angular
Angular wird als Frontend verwendet. Dabei greift es auf die REST-Endpoints zu und ist für die gesamte Nutzerinteraktion zuständig.

### ASP.NET Core
Hier sind die REST-Endpoints implementiert, bei denen Daten aus der Datenbank geliefert werden oder neue angelegt werden.

### Leocloud
Hier befindet sich unser Projekt, welches automatisch bei einem Merge auf dem Main-Branch aktualisiert wird.   
Dabei gibt es 2 Punkte:  
* Unser Angular-Projekt (https://student.cloud.htl-leonding.ac.at/s.raaber/onlinereservation/)
* Und das Backend mit RestEndpoints hierbei ist die Datenbank inkludiert

```mermaid
C4Context
    Container_Boundary(c1, "Leocloud") {
        Container(angular, "Frontend", "Angular", "Nutzerinteraktion")
        Container(api, "API", "ASP.NET", "REST-Endpoints")
        Container(database, "Database", "SQL-Server", "Speichert alle Nutzer, Restaurants, Reservierungen, ...")
    }
    Rel(angular,api, "Anfragen")
    Rel(api,database,"Lesen und schreiben")
    
```

## Deployment

```mermaid
flowchart TD
    id1["Bei Github container registry (ghcr) eingelogt"]
    id2[Image vom Backend / Frontend bauen]
    id3[Auf ghcr hochladen]
    id4[Auf der LeoCloud eingelogen]
    id5[Auf der LeoCloud deployen]

    id1 --> id2
    id2 --> id3
    id3 --> id4
    id4 --> id5
```

## ERD

```mermaid
erDiagram

    ZipCode ||--o{ Restaurant : in
    Person  ||--o| Customer : ist
    Person ||--o| Employee : ist
    Employee }|--|| Restaurant : "arbeitet in"
    Restaurant }o--|| RestaurantTable : von

    Restaurant ||--o{ RestaurantOpeningTime : "öffnet um"
    Reservation ||--|{ RestaurantTable : "hat"
    Reservation }o--||  Restaurant : "hat"
    Customer ||--o{ Reservation : reserviert
    Category }o--|| RestaurantCategory : hat
    Restaurant }o--|| RestaurantCategory : von



    
    
    Person  {
        int id
        string firstName
        string lastName
        string password
        string email
    }
    Customer {
        string customerNumber
    }
    Employee {
        bool isAdmin
        int restaurantId
    }
    Category {
        int id
        string name
    }
    Reservation {
        int id
        DateTime reservationDay
        DateTime startTime
        DateTime endTime
        int customerId
        int restaurantTableId
        int restaurantId
    }
    
    Restaurant {
        int id
        string name
        int zipCodeId
        string address
        string streetNr
    }
    
    RestaurantOpeningTime {
        int id
        int day
        DateTime openingTime
        DateTime closingTime
        int restaurantId
    }
    
    RestaurantTable {
        int id
        int seatPlaces
        int restaurantId
    }
    
    ZipCode {
        int id
        string zipCodeNr
        string location
        string district
    }
    
    RestaurantCategory {
        int categoryId
        int restaurantId
    }
    


```

## Funktionalität
Am Anfang befindet man sich auf einer Login-Seite, wo man sich Anmelden kann oder die Option hat ein neues Konto / Restaurant zu erstellen.   
Anschließend wird man entweder zur Kunden- oder Restaurantseite weitergeleitet.   

### Kunde
Hier kommt man zum Restaurantfilter, wo man nach Restaurant mit mehreren Optionen filtern kann (Name oder Bezirk, Ort, Datum, Uhrzeit, Essen). Hierbei wird der Name, Beschreibung und ein Bild des Restaurants angezeigt. Wenn man anschließend auf ein Restaurant klickt, kommt man zu einer genaueren Übersicht vom Restaurant (Bilder, Öffnungszeiten, Beschreibung, Arten des Essens (Italienisch, vegan, ...)). In dieser Ansicht kann man dann auch direkt reservieren.   
Man kann sich aber auch die eigenen Reservierungen anschauen und stornieren falls gewünscht. 

### Restaurant - Mitarbeiter / Besitzer
Hier wird man zur Übersicht vom eigenen Restaurant weitergeleitet. Dabei kann man Reservierungen von Kunden anschauen, aber auch Daten vom Restaurant ändern. 

```mermaid
graph TD
    A[Login]
    B[Restaurant]
    C[Gast]
    D[Restaurantsuche]
    E[Reservierungsübersicht]
    F[Restaurantbearbeitung]
    G[Detailierte Restaurantansicht]
    H[Reservieren]
    I[Reservierungsübersicht]
    J[Details zur Reservierung]
    K[Reservierung stornieren]
    L[Reservierung verweigern]
    M[Manuelle Reservierung]

    A --> B
    A --> C
    B --> F
    B --> E
    C --> D
    D --> G
    G --> H
    H --Restaurant erhält Reservierung--> E
    E --> L
    E --> M
    H --> I
    C --> I
    I --> G
    E --> J
    I --> K
```

## Wichtigsten Funktionen
### Restaurantsuche
Hierbei werden die verschiedenen gegebenen Filter genommen und alle angewendet, wodurch wir eine Liste erhalten, mit der wir nur passende Restaurants erhalten. Aber wenn ein Restaurantname eingegeben wird, wird nur nach diesem gesucht und die andere Kriterien sind irrelevant.

### Reservierung
Bei der Reservierung muss man Datum mit Uhrzeit und Personenanzahl eingeben. Falls die Personenanzahl zu groß ist für einen Tisch, kann man eine Reservierung mit mehreren Tischen anfragen, diese muss dann aber vom Restaurant bestätigt werden. (Reservierung mit mehreren Tischen ist nur eine Erweiterung)

Ablauf bei der Reservierung:

```mermaid
stateDiagram-v2
state "Restaurantansicht" as r
state "Reservierungsdaten eingeben" as rv
state "Reservierung tätigen" as rt
state "Reservierung erfolgreich" as re
state "Reservierung bestätigen" as rb
state if <<choice>>
state if_b <<choice>>
state if_s <<choice>>
[*]--> r
r --> rv
rv --> rt
rt --> if
if --> re : Keine Bestätigung erforderlich
if --> rb : Bestätigung erforderlich
rb --> if_b
if_b --> re : Reservierunge bestätigt
if_b --> [*] : Reservierung verweigert
re --> if_s
if_s --> [*] : Reservierung storniert
if_s --> [*] : Restaurant besucht
if_s --> [*] : Nicht erschienen
```
