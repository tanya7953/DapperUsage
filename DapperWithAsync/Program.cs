using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;

namespace DapperExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var connectionString = "Data Source=LAPTOP-G4RIVN4A\\SSMS01;Database=Dapper;Trusted_Connection=True;";

            var startTime = DateTime.Now;
            Console.WriteLine($"[Async] Start Time: {startTime}");

            // Start multiple insertion tasks without waiting for each to complete
            var tasks = new Task[10];  // Adjust the number to your needs
            for (int i = 1; i <= 10; i++)
            {
                int index = i;  // Avoid closure issue
                tasks[i - 1] = Task.Run(() => InsertDataAsync(connectionString, index));
            }

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);

            var endTime = DateTime.Now;
            Console.WriteLine($"[Async] End Time: {endTime}");
            Console.WriteLine($"[Async] Duration: {endTime - startTime}");

            Console.WriteLine("Asynchronous entries have been inserted into the database.");
        }

        static async Task InsertDataAsync(string connectionString, int i)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var user = new User { Id = i, Name = "Tanya 0" + i };
                var insertQuery = "INSERT INTO UserData2 (Id, Name) VALUES (@Id, @Name)";

                Console.WriteLine($"[Async] Inserting: Id={user.Id}, Name={user.Name}");
                await connection.ExecuteAsync(insertQuery, user);
                Console.WriteLine($"[Async] Inserted: Id={user.Id}, Name={user.Name}");
            }
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
