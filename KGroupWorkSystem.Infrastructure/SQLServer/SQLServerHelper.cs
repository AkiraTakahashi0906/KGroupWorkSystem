using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Infrastructure.SQLServer
{
    internal static class SQLServerHelper
    {
        internal readonly static string ConnectionString;
        static SQLServerHelper()
        {
            //pass00000000
            var builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"DESKTOP-8B0KCU1\SQLEXPRESS";
            builder.InitialCatalog = "KGWS";
            builder.IntegratedSecurity = true;
            ConnectionString = builder.ConnectionString;
        }
        internal static void Query(
                                               string sql,
                                               SqlParameter[] parameters,
                                               Action<SqlDataReader> action)
        {
            using (var connection = new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {

                connection.Open();
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        action(reader);
                    }
                }
            }
        }

        internal static void Execute(
                                                  string sql,
                                                  SqlParameter[] parameters)
        {
            using (var connection =
            new SqlConnection(ConnectionString))
            using (var command = new SqlCommand(sql, connection))
            {
                connection.Open();

                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                command.ExecuteNonQuery();
            }
        }
    }
}
