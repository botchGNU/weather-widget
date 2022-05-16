using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Globalization;

namespace weather_widget.Model
{
    class DataBaseManagerModel
    {
        public WeatherToDisplayListModel WeatherToDisplays { get; private set; }

        private WeatherInfoListModel weatherInfos;

        private const string FilePath = @"..\\..\\..\\..\\..\\weatherwidget.db";

        private void UpdateWeatherToDisplay()
        {
            // TO DO: UPDATE WEATHERTODISPLAY after Reading and Saving!!!+

            // TO DO: Do this in DataBase Manager
            /*
            private const string unitWinSpeed = "m/s";
            private const string unitIcon = ".png";
            private const string unitTemp = "°C";
            private const string unitHumidity = "%";
            */
        }

        /// <summary>
        /// This method gets forecasts from openweather
        /// </summary>
        public void GetDataFromOpenWeather(string CityName)
        {
            try
            {
                GetWeather(CityName);
            }
            catch (Exception ex)
            { 
                // Inform user that it failed
                Debug.WriteLine(ex.Message); 
            }
        }

        private async void GetWeather(string CityName)
        {
            APIManagerModel apimanagerModel = new APIManagerModel();
            Task<WeatherInfoListModel> TaskweatherInfos = apimanagerModel.GetWeather(CityName);

            weatherInfos = await TaskweatherInfos;

            // Rankweil
            //weatherInfos.cityid = 2767974;
            //weatherInfos.countryzip = "AT";

            // London
            weatherInfos.cityname = CityName;
            weatherInfos.cityid = 2643743;
            weatherInfos.countryzip = "GB";
            
            SaveIntoDatabase(CityName, weatherInfos.cityid, weatherInfos.countryzip);
        }
        public void LoadFromDatabase(string CityName, int CityId, string CountryZip)
        {
            SQLiteDataReader SQLiteDataReader = null; // see our project


            //return new WeatherInfoListModel(); // TO DO: reading
        }

        public void SaveIntoDatabase(string CityName, int CityId, string CountryZip)
        {

            // .NET Framework
            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            // create command, which will communicate with DB
            SQLiteCommand cmd = new SQLiteCommand(connection);

            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS weatherinfo(
                              id INTEGER PRIMARY KEY, 
                              cityid INTEGER, cityname TEXT, countryzip TEXT, 
                              weatherdescription TEXT, weathericon TEXT, weatherdaytime TEXT, maxtemperature DOUBLE, mintemperature DOUBLE, winddirection DOUBLE, winddirectionasstring TEXT, windspeed DOUBLE, humidity DOUBLE,
                              FOREIGN KEY(cityid) REFERENCES citylist(id), FOREIGN KEY(cityname) REFERENCES citylist(cityname), FOREIGN KEY(countryzip) REFERENCES citylist(countryzip)
                              )";
            cmd.ExecuteNonQuery();

            foreach (WeatherInfoModel item in weatherInfos)
            {
                // CRUD ... Create Read Update

                // TO DO: Use DateTime from DataBaseUpdateManager!!
                // TO DO: CityName, CityId & co. should be prettier coded
                string cmdtext = "";
                if(CheckIfCurrentDataExist(item, CityName) == false)
                {
                    cmdtext = InsertIntoDataBase(item, CityName, CityId, CountryZip); // ->INSERT
                }
                else
                {
                    cmdtext = UpdateDataBase(item, CityName, CityId, CountryZip); // -> UPDATE 
                }

                cmd.CommandText = cmdtext;

               /*cmd.commandtext = $"insert into " +
                    $"weatherinfo(cityid, cityname, countryzip, weatherdescription, weathericon, weatherdaytime, maxtemperature, mintemperature, winddirection, winddirectionasstring, windspeed, humidity)" +
                    $"values({(cityid)},'{cityname}','{countryzip}','{item.weatherdescription}','{item.weathericon}','{item.weatherdaytime.tostring("yyyy-mm-dd hh:mm:ss")}','{item.maxtemperature.tostring(new cultureinfo("en-us"))}','{item.mintemperature.tostring(new cultureinfo("en-us"))}','{item.winddirection.tostring(new cultureinfo("en-us"))}','{item.winddirectionasstring}', '{item.windspeed.tostring(new cultureinfo("en-us"))}', '{item.humidity.tostring(new cultureinfo("en-us"))}')";
               */
                cmd.ExecuteNonQuery();
            }

            connection.Close();
        }
        /// <summary>
        /// Gives you a list of string, depending on the letters of a city
        /// </summary>
        /// <param name="LettersForCityname">Letters of a city, that needs to be searched. Capital or small letters doesn't matter.</param>
        /// <returns>List of string</returns>
        public List<string> GetCitiesByLetters(string LettersForCityname)
        {
            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            SQLiteCommand cmd = connection.CreateCommand();

            cmd.CommandText = $"SELECT cityname FROM citylist " +
                                $"WHERE upper(cityname) LIKE '{LettersForCityname.ToUpper()}%'";

            SQLiteDataReader reader = cmd.ExecuteReader();
            List<string> cities= new List<string>();

            while (reader.Read())
            {
                cities.Add(reader.GetString("cityname"));
            }
            connection.Close();

            if(cities.Count > 0)
            {
                return cities;
            }
            else
            {
                cities.Add("Not Available! City doesn't exist in citylist!"); // NA... not available
                return cities; 
            }
        }

