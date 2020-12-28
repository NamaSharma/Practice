using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ATmV2
{
    public class SQLOperations
    {
        public readonly string ConnectionString;

        public SQLOperations()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ATM"].ConnectionString;
        }

        public int ExecuteStoreProcScalar(string procName, SqlParameter[] sqlParameterCollection)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(procName, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var sqlParameter in sqlParameterCollection)
                {
                    command.Parameters.Add(sqlParameter);
                }
                connection.Open();
                rowsAffected = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            return rowsAffected;
        }

        public int ExecuteStoreProcNonQuery(string procName, SqlParameter[] sqlParameterCollection)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(procName, connection);
                command.CommandType = CommandType.StoredProcedure;
                foreach (var sqlParameter in sqlParameterCollection)
                {
                    command.Parameters.Add(sqlParameter);
                }
                connection.Open();
                rowsAffected = Convert.ToInt32(command.ExecuteNonQuery());
                connection.Close();
            }
            return rowsAffected;
        }
        public SqlDataReader ExecuteStoreProcReader(string procName, SqlParameter[] sqlParameterCollection)
        {
            SqlDataReader dr;
            SqlConnection connection = new SqlConnection(ConnectionString);

            SqlCommand command = new SqlCommand(procName, connection);
            command.CommandType = CommandType.StoredProcedure;
            foreach (var sqlParameter in sqlParameterCollection)
            {
                command.Parameters.Add(sqlParameter);
            }
            connection.Open();
            dr = command.ExecuteReader();

            return dr;
        }
    }
}
