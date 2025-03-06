using System;
using System.Data.SqlClient;
using System.IO;
using System.Net;

class VulnerableApp
{
    static void Main()
    {
        // Hardcoded credentials (CWE-798)
        string username = "admin";
        string password = "password123";

        // SQL Injection (CWE-89)
        Console.Write("Enter User ID: ");
        string userId = Console.ReadLine();
        string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
        string query = "SELECT * FROM Users WHERE UserID = '" + userId + "'"; // Unsafe concatenation

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["Username"]);
            }
        }

        // Insecure File Handling (CWE-22)
        Console.Write("Enter filename: ");
        string fileName = Console.ReadLine();
        string filePath = "/var/logs/" + fileName; // Path traversal risk
        string fileContent = File.ReadAllText(filePath);
        Console.WriteLine(fileContent);

        // Insecure HTTP Request (CWE-200)
        WebClient client = new WebClient();
        string data = client.DownloadString("http://example.com/secret-data"); // HTTP instead of HTTPS
        Console.WriteLine(data);
    }
}
