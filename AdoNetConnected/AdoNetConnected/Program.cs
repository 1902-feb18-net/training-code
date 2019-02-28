using System;
using System.Data.SqlClient;

namespace AdoNetConnected
{
    class Program
    {
        static void Main(string[] args)
        {
            // ADO.NET
            // originally Active Data Objects, ADO. later ADO.NET
            // ADO.NET is the "brand name"/"namespace" for all .NET data access stuff.
            // but in practice, when we say "ADO.NET", we mean the old fashioned way
            // we're about to see.

            // ADO.NET has things in System.Data namespace.
            // we need a data provider - use NuGet package System.Data.SqlClient
            //    for SQL Server connections.

            // the connection string has all we need to connect to the database.
            var connString = SecretConfiguration.ConnectionString;

            // for connected architecture:

            // (we should also be catching exceptions)
            using (var connection = new SqlConnection(connString))
            {
                // 1. open the connection
                connection.Open();

                // 2. execute query
                var commString = "SELECT * FROM Movie.Movie;";
                using (var command = new SqlCommand(commString, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // we have command.ExecuteReader which returns a DataReader
                        //      for SELECT queries...
                        // we have command.ExecuteNonQuery which just returns number
                        //     of rows affected, for DELETE, INSERT, etc.

                        // 3. process the results.
                        if (reader.HasRows)
                        {
                            // "reader.Read" advances the "cursor" through the results,
                            // one row at a time
                            while (reader.Read())
                            {
                                object id = reader["MovieId"];
                                object title = reader["Title"];
                                string fullTitle = reader.GetString(5); // fifth column

                                // i could do downcasting and instantiate some
                                // Movie class. or just print results

                                Console.WriteLine($"Movie #{id}: {fullTitle}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("no rows returned.");
                        }

                        // 4. close the connection.
                        connection.Close();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
