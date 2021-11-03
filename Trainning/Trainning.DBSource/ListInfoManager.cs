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
    public class ListInfoManager
    {
        private static string GetConnectionString()
        {
            string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return val;
        }

        public static DataTable GetListInfo()
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT ListID, ID, ListName, Status, StartTime, EndTime
                    FROM ListInfo
                    ORDER BY ListID DESC
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

        public static DataTable SearchDate(string startTime, string endTime)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT*
                    FROM ListInfo
                    WHERE StartTime>=@startTime AND EndTime<=@endTime
                    ORDER BY ListID ASC
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@startTime", startTime);
                    command.Parameters.AddWithValue("@endTime", endTime);
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

        public static DataTable SearchDateStart(string startTime)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT*
                    FROM ListInfo
                    WHERE StartTime>=@startTime
                    ORDER BY ListID ASC
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@startTime", startTime);
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

        public static DataTable SearchDateEnd(string endTime)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT*
                    FROM ListInfo
                    WHERE EndTime<=@endTime
                    ORDER BY ListID ASC
                ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@endTime", endTime);
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
        public static DataRow GetListInfoByID(string id)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" SELECT ListID, ListName, ListContent, Status, StartTime, Endtime
                    FROM ListInfo
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

        public static void CreateList(string listName, string listContent, string status, string startTime, string endTime)
        {
            string connectionString = GetConnectionString();
            string dbCommandString =
                $@" INSERT INTO ListInfo
                    (
                        ListName
                        ,ListContent
                        ,Status
                        ,StartTime
                        ,EndTime
                    )
                    VALUES
                    (
                        @listName
                        ,@listContent
                        ,@status
                        ,@startTime
                        ,@endTime
                    ) ";


            // connect db & execute
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(dbCommandString, connection))
                {
                    command.Parameters.AddWithValue("@listName", listName);
                    command.Parameters.AddWithValue("@listContent", listContent);
                    command.Parameters.AddWithValue("@status", status);
                    command.Parameters.AddWithValue("@startTime", startTime);
                    command.Parameters.AddWithValue("@endTime", endTime);

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
