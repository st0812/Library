using Library.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Sqlite
{
    public class SqliteBooks : IBooks
    {
        private string _connectionString;

        public SqliteBooks(string connectionString)
        {
            _connectionString = new SqliteConnectionStringBuilder { DataSource = connectionString }.ToString();
            CreateTableIfNotExists();
        }

        private void CreateTableIfNotExists()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("CREATE TABLE IF NOT EXISTS Books (Id TEXT PRIMARY KEY, BookStatus INTEGER)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Add(Book book)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("INSERT INTO Books (Id, BookStatus) VALUES (@Id, @BookStatus)", connection))
                {
                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@BookStatus", (int)book.BookStatus);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStatus(string id, BookStatus status)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("UPDATE Books SET BookStatus = @BookStatus WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@BookStatus", (int)status);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new ArgumentException($"No book found with ID {id}");
                    }
                }
            }
        }

        public BookStatus QueryStatus(string id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT BookStatus FROM Books WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    object result = command.ExecuteScalar();
                    if (result == null)
                    {
                        throw new ArgumentException($"No book found with ID {id}");
                    }

                    return (BookStatus)Convert.ToInt32(result);
                }
            }
        }

        public List<Book> FindBooksInStorage()
        {
            var books = new List<Book>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT Id, BookStatus FROM Books WHERE BookStatus = @BookStatus", connection))
                {
                    command.Parameters.AddWithValue("@BookStatus", (int)BookStatus.InStorage);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book(reader.GetString(0), (BookStatus)reader.GetInt32(1)));
                        }
                    }
                }
            }

            return books;
        }

         public List<Book> FindBooks()
        {
            var books = new List<Book>();
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT Id, BookStatus FROM Books", connection))
                {
                    command.Parameters.AddWithValue("@BookStatus", (int)BookStatus.InStorage);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            books.Add(new Book(reader.GetString(0), (BookStatus)reader.GetInt32(1)));
                        }
                    }
                }
            }

            return books;
        }


    }

}
