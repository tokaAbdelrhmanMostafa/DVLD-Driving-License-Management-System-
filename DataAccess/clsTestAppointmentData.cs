using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
   public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
            ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, 
            ref bool IsLocked, ref int RetakeTestApplicationID)
            { 
            bool IsFound=false;
            SqlConnection connection= new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from TestAppointments where  TestAppointmentID=@TestAppointmentID  order by AppointmentDate Desc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try { 
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

                    IsLocked = (bool)reader["IsLocked"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = (float)reader["PaidFees"];
                   LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    IsFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];

                }
                else { IsFound = false; }
                reader.Close();
            }
            catch (Exception ex){ 
                IsFound = false;
                Console.WriteLine(ex.Message);
            }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool GetLastTestAppointment ( int LocalDrivingLicenseApplicationID,  int TestTypeID,
            ref int TestAppointmentID,ref DateTime AppointmentDate,
            ref float PaidFees, ref int CreatedByUserID,ref bool IsLocked,ref int RetakeTestApplicationID)
       
        { 
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT       top 1 *
                FROM            TestAppointments
                WHERE        (TestTypeID = @TestTypeID) 
                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                order by TestAppointmentID Desc";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try { 
               
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

                    IsLocked = (bool)reader["IsLocked"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = (float)reader["PaidFees"];
                   // LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    IsFound = true;
                    //TestTypeID = (int)reader["TestTypeID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                }
                else { IsFound = false; }
                reader.Close();

            }
            catch (Exception ex) { IsFound = false; }
            finally { connection.Close(); }
            return IsFound;
        }
       
    public static DataTable GetAllTestAppointments()
        {
            DataTable dt= new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from   TestAppointments";
            SqlCommand command = new SqlCommand(query, connection);
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { dt.Load(reader); }
                reader.Close();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }

            return dt;
        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(
            int LocalDrivingLicenseApplicationID, int TestTypeID) { 
        DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select * from   TestAppointments where 
TestTypeID=@TestTypeID
and
(LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try { connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { dt.Load(reader); 
                }
                reader.Close ();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return dt;


        }
        public static int AddNewTestAppointment(
             int TestTypeID, int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate, float PaidFees, int CreatedByUserID,
             int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;
          SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into TestAppointments
(TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,PaidFees,
CreatedByUserID,RetakeTestApplicationID)
values(@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate,@PaidFees,
@CreatedByUserID,@RetakeTestApplicationID)
select SCOPE_IDENTITY();
";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);

            try
            { 
                connection.Open();
              object result= command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
                }

            }
            catch (Exception ex) { Console.WriteLine(); }
            finally { connection.Close(); }
            return TestAppointmentID;
        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID,
            int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate, float PaidFees,
             int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestAppointments  
                            set TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked=@IsLocked,
                                RetakeTestApplicationID=@RetakeTestApplicationID
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);





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

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try { 
                connection.Open();
               object result= command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }

            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { connection.Close(); }
            return TestID;
        }

    }
}
