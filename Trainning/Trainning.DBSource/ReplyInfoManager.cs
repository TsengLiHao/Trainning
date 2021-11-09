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

        public static DataTable GetQuestionByID()
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT DISTINCT Name, ReplyID, Phone, Email, Age, ReplyTime, ListID, QuestionID, ReplyAnswer
                    FROM ReplyInfo
                    ORDER BY ReplyID DESC
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
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
