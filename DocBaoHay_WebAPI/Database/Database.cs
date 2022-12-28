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

        public static object ExecuteCommand(string StoredProcedureName, Dictionary<string, object> StoredProcedureParameters = null, int OutputType = 1)
        {
            try
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
                    // Set @Result as an output parameter in all procedures
                    // Output is integer
                    if (OutputType == 1)
                    {
                        sqlCommand.Parameters.Add("@Result", SqlDbType.Int).Direction = ParameterDirection.Output;
                    } 
                    // Output is string
                    else if (OutputType == 2)
                    {
                        sqlCommand.Parameters.Add("@Result", SqlDbType.NVarChar, -1).Direction = ParameterDirection.Output;
                    }

                    sqlCommand.ExecuteNonQuery();
                    // Get the value of @Result ouput
                    object result = sqlCommand.Parameters["@Result"].Value;
                    // Execute command successfully
                    return result;
                }
            } catch {
                // Fail to execute command
                return null;
            }
        }
    }
}