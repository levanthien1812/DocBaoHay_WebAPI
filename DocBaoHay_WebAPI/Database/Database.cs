using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DocBaoHay_WebAPI.Database
{
    public class Database
    {
        public static DataTable ReadTable(string StoredProcedureName, Dictionary<string, object> StoredProcedureParameters = null)
        {
            DataTable table = new DataTable();

            string SQLConnectionString = ConfigurationManager.ConnectionStrings["QLDBConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(SQLConnectionString))
            {
                connection.Open();

                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = StoredProcedureName;
                sqlCommand.CommandType = CommandType.StoredProcedure;

                if (StoredProcedureParameters != null)
                {
                    foreach (KeyValuePair<string, object> kvp in StoredProcedureParameters)
                    {
                        if (kvp.Value == null)
                        {
                            sqlCommand.Parameters.AddWithValue("@" + kvp.Key, DBNull.Value);
                        }
                        else
                        {
                            sqlCommand.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                        }
                    }
                }

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(table);
            }
            return table;
        }
    }
}