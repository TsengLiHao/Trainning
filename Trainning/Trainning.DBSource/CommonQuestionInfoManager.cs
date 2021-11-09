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
    public class CommonQuestionInfoManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable GetCommonQuestionInfo()
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT CommonQuestionID, CommonQuestionTitle, CommonQuestionName, Type, Required, Answer
                    FROM CommonQuestionInfo
                    ORDER BY CommonQuestionID ASC
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

        public static DataRow GetCommonQuestionInfoByTitle(string commonQuestionTitle)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT CommonQuestionTitle, CommonQuestionName, Type, Required, Answer
                    FROM CommonQuestionInfo
                    WHERE CommonQuestionTitle=@commonQuestionTitle
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@commonQuestionTitle", commonQuestionTitle);
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
        public static void CreateCommonQuestion(string commonQuestionTitle, string commonQuestionName, string type, int required, string answer)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" INSERT INTO CommonQuestionInfo
                    (
                        CommonQuestionTitle
                        ,CommonQuestionName
                        ,Type
                        ,Required
                        ,Answer
                    )
                    VALUES
                    (
                        @commonQuestionTitle
                        ,@commonQuestionName
                        ,@type
                        ,@required
                        ,@answer
                    ) ";


            // connect db & execute
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@commonQuestionTitle", commonQuestionTitle);
                    command.Parameters.AddWithValue("@commonQuestionName", commonQuestionName);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@required", required);
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

        public static bool UpdateCommonQuestion(int commonQuestionID, string commonQuestionTitle, string commonQuestionName, string type, int required, string answer)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" UPDATE CommonQuestionInfo
                    SET
                        CommonQuestionTitle    = @commonQuestionTitle
                        ,CommonQuestionName     = @commonQuestionName
                        ,Type                   = @type
                        ,Required               = @required
                        ,Answer                 = @answer
                    WHERE CommonQuestionID = @commonQuestionID ";


            // connect db & execute
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@commonQuestionID", commonQuestionID);
                    command.Parameters.AddWithValue("@commonQuestionTitle", commonQuestionTitle);
                    command.Parameters.AddWithValue("@commonQuestionName", commonQuestionName);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@required", required);
                    command.Parameters.AddWithValue("@answer", answer);

                    try
                    {
                        connection.Open();
                        int effectRow = command.ExecuteNonQuery();

                        if (effectRow == 1)
                            return true;
                        else
                            return false;
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteLog(ex);
                        return false;
                    }
                }
            }
        }

        public static void DeleteCommonQuestion(int commonquestionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" DELETE [CommonQuestionInfo]
                    WHERE CommonQuestionID = @commonquestionID ";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@commonquestionID", commonquestionID);

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
