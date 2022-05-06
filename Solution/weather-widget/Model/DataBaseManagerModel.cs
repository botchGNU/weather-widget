using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace weather_widget.Model
{
    public class DataBaseManagerModel
    {
        private WeatherInfoListModel weatherInfos;
        private string FilePath = @"..\\..\\..\\..\\..\\WeatherInfo.db";
        private bool receivedJSON = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cityname"></param>
        public void GetDataFromOpenWeather(string cityname)
        {
            try
            {
                GetWeather(cityname);
            }
            catch (Exception ex)
            { Debug.WriteLine(ex.Message); }
        }
        private async void GetWeather(string cityname)
        {
            APIManagerModel apimanagerModel = new APIManagerModel();
            Task<WeatherInfoListModel> TaskweatherInfos = apimanagerModel.GetWeather(cityname);

            weatherInfos = await TaskweatherInfos;
            Debug.WriteLine(weatherInfos.Count);
            weatherInfos.cityid = 0;
            weatherInfos.countryzip = "AT";
            SaveIntoDatabase(cityname, weatherInfos.cityid, weatherInfos.countryzip);
        }

        private void SaveIntoDatabase(string CityName, int CityId, string CountryZip)
        {

            /*
            if (weatherInfos != null)
            {
                // TODO: Check if Cityname exists in Database Citylist!!
                weatherInfos.cityid = CityId;
                weatherInfos.countryzip = CountryZip;
                weatherInfos.cityname = CityName;

                weatherInfos.SaveToSqlite(FilePath);
            }
            */
        }

        // CRUD

        // TODO: Edit code to my needs
        /// <summary>
        /// This method is for inserting, updating or removing from Database
        /// </summary>
        /// <param name="query"></param>
        /// <param name="args"></param>
        /// <returns></returns>
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
        /// Reading DataBase
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private DataTable Execute(string query)
        {
            if (string.IsNullOrEmpty(query.Trim()))
                return null;

            using (var con = new SQLiteConnection(FilePath))
            {
                con.Open();
                using (var cmd = new SQLiteCommand(query, con))
                {
                    /*
                    foreach (KeyValuePair<string, object> entry in args)
                    {
                        cmd.Parameters.AddWithValue(entry.Key, entry.Value);
                    }
                    */

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
            const string query = "INSERT INTO User(XXX, XX) VALUES(@XXX, @XX)";

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
            const string query = "UPDATE WeatherInfoList SET WeatherDescription = @weatherdesc, WeatherIcon = @weathericon WHERE Id = @id";

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
            const string query = "DELETE from WeatherInfoList WHERE Id = @id";

            //here we are setting the parameter values that will be actually 
            //replaced in the query in Execute method
            var args = new Dictionary<string, object>
            {
                {"@id", 1} // ???? how to do ???
            };
            return ExecuteWrite(query, args);
        }

        /*
        public void SaveToSqlite(string fileName)
        {
            // Info zu SQLite:
            // https://SQLite.org
           
            // Install-Package system.data.sqlite

            // .NET Core (using Microsoft.Data.SQLite)
            SQLiteConnection conn = CreateSQLiteConnection(fileName);

            // Kommando erzeugen, das mit der DB kommuniziert
            SQLiteCommand cmd = new SQLiteCommand("DROP TABLE IF EXISTS adressdata", conn);
            cmd.ExecuteNonQuery();

 
            cmd.CommandText = "DROP TABLE IF EXISTS weatherinfo";

            cmd.ExecuteNonQuery();

            // TODO: cityid, cityname, coutryzip --> FOREIGN KEYS
            cmd.CommandText = @"CREATE TABLE weatherinfo(id INTEGER PRIMARY KEY,
                              cityid INTEGER, cityname TEXT, countryzip TEXT,
                              weatherdescription TEXT, weathericon TEXT, weatherdaytime TEXT, maxtemperature DOUBLE, 
                              mintemperature DOUBLE, winddirection DOUBLE, winddirectionasstring TEXT, windspeed DOUBLE, humidity DOUBLE)";
            cmd.ExecuteNonQuery();

            foreach (WeatherInfoModel item in this)
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
}
