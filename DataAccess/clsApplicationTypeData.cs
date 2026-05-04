using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.clsDataAccessSettings;

namespace DataAccess
{
 public    class clsApplicationTypeData
    {
       public static bool GetApplicationTypeDataByID(int ApplicationTypeID , 
           ref string ApplicationTypeTitle , ref float ApplicationFees) {
          bool IsFound=false;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from ApplicationTypes where" +
                " ApplicationTypeID=@ApplicationTypeID";
            SqlCommand command = new SqlCommand(query,Connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) { 
                    IsFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees =  Convert.ToSingle( reader["ApplicationFees"]);
                }
                else { IsFound = false; }
                reader.Close();
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                IsFound = false; }
            finally { Connection.Close(); }
                return IsFound;

        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from ApplicationTypes order by ApplicationTypeTitle";
            SqlCommand command = new SqlCommand(query,Connection);
            try { 
                Connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if (reader.HasRows) {
                    dt.Load(reader);
                    
                }
                reader.Close();

            }
            catch (Exception ex) {
                //Console.WriteLine(ex.Message); 
            }
            finally { Connection.Close(); }
            return dt;
        }
        public static int AddNewApplicationType(string ApplicationTypeTitle , 
            float ApplicationFees)
        {
            int ApplicationTypeID = -1;
            SqlConnection Connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "insert into  ApplicationTypes " +
                "(ApplicationTypeTitle,  ApplicationFees) values(@ApplicationTypeTitle,@ApplicationFees)    SELECT SCOPE_IDENTITY();";
           SqlCommand command=new SqlCommand(query,Connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);
            try { 
                Connection.Open();
                object Result=command.ExecuteScalar();
                if (Result != null &&int.TryParse(Result.ToString(), out int ID) ) {
                    ApplicationTypeID = ID;
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { Connection.Close(); }
            return ApplicationTypeID;

        }

        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  ApplicationTypes  
                            set ApplicationTypeTitle = @Title,
                                ApplicationFees = @Fees
                                where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Fees", Fees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

    }
}
