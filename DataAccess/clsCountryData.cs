using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsCountryData
    {
        public enum enGender { male=0, female=1}
        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool IsFound=false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Countries where CountryID=@CountryID";
            SqlCommand command= new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) {
                    CountryName =(string) reader["@CountryName"];
                    reader.Close();
                    return true;
                }
                else { return  false; }
            }
            catch (Exception ex){ Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return false;
        }
        public static bool GetCountryInfoByName( string CountryName, ref int CountryID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from Countries where CountryName=@CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryID = (int)reader["@CountryID"];
                    reader.Close();
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return false;
        }
        public static DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries order by CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex) { //Console.WriteLine(ex.Message);
                                   }
            finally { connection.Close(); }
            return dt;
        }

    }
}
