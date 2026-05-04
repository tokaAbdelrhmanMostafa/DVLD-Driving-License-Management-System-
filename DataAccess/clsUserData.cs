using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class clsUserData
    {
        public static bool GetUserInfoByID
           (int UserID, ref int PersonID,
            ref string UserName,
            ref string Password, ref bool IsActive)
        {
            bool IsFound=false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read()) {
                    IsFound = true;
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password=(string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                }
                else { IsFound = false; }
                reader.Close();
            }
            catch { IsFound = false; }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool GetUserInfoByUsernameAndPassword(
             string UserName,  string Password, ref int UserID,
         ref   int PersonID , ref bool IsActive )
        {
            {
                bool IsFound=false;
                SqlConnection connection = new SqlConnection( clsDataAccessSettings.ConnectionString);
                string query = @"select * from Users where UserName =@UserName and Password=@Password ";
          SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@UserName", UserName);
                command.Parameters.AddWithValue("@Password", Password);

                try
                { 
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read()) { 
                        IsFound = true;
                        UserID = (int)reader["UserID"];
                        PersonID = (int)reader["PersonID"];
                        UserName = (string)reader["UserName"];
                        Password = (string)reader["Password"];
                        IsActive = (bool)reader["IsActive"];
                        reader.Close();
                    }
                    else { IsFound = false; }

                }
                catch { return false; }
                finally{ connection.Close(); }
                return IsFound;
            }

        }

        public static int AddNewUser(int PersonID , string UserName ,
            string Password , bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"insert into Users
(PersonID ,  UserName , Password , IsActive)
values(@PersonID ,  @UserName , @Password , @IsActive);
SELECT SCOPE_IDENTITY();
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try {
                connection.Open();
               object result= command.ExecuteScalar();
                if (result != DBNull.Value && int.TryParse(result.ToString(), out int id)) {
                    UserID = id;
                }
            }
            catch {  }
            finally {  connection.Close(); }
            return UserID;

        }

        public static bool UpdateUser(int UserID, int PersonID, 
            string UserName,
             string Password, bool IsActive)
        {
            int RowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @" Update Users
set PersonID = @PersonID,
    UserName = @UserName,
    Password = @Password,
    IsActive = @IsActive
where UserID = @UserID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try { 
                connection.Open();
                RowAffected = command.ExecuteNonQuery();
            }
            catch { }
            finally { connection.Close(); }
           return  RowAffected > 0;
        }

        public static DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT  Users.UserID, Users.PersonID,
                            FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                             Users.UserName, Users.IsActive
                             FROM  Users INNER JOIN
                                    People ON Users.PersonID = People.PersonID";

            SqlCommand cmd = new SqlCommand(query, connection);
            try {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows) { dt.Load(reader); }
            }
            catch { }
            finally { connection.Close(); }
            return dt;
        }

        public static bool DeleteUser(int UserID)
        {
            int RowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"delete Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try {  connection.Open();
                RowAffected= command.ExecuteNonQuery();}
            catch { }
            finally { connection.Close(); }
            return RowAffected > 0;
        }

        public static bool IsUserExist(int UserID) {
            bool IsFound=false;
            SqlConnection connection = new SqlConnection( clsDataAccessSettings.ConnectionString);
            string query = @"select found=1 from Users where UserID=@UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try { 
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
            }
            catch { return false; }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool IsUserExist(string UserName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"select found=1 from Users where UserName=@UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
            }
            catch { return false; }
            finally { connection.Close(); }
            return IsFound;
        }
        public static bool ChangePassword(int UserID , string NewPassword)
        {
            int RowAffected = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"Update Users 
set Password=@NewPassword
where UserID=@UserID";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            try { 
                connection.Open();
              RowAffected = cmd.ExecuteNonQuery();

            }
            catch { return false; }
            finally { connection.Close(); }return RowAffected > 0;

        }


    }

}
