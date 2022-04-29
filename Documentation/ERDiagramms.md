```mermaid
 erDiagram
       WEATHERINFO { 
       		int id PK "Unique id with autoincrement"
       		string CityName FK "The name of the city from Citylist"
            string CountryZip FK "The zipped name of the country from Citylist"
            string Weather "Description of current weather"
            string WeatherIcon "Weathericon path --> Weathericon; for displaying the icon e.g. xxxFOLDERxxx/icons/04d.png"
            string WeatherDay "Weatherday: Datetime"
            string Temperature "avg. temperature in these 3 hours"
            string MaxTemperature ""
            string MinTemperature
            string WindDirection
            string WindSpeed
            string Humidity      		
       }
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
