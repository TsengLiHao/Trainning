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
                $@" SELECT DISTINCT QuestionID, QuestionName, Type, Required, Answer
                    FROM QuestionInfo
                    WHERE ID = @id
                    ORDER BY QuestionID ASC
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

        public static void CreateQuestion(string id, int questionID, string questionName, string type, int required, string answer)
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
                        ,Answer
                    )
                    VALUES
                    (
                        @id
                        ,@questionID
                        ,@questionName
                        ,@type
                        ,@required
                        ,@answer
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

        public static bool UpdateQuestion(string id, int questionID, string questionName, string type, int required, string answer)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" UPDATE QuestionInfo
                    SET
                        ID              = @id
                        ,QuestionID     = @questionID
                        ,QuestionName   = @questionName
                        ,Type           = @type
                        ,Required       = @required
                        ,Answer         = @answer
                    WHERE QuestionID = @questionID ";


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

        public static void DeleteQuestion(int questionID)
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

        public static DataTable GetAnswerInfoByID(string id, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT ID, QuestionID, value
                    FROM QuestionInfo
                    CROSS APPLY STRING_SPLIT(Answer, ';')
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

        public static DataRow GetCalSingle(string id, int questionID, string value, string answer)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT COUNT(Reply.ReplyAnswer) as cal
                    FROM QuestionInfo
					JOIN ReplyInfo as Reply
					ON Reply.QuestionID = QuestionInfo.QuestionID
                    CROSS APPLY STRING_SPLIT(Answer, ';')
                    WHERE ID = @id AND Reply.QuestionID = @questionID AND value=@value AND Reply.ReplyAnswer=@answer
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@questionID", questionID);
                    command.Parameters.AddWithValue("@value", value);
                    command.Parameters.AddWithValue("@answer", answer);
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

        public static DataTable GetReplySin(string replyAnswer, string listID, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT * FROM ReplyInfo 
                    WHERE ReplyAnswer LIKE @replyAnswer AND ListID=@listID AND QuestionID=@questionID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@replyAnswer", replyAnswer);
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

        public static DataTable GetReplyMul(string replyAnswer, string listID, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT * FROM ReplyInfo 
                    WHERE ReplyAnswer  LIKE '%{replyAnswer}%' AND ListID=@listID AND QuestionID=@questionID
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@replyAnswer", replyAnswer);
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

        public static DataTable GetInputCount(string listID, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT * FROM ReplyInfo 
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

        public static DataTable GetMulCount(string listID, int questionID)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT * FROM ReplyInfo 
                    CROSS APPLY STRING_SPLIT(ReplyAnswer, ',')
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

        public static void DeleteAllQuestion(string id)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" DELETE [QuestionInfo]
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
