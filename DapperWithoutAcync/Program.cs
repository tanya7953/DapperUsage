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

            var startTime = DateTime.Now;
            Console.WriteLine($"[Sync] Start Time: {startTime}");

            InsertData(connectionString);

            var endTime = DateTime.Now;
            Console.WriteLine($"[Sync] End Time: {endTime}");
            Console.WriteLine($"[Sync] Duration: {endTime - startTime}");

            Console.WriteLine("Synchronous entries have been inserted into the database.");
        }

        static void InsertData(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 1; i <= 10; i++) // Adjust the number to your needs
                {
                    var user = new User { Id = i, Name = "Tanya 0" + i };
                    var insertQuery = "INSERT INTO UserData3 (Id, Name) VALUES (@Id, @Name)";

                    Console.WriteLine($"[Sync] Inserting: Id={user.Id}, Name={user.Name}");
                    connection.Execute(insertQuery, user);
                    Console.WriteLine($"[Sync] Inserted: Id={user.Id}, Name={user.Name}");
                }
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
