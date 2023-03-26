// See https://aka.ms/new-console-template for more information
using Microsoft.Data.Sqlite;

internal class Program
{
    private static void Main(string[] args)
    {
        
            var sqlConnectionSb = new SqliteConnectionStringBuilder { DataSource = ":memory:" };

        using (var cn = new SqliteConnection(sqlConnectionSb.ToString()))
        {
            cn.Open();

            using (var cmd = new SqliteCommand("select sqlite_version()",cn))
            {
                Console.WriteLine(cmd.ExecuteScalar());
            }
        }
    }
}