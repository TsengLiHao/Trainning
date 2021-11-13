using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainning.DBSource
{
    public class ReplyInfoManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable GetReplyInfoByListID(string listID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" WITH x AS (SELECT [Name],ReplyTime,ListID,
                    RN = Row_number() 
                         OVER( 
                         partition BY [Name]
                         ORDER BY ReplyTime DESC) 
                         FROM   ReplyInfo) 
                    SELECT [Name],ReplyTime,ListID
                    FROM   x
                    WHERE  rn = 1 AND ListID=@listID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@listID", listID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        public static DataTable GetReplyInfo(string listID, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT Name, Phone, Email, Age, ReplyTime, ListID, QuestionID, ReplyAnswer
                    FROM ReplyInfo
                    WHERE ListID=@listID AND QuestionID=@questionID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@listID", listID);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        return dt;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        public static DataRow GetReplyByName(string name)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT Name, Phone, Email, Age, ReplyTime, ListID, QuestionID, ReplyAnswer
                    FROM ReplyInfo
                    WHERE Name=@name
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0)
                            return null;

                        DataRow dr = dt.Rows[0];
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        public static DataRow GetReply(string name, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT Name, Phone, Email, Age, ReplyTime, ListID, QuestionID, ReplyAnswer
                    FROM ReplyInfo
                    WHERE Name=@name AND QuestionID=@questionID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        reader.Close();

                        if (dt.Rows.Count == 0)
                            return null;

                        DataRow dr = dt.Rows[0];
                        return dr;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return null;
                    }
                }
            }
        }

        public static void CreateReply(string name, int phone, string email, int age, string listID, int questionID, string answer)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" INSERT INTO ReplyInfo
                    (
                        Name
                        ,Phone
                        ,Email
                        ,Age
                        ,ReplyTime
                        ,ListID
                        ,QuestionID
                        ,ReplyAnswer
                    )
                    VALUES
                    (
                        @name
                        ,@phone
                        ,@email
                        ,@age
                        ,@replyTime
                        ,@listID
                        ,@questionID
                        ,@answer
                    ) ";


            // connect db & execute
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@phone", phone);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@age", age);
                    command.Parameters.AddWithValue("@replyTime", DateTime.Now);
                    command.Parameters.AddWithValue("@listID", listID);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    command.Parameters.AddWithValue("@answer", answer);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                    }
                }
            }
        }
    }
}
