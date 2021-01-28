using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Data.SqlClient;

namespace CS_OneOffBounty_BankService
{
    public class SqlHelper
    {
        public static readonly string SqlConnectionString = System.Configuration.ConfigurationManager.AppSettings["SqlServerConnection"];
        public static readonly string InterfaceUrl = System.Configuration.ConfigurationManager.AppSettings["InterfaceUrl"];
        public static DataSet ExecuteDataset(string commandText)
        {
            return ExecuteDataset(CommandType.Text, commandText);
        }

        public static DataSet ExecuteDataset(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters);
        }
        public static DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return ExecuteDataset(SqlConnectionString, commandType, commandText);
        }

        public static DataSet ExecuteDataset(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteDataset(SqlConnectionString, commandType, commandText, commandParameters);
        }


        public static DataSet ExecuteDataset(string SqlConnectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            using (SqlConnection conn = new SqlConnection(SqlConnectionString))
            {
                DataSet dataset = new DataSet();
                using (SqlCommand cmd = new SqlCommand())
                {
                    PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dataset);
                        return dataset;
                    }
                }
            }
        }


        public static int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText);
        }

        public static int ExecuteNonQuery(string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }
        public static int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(SqlConnectionString, commandType, commandText);
        }
        public static int ExecuteNonQuery(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            return ExecuteNonQuery(SqlConnectionString, commandType, commandText, commandParameters);
        }



        /// <summary>
        /// Execute a database query which does not include a select
        /// </summary>
        /// <param name="connString">Connection string to database</param>
        /// <param name="cmdType">Command type either stored procedure or SQL</param>
        /// <param name="cmdText">Acutall SQL Command</param>
        /// <param name="commandParameters">Parameters to bind to the command</param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string SqlConnectionString, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {

            using (SqlConnection connection = new SqlConnection(SqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    //Prepare the command
                    PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                    //Execute the command
                    int val = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    return val;
                }
            }
        }



        public static int ExecuteNonQuery(SqlTransaction tran, CommandType cmdType, string cmdText, params SqlParameter[] commandParameters)
        {
            SqlCommand cmd = new SqlCommand();
            //Prepare the command   
            PrepareCommand(cmd, tran.Connection, tran, cmdType, cmdText, commandParameters);
            //Execute the command
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }


        /// <summary>
        /// Internal function to prepare a command for execution by the database
        /// </summary>
        /// <param name="cmd">Existing command object</param>
        /// <param name="conn">Database connection object</param>
        /// <param name="trans">Optional transaction object</param>
        /// <param name="cmdType">Command type, e.g. stored procedure</param>
        /// <param name="cmdText">Command test</param>
        /// <param name="commandParameters">Parameters for the command</param>
        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] commandParameters)
        {

            //Open the connection if required
            if (conn.State != ConnectionState.Open)
                conn.Open();

            //Set up the command
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            //Bind it to the transaction if it exists
            if (trans != null)
                cmd.Transaction = trans;

            // Bind the parameters passed in
            if (commandParameters != null)
            {
                foreach (SqlParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }
        }

    }
}