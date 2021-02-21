using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using TestBackendUser.CrossCutting;

namespace TestBackendUser.Infra
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenConnection()
        {
            var connection = DataBaseConfiguration();          

            if (connection.State != System.Data.ConnectionState.Open)
                connection.Open();

            return connection;
        }

        private static SqliteConnection DataBaseConfiguration()
        {
            string filename = @"TestBackendUser.DataBase.db";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + filename;
            string connectionString = ConnectionStrings.UserConnectionString.Replace("@path", $"{filePath}");
            if (!File.Exists(filePath))
            { 
                var connection = new SqliteConnection(connectionString);
                connection.Open();               

                connection.Execute(@"CREATE TABLE [Usuario] (
                                      [Id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
                                    , [Nome] varchar  NULL
                                    , [Email] varchar  NULL
                                    , [Senha] varchar  NULL
                                    )");
                connection.Close();
                return connection;
            }
            else
            {
                return new SqliteConnection(connectionString);
            }

        }
    }
}
