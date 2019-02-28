using System;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetDisconnected
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter condition: ");
            var condition = Console.ReadLine();
            var commandString = $"SELECT * FROM Movie.Genre WHERE {condition}";

            // disconnected architecture
            // rather than maximizing the speed of getting the results
            // we want to minimize the time the connection spends open

            // still need NuGet package System.Data.SqlClient (for SQL Server)
            // and using directive "using System.Data;", "using System.Data.SqlClient;"

            // System.Data.DataSet can store data that DataReader gets,
            // with the help of DataAdapter.
            var dataSet = new DataSet();

            var connectionString = SecretConfiguration.ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            {
                // disconnected architecture with ADO.NET

                // step 1. open the connection
                connection.Open();
                using (var command = new SqlCommand(commandString, connection))
                using (var adapter = new SqlDataAdapter(command))
                {
                    // step 2. execute the query, filling the dataset
                    adapter.Fill(dataSet);
                    // (the DataAdapter is internally using DataReader)
                }

                // step 3. close the connection
                connection.Close();
            }

            // step 4. process the results
            // (not while connection is open, to get it closed ASAP)

            // inside DataSet is some DataTables
            // inside DataTable is DataColumn, DataRow
            // inside DataRow is object[]
            // this is non-generic

            // old-fashioned non-generic foreach (based on nongeneric IEnumerable)
            // does implicit downcasting
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                DataColumn idCol = dataSet.Tables[0].Columns["GenreId"];

                Console.WriteLine($"Genre #{row[idCol]}: {row[1]}");
            }

            Console.ReadLine();
        }
    }
}
