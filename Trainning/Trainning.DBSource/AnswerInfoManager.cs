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
    public class AnswerInfoManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }
        public static DataTable GetAnswerInfoByID(string id, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT ID, QuestionID, Answer
                    FROM AnswerInfo
                    WHERE ID = @id AND QuestionID = @questionID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
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
    }
}