        private string InsertIntoDataBase(WeatherInfoModel weatherInfo, string CityName, int CityId, string CountryZip)
        {
            string sqlitecmd = $"INSERT INTO " +
                    $"weatherinfo(cityid, cityname, countryzip, weatherdescription, weathericon, weatherdaytime, maxtemperature, mintemperature, winddirection, winddirectionasstring, windspeed, humidity)" +
                    $" VALUES({(CityId)},'{CityName}','{CountryZip}','{weatherInfo.WeatherDescription}','{weatherInfo.WeatherIcon}','{weatherInfo.WeatherDayTime.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                    $" '{weatherInfo.MaxTemperature.ToString(new CultureInfo("en-US"))}','{weatherInfo.MinTemperature.ToString(new CultureInfo("en-US"))}','{weatherInfo.WindDirection.ToString(new CultureInfo("en-US"))}'," +
                    $" '{weatherInfo.WindDirectionAsString}', '{weatherInfo.WindSpeed.ToString(new CultureInfo("en-US"))}', '{weatherInfo.Humidity.ToString(new CultureInfo("en-US"))}')"; 

            return sqlitecmd;
        }

        private bool CheckIfCurrentDataExist(WeatherInfoModel weatherinfo, string CityName)
        {
            SQLiteConnection connection = CreateSQLiteConnection(FilePath);

            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT weatherdaytime FROM weatherinfo " +
                                        $"WHERE weatherdaytime == '{weatherinfo.WeatherDayTime.ToString("yyyy-MM-dd HH:mm:ss")}' " +
                                        $"AND cityname == '{CityName}'";

            SQLiteDataReader reader = command.ExecuteReader();

            int i = 0;
            while (reader.Read())
            {
                i++;
            }
            connection.Close();

            if(i > 0)
            {
                return true; // it exists
            }
            return false;
        }
        private string UpdateDataBase(WeatherInfoModel weatherInfo, string CityName, int CityId, string CountryZip)
        {
            string sqlitecmd = sqlitecmd = $"UPDATE weatherinfo" +
                    $" SET weatherdescription = '{weatherInfo.WeatherDescription}'," +
                        $" weathericon = '{weatherInfo.WeatherIcon}'," +
                        $" maxtemperature = '{weatherInfo.MaxTemperature.ToString(new CultureInfo("en-US"))}'," +
                        $" mintemperature = '{weatherInfo.MinTemperature.ToString(new CultureInfo("en-US"))}'," +
                        $" winddirection = '{weatherInfo.WindDirection.ToString(new CultureInfo("en-US"))}'," +
                        $" winddirectionasstring = '{weatherInfo.WindDirectionAsString}', " +
                        $" windspeed = '{weatherInfo.WindSpeed.ToString(new CultureInfo("en-US"))}', " +
                        $" humidity = '{weatherInfo.Humidity.ToString(new CultureInfo("en-US"))}'" +
                    $" WHERE cityid = {(CityId)} AND cityname = '{CityName}' AND countryzip = '{CountryZip}' " +
                        $" AND weatherdaytime = '{weatherInfo.WeatherDayTime.ToString("yyyy-MM-dd HH:mm:ss")}'";

            return sqlitecmd;
        }

        private SQLiteConnection CreateSQLiteConnection(string fileName)
        {
            SQLiteConnection conn = new SQLiteConnection($"Data Source={fileName}");
            try
            {
                conn.Open();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return conn;
        }
    }

