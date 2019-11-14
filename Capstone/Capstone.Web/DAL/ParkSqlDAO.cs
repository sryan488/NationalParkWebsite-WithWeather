using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAO : IParkDAO
    {
        private readonly string connectionString;

        public ParkSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<Park> GetParks()
        {
            IList<Park> parks = new List<Park>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = @"SELECT * FROM park ORDER BY parkName ASC";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        parks.Add(RowToObject(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return parks;
        }

        public IList<Weather> GetWeather(string parkCode)
        {
            IList<Weather> weatherForecast = new List<Weather>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT *  FROM weather WHERE parkCode = @pc ORDER BY fiveDayForecastValue", conn);
                    cmd.Parameters.AddWithValue("@pc", parkCode);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather weather = new Weather();
                        weather.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        weather.Low = Convert.ToInt32(reader["low"]);
                        weather.High = Convert.ToInt32(reader["high"]);
                        weather.Forecast = Convert.ToString(reader["forecast"]);
                        weatherForecast.Add(weather);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return weatherForecast;
        }

        public Park GetPark(string code)
        {
            Park park = new Park();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM park WHERE parkCode = @pc", conn);
                    cmd.Parameters.AddWithValue("@pc", code);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        park = RowToObject(reader);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return park;
        }

            public int SavePost(Survey newSurvey)
        {

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = $"INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) VALUES (@pc, @ea, @s, @al)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@pc", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@ea", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@s", newSurvey.State);
                    cmd.Parameters.AddWithValue("@al", newSurvey.ActivityLevel);

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public IList<Survey> GetAllSurveys()
        {
            IList<Survey> surveys = new List<Survey>();

            try
            {
                // Create a new connection object
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    string sql = @"SELECT COUNT(parkName) count, parkName, p.parkCode FROM survey_result sr JOIN park p ON sr.parkCode = p.parkCode GROUP BY parkName, p.parkCode ORDER BY count DESC, parkName ASC";
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        surveys.Add(RowToSurvey(reader));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return surveys;

        }

        private Survey RowToSurvey(SqlDataReader reader)
        {
            Survey survey = new Survey();
            survey.ParkName = Convert.ToString(reader["parkName"]);
            survey.ParkCode = Convert.ToString(reader["parkCode"]);
            survey.Count = Convert.ToInt32(reader["count"]);
          
            return survey;
        }



        private Park RowToObject(SqlDataReader reader)
        {
            Park park = new Park();
            park.ParkCode = Convert.ToString(reader["parkCode"]);
            park.ParkName = Convert.ToString(reader["parkName"]);
            park.State = Convert.ToString(reader["state"]);
            park.Acreage = Convert.ToInt32(reader["acreage"]);
            park.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
            park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
            park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
            park.Climate = Convert.ToString(reader["climate"]);
            park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
            park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
            park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
            park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
            park.ParkDescription = Convert.ToString(reader["parkDescription"]);
            park.EntryFee = Convert.ToDouble(reader["entryFee"]);
            park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);
            return park;
        }

    }
}
