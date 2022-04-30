# Data-Structure:


```mermaid
 erDiagram
 	   CITYLIST ||..o{ WEAHTERINFO : canhave
 	   CITYLIST{
 	   int id PK "Unique id for city"
 	   string name "city name"
 	   string state "state zip (is not going to be used)"
 	   string country "country zip"
 	   string coord "coordinates stored e.g. as {'lon': 47.159401, 'lat': 34.330502}"
 	   }
       WEATHERINFO { 
       		int id PK "Unique id with autoincrement"
       		int CityID FK "Foreign Key: The id of the city from Citylist"
       		string CityName FK "Foreign Key: The name of the city from Citylist"
            string CountryZip FK "The zipped name of the country from Citylist"
            string WeatherDescription "Description of current weather"
            string WeatherIcon "Weathericon e.g. 04d.png"
            DateTime WeatherDayTime "Datetime"
            double MaxTemperature "within 3h"
            double MinTemperature "within 3h"
            double WindDirection "as value"
            double WindDirectionAsString "as string e.g. N, NW,..."
            double WindSpeed "in m/s"
            double Humidity "in %"       		
       }
```

Each city can have 0 or more forecasts. Average/min./max. temperature is going to be received by database.



Example for ER-Diagram:




```mermaid
erDiagram
DOG {
         int age
         string breed
         string pedigree
       }
       OWNER ||..|{ DOG : owns
       OWNER {
         string name
         string address
         string homePhoneNumber
         string mobilePhoneNumber
       }
       TRAINER }|..|{ DOG : trains
       PROGRAMME }|--|| TRAINER : "operated by"
       DOG ||--|{ PROGRAMME : attends
       PROGRAMME {
         float costPerSession
       }
```


Information for creating an ER-Diagram

| Value (left) | Value (right) | Meaning                       |
| ------------ | ------------- | ----------------------------- |
| `|o`         | `o|`          | Zero or one                   |
| `||`         | `||`          | Exactly one                   |
| `}o`         | `o{`          | Zero or more (no upper limit) |
| `}|`         | `|{`          | One or more (no upper limit)  |

Keys can be "PK" or "FK", for Primary Key or Foreign Key. And a `comment` is defined by double quotes at the end of an attribute. Comments themselves cannot have double-quote characters in them.
