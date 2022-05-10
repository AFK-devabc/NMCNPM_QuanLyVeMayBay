using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace NNCNPM_QuanLyVeMayBay
{
    static class SQLHandler
    {
        private static string strConnection = ConfigurationManager.ConnectionStrings["stringDatabase"].ConnectionString;
        private static SqlConnection sqlConnection = new SqlConnection(strConnection);
        private static SqlCommand sqlCommand;
        private static SqlDataAdapter sqlAdapter = new SqlDataAdapter();

        public static DataTable GetDataTableCommand(string command)
        {
            sqlConnection.Open();
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = command;
            sqlAdapter.SelectCommand = sqlCommand;

            DataTable table = new DataTable();
            table.Clear();
            try
            {
                sqlAdapter.Fill(table);
            }
            catch { }
            sqlConnection.Close();

            return table;
        }

        public static void ExecuteCommand(string command)
        {
            sqlConnection.Open();
            sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = command;

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}
