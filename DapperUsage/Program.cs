using System;
using System.Data.SqlClient;
using Dapper;

namespace DapperExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=LAPTOP-G4RIVN4A\\SSMS01;Database=Dapper;Trusted_Connection=True;";

            InsertData(connectionString);

            Console.WriteLine(" entries have been inserted into the database.");
        }

        static void InsertData(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 1; i <= 20000; i++)
                {
                    var user = new User { Id = i, Name = "Tanya 0" + i };
                    var insertQuery = "INSERT INTO UserData (Id, Name) VALUES (@Id, @Name)";
                    connection.Execute(insertQuery, user);
                }
            }
            Console.WriteLine("data inserted1");
            Console.WriteLine("data inserted2");
            Console.WriteLine("data inserted3");
            Console.WriteLine("data inserted4");
            Console.WriteLine("data inserted5");
            Console.WriteLine("data inserted6");
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
