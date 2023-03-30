using Library.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Sqlite
{
    public class SqliteCheckoutHistories:ICheckoutHistories
    {
        private readonly string _connectionString;

        public SqliteCheckoutHistories(string connectionString)
        {
            _connectionString = new SqliteConnectionStringBuilder { DataSource = connectionString }.ToString();
            _connectionString = connectionString;

            CreateTableIfNotExists();
        }
        private void CreateTableIfNotExists()
        {
            string createTableQuery = @"CREATE TABLE IF NOT EXISTS CheckoutHistories (
                                           HistoryId TEXT PRIMARY KEY,
                                           UserID TEXT NOT NULL,
                                           BookID TEXT NOT NULL,
                                           CheckoutDate TEXT NOT NULL,
                                           ReturnDate TEXT NOT NULL
                                       )";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("" +
                    createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
        public void Add(CheckoutHistory history)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand("INSERT INTO CheckoutHistories (HistoryId, UserID, BookID, CheckoutDate, ReturnDate) VALUES (@HistoryId, @UserID, @BookID, @CheckoutDate, @ReturnDate)", connection))
                {
                    command.Parameters.AddWithValue("@HistoryId", history.HistoryId);
                    command.Parameters.AddWithValue("@UserID", history.UserID);
                    command.Parameters.AddWithValue("@BookID", history.BookID);
                    command.Parameters.AddWithValue("@CheckoutDate", history.CheckoutDate);
                    command.Parameters.AddWithValue("@ReturnDate", history.ReturnDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<CheckoutHistory> FindCheckoutHistoriesBy(string userID)
        {
            var histories = new List<CheckoutHistory>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand("SELECT * FROM CheckoutHistories WHERE UserID=@UserID", connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var history = new CheckoutHistory(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDateTime(3),
                                reader.GetDateTime(4));
                            histories.Add(history);
                        }
                    }
                }
            }

            return histories;
        }
    }
}
