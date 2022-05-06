# Mikail

```mermaid
classDiagram

	DataBaseManager --> APIManager : get data from internet
	
	APIManager --> JSONModel : helper class to deserialize data from JSON
	
	%% This is helper class, which gets the properties, that we need for our application
	JSONModel --> JSONResponce
	JSONModel --> JSONListItem
	JSONModel --> JSONMainInfo
	JSONModel --> JSONWeatherType
	JSONModel --> JSONWindInfo
	JSONModel --> JSONCity	
	
	%% After that, APIManager converts this thata into a List of weatherinfomodel
	APIManager --> WeatherInfoListModel : convert received JSON into WeatherInfoListModel
	
	WeatherInfoListModel --|> WeatherInfoModel : inherits List of WeatherInfoModel
	
	DataBaseUpdateManager --> DataBaseManager : give info to update current weather data
	
	DataBaseManager --> WeatherInfoListModel : stores into database
	
	%% Weather data class, which are going to be displayed (not fix at the moment)
	DataBaseManager --> WeatherToDisplay : get the relevant data, that has to be displayed
	
	
    class APIManager{
    -string API_KEY
    -WeatherInfoListModel GetWeatherInfos(string JSONContent)
    -ToWeatherInfoModel WeatherInfoModel(JSONListItem item)
    -string WindDirConverter(double winddir)
    +Task GetWeather(string location)
    }
    class WeatherToDisplay{
    + MAYBE, THERE WILL BE ANOTHER CLASS, THAT WILL REPRESENT THE DATA, WHICH IS GOING TO BE BINDED (DISPLAYED)
    }
    class DataBaseManager{
    -
    -
    + THIS CLASS WILL HAVE METHODS, TO GET MAX, MIN, AVG, .. FROM DATABASE
    + THIS CLASS WILL HAVE METHOD TO STORE DATA FROM DATABASE INTO WEATHERINFOLISTMODEL
    + THIS CLASS WILL SAVE DATA RECEIVED FROM INTERNET INTO DATABASE
    }
    class DataBaseUpdateManager{
    -
    -
    + THIS CLASS WILL GIVE INFO TO UPDATE CURRENT DATA (WEATHERFORECASTS)
    }
    class JSONModel{
    +class JSONResponce
    +class JSONListItem
    +class JSONMainInfo
    +class JSONWeatherType
    +class JSONWeatherWindInfo
    +class JSONCity
    }
    class JSONResponce {
    	~List<JSONListItems> items
    	~JSONCity City
    }
    class JSONListItem {
        +List<JSONWeatherType> WeatherTypes
    	+JSONWeatherWindInfo WeatherWindInfo
    	+string DateTime
    }
    class JSONMainInfo {
       	+double Temp_min
       	+double Temp_max
       	+double Humidity
    }
    class JSONWeatherType {
    	+string Description
    	+string Icon
    }
    class JSONWindInfo {
    	+double WindSpeed
    	+double WindDirection
    }
    class JSONCity {
    	+string Name
    	+string CountryZip
    }
    
    class WeatherInfoModel{
        +string WeatherDescription
    	+string WeatherIcon
    	+DateTime WeatherDayTime
    	+double MaxTemperature
    	+double MinTemperature
    	+double WindDirection
    	+string WindDirectionAsString
    	+double WindSpeed
    	+double Humidity
    	+WeatherInfoModel(string weatherdesc, ...)
    }
    class WeatherInfoListModel{
    }
```