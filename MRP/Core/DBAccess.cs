using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Core
{
    static class DBAccess
    {
        static OleDbConnection connection = null;

        static DBAccess()
        {

            string connString = ConfigurationManager.AppSettings["ConnectionString"];

            connection = new OleDbConnection(connString);
        }

        public static DataTable ExecuteDataTable(string sql)
        {
            try
            {
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(sql, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
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
            catch (Exception ex)
            {
                throw ex;
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
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
