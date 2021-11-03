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
    public class QuestionInfoManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable GetQuestionInfo()
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT QuestionID, QuestionName, Type, Required
                    FROM QuestionInfo
                    ORDER BY QuestionID ASC
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

        public static DataTable GetQuestionByID(string id)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT QuestionID, QuestionName, Type, Required
                    FROM QuestionInfo
                    WHERE ID = @id
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
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

        public static DataRow GetQuestionInfoByID(string id)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT QuestionID, QuestionName, Type, Required
                    FROM QuestionInfo
                    WHERE ID = @id
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
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

        public static void CreateQuestion(string id,int questionID, string questionName, string type, int required)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" INSERT INTO QuestionInfo
                    (
                        ID
                        ,QuestionID
                        ,QuestionName
                        ,Type
                        ,Required
                    )
                    VALUES
                    (
                        @id
                        ,@questionID
                        ,@questionName
                        ,@type
                        ,@required
                    ) ";


            // connect db & execute
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    command.Parameters.AddWithValue("@questionName", questionName);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@required", required);

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

        public static void DeleteQuestion(string questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" DELETE [QuestionInfo]
                    WHERE QuestionID = @questionID ";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@questionID", questionID);

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
