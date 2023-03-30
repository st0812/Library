using Library.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Sqlite
{
    public class SqliteBookReturnAgreements : IBookReturnAgreements
    {
        private readonly string _connectionString;

        public SqliteBookReturnAgreements(string connectionString)
        {
            _connectionString = new SqliteConnectionStringBuilder { DataSource = connectionString }.ToString();
            _connectionString = connectionString;
            CreateTableIfNotExists();

        }
        private void CreateTableIfNotExists()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("" +
                    "CREATE TABLE IF NOT EXISTS BookReturnAgreements ( ID TEXT PRIMARY KEY, UserID TEXT NOT NULL,  BookID TEXT NOT NULL,  CheckoutDate DATETIME NOT NULL,DueDate DATETIME NOT NULL)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Add(BookReturnAgreement transaction)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("INSERT INTO BookReturnAgreements (ID, UserID, BookID, CheckoutDate, DueDate) VALUES (@ID, @UserID, @BookID, @CheckoutDate, @DueDate)", connection))
                {
                    command.Parameters.AddWithValue("@ID", transaction.ID);
                    command.Parameters.AddWithValue("@UserID", transaction.UserID);
                    command.Parameters.AddWithValue("@BookID", transaction.BookID);
                    command.Parameters.AddWithValue("@CheckoutDate", transaction.CheckoutDate);
                    command.Parameters.AddWithValue("@DueDate", transaction.DueDate);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string transactionID)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("DELETE FROM BookReturnAgreements WHERE ID = @ID", connection))
                {
                    command.Parameters.AddWithValue("@ID", transactionID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public BookReturnAgreement Get(string bookID)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM BookReturnAgreements WHERE BookID = @BookID",connection);


                command.Parameters.AddWithValue("@BookID", bookID);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    var id = reader.GetString(0);
                    var userId = reader.GetString(1);
                    var bookId = reader.GetString(2);
                    var checkoutDate = reader.GetDateTime(3);
                    var dueDate = reader.GetDateTime(4);

                    return new BookReturnAgreement(id, userId, bookId, checkoutDate, dueDate);
                }

                return null;
            }
        }

        public List<BookReturnAgreement> GetAgreementsBy(string userID)
        {
            var agreements = new List<BookReturnAgreement>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                var command = new SqliteCommand("SELECT * FROM BookReturnAgreements WHERE UserID = @UserID",connection);
                command.Parameters.AddWithValue("@UserID", userID);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var id = reader.GetString(0);
                    var userId = reader.GetString(1);
                    var bookId = reader.GetString(2);
                    var checkoutDate = reader.GetDateTime(3);
                    var dueDate = reader.GetDateTime(4);

                    agreements.Add(new BookReturnAgreement(id, userId, bookId, checkoutDate, dueDate));
                }
            }

            return agreements;
        }

        public List<BookReturnAgreement> FindOverdues(DateTime dateTime)
        {
            List<BookReturnAgreement> agreements = new List<BookReturnAgreement>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT * FROM BookReturnAgreements WHERE DueDate < @dueDate;", connection))
                {
                    command.Parameters.AddWithValue("@dueDate", dateTime);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookReturnAgreement agreement = new BookReturnAgreement(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDateTime(3),
                                reader.GetDateTime(4)
                            );
                            agreements.Add(agreement);
                        }
                    }
                }
            }

            return agreements;
        }

        public List<BookReturnAgreement> FindOverduesBy(string userID, DateTime dateTime)
        {
            List<BookReturnAgreement> agreements = new List<BookReturnAgreement>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (SqliteCommand command = new SqliteCommand("SELECT * FROM BookReturnAgreements WHERE UserID = @userID AND DueDate < @dueDate;", connection))
                {
                    command.Parameters.AddWithValue("@userID", userID);
                    command.Parameters.AddWithValue("@dueDate", dateTime);
                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookReturnAgreement agreement = new BookReturnAgreement(
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDateTime(3),
                                reader.GetDateTime(4)
                            );
                            agreements.Add(agreement);
                        }
                    }
                }
            }

            return agreements;
        }
    }
}
