using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;

namespace MRP.Core
{
    static class DbAccess
    {
        static OleDbConnection connection;

        static DbAccess()
        {

            string connString = ConfigurationManager.AppSettings["ConnectionString"];

            connection = new OleDbConnection(connString);
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            try
            {
                connection.Open();
                var adapter = new OleDbDataAdapter(sql, connection);
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
            finally
            {
                connection.Close(); 
            }
        }

        public static void ExecuteNonQuery(string sql)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sql, connection);
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        
        public static int ExecuteScalar(string sql)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sql, connection);
                object result = command.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            finally
            {
                connection.Close();
            }
        }

        public static string ExecuteReader(string sql)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(sql, connection);
                OleDbDataReader reader = command.ExecuteReader();
                string result = null;
                if (reader.Read())
                    result = Convert.ToString(reader[0]);
                return result;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool TestConect()
        {
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