        // CRUD
        /*
        // TODO: Edit code to my needs
        /// <summary>
        /// This method is for inserting, updating or removing from database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="args"></param>
        /// <returns>number of affected elements in database</returns>
        private int ExecuteWrite(string query, Dictionary<string, object> args)
        {
            int numberOfRowsAffected;

            //setup the connection to the database
            using (var con = new SQLiteConnection(FilePath))
            {
                con.Open();

                //open a new command
                using (SQLiteCommand cmd = new SQLiteCommand(query, con))
                {
                    //set the arguments given in the query
                    foreach (var pair in args)
                    {
                        cmd.Parameters.AddWithValue(pair.Key, pair.Value);
                    }

                    //execute the query and get the number of row affected
                    numberOfRowsAffected = cmd.ExecuteNonQuery();
                }
                return numberOfRowsAffected;
            }
        }

        // TODO: Edit code to my needs
        /// <summary>
        /// Method for reading content from database
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable ReadDatabase(string query)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using (var con = new SQLiteConnection(FilePath))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    foreach (KeyValuePair<string, object> entry in args)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }

                    var da = new SQLiteDataAdapter(cmd);

                    var dt = new DataTable();
                    da.Fill(dt);

                    da.Dispose();
                    return dt;
                }
            }
        }


        // TODO: Edit code to my needs
        /// <summary>
        /// Adding weather forecasts to database
        /// </summary>
        /// <param name="weatherInfo"></param>
        /// <returns></returns>
        private int AddWeatherInfo(WeatherInfoModel weatherInfo)
        {
            const string query = "INSERT INTO weatherinfo(XXX, XX) VALUES(@XXX, @XX)";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@XXX", weatherInfo.WeatherIcon},
                {"@XX", weatherInfo.WeatherDayTime.ToString()}
            };

            return ExecuteWrite(query, args);
        }

        // TODO: Edit code to my needs
        private int EditWeatherInfo(WeatherInfoModel weatherInfo)
        {
            const string query = "UPDATE weatherinfo SET WeatherDescription = @weatherdesc, WeatherIcon = @weathericon WHERE Id = @id";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                //{"@id", user.Id}, ???
                {"@weatherdesc", weatherInfo.WeatherDescription},
                {"@weathericon", weatherInfo.WeatherIcon}
            };

            return ExecuteWrite(query, args);

        }


        // TODO: Edit code to my needs
        private int DeleteWeatherInfo(WeatherInfoModel weatherInfo)
        {
            const string query = "DELETE from weatherinfo WHERE Id = @id";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@id", 1} // ???? how to do ???
            };
            return ExecuteWrite(query, args);
        }

        private void SaveIntoDB(WeatherInfoListModel weatherInfos, string fileName)
        {
            // .NET Core (using Microsoft.Data.SQLite)
            SQLiteConnection conn = CreateSQLiteConnection(fileName);

            // TODO: Check if it exists

            /*
            cmd.CommandText = "DROP TABLE IF EXISTS weatherinfo";

            cmd.ExecuteNonQuery();
            */

            // IF IT DOESN'T EXIST --> CREATE TABLE weatherinfo

            // TODO: cityid, cityname, coutryzip --> FOREIGN KEYS
            /*
            cmd.CommandText = @"CREATE TABLE weatherinfo(id INTEGER PRIMARY KEY,
                              cityid INTEGER, cityname TEXT, countryzip TEXT,
                              weatherdescription TEXT, weathericon TEXT, weatherdaytime TEXT, maxtemperature DOUBLE, 
                              mintemperature DOUBLE, winddirection DOUBLE, winddirectionasstring TEXT, windspeed DOUBLE, humidity DOUBLE)";
            cmd.ExecuteNonQuery();


            foreach (WeatherInfoModel weatherInfo in weatherInfos)
            {
                // CRUD ... Create Read Update Delete


                // neue Datensätze -> INSERT ("create")
                cmd.CommandText = $"INSERT INTO " +
                    $"weatherinfo(cityid, cityname, countryzip, weatherdescription, weathericon, weatherdaytime, maxtemperature, mintemperature, winddirection, winddirectionasstring, windspeed, humidity)" +
                    $"VALUES({(cityid)},'{cityname}','{countryzip}','{item.WeatherDescription}','{item.WeatherIcon}','{item.WeatherDayTime.ToString()}','{item.MaxTemperature}','{item.MinTemperature}','{item.WindDirection}','{item.WindDirectionAsString}', '{item.WindSpeed}', '{item.Humidity}')";
                cmd.ExecuteNonQuery();

                // geänderte Datensätze -> UPDATE 

                // gelöschte Datensätze -> DELETE

            }

            conn.Close();
        }
        private SQLiteConnection CreateSQLiteConnection(string fileName)
        {
            SQLiteConnection conn = new SQLiteConnection($"Data Source={fileName}");
            try
            {
                conn.Open();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return conn;
        }
            */
}
