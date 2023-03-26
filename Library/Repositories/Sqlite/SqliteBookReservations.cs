using Library.Model;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Sqlite
{
    public class SqliteBookReservations : IBookReservations
    {
        private string _connectionString;

        public SqliteBookReservations(string connectionString)
        {
            _connectionString = new SqliteConnectionStringBuilder { DataSource = connectionString }.ToString();
            CreateTableIfNotExists();

        }

        private void CreateTableIfNotExists()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Reservations (ReservationId TEXT PRIMARY KEY,UserID TEXT NOT NULL, BookID TEXT NOT NULL, ReservationDate DATETIME NOT NULL)", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }


        public void Add(BookReservation reserve)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("INSERT INTO Reservations (ReservationId, UserID, BookID, ReservationDate) " +
                        "VALUES (@reservationId, @userId, @bookId, @reservationDate)",connection))
                {
                    command.Parameters.AddWithValue("@reservationId", reserve.ReservationId);
                    command.Parameters.AddWithValue("@userId", reserve.UserID);
                    command.Parameters.AddWithValue("@bookId", reserve.BookID);
                    command.Parameters.AddWithValue("@reservationDate", reserve.ReservationDate);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(string reserveID)
        {
            using (var connection = new SqliteConnection(_connectionString)) {
                connection.Open();
                using (var command = new SqliteCommand("DELETE FROM Reservations WHERE ReservationId = @reservationId", connection))
                {
                    command.Parameters.AddWithValue("@reservationId", reserveID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Exists(string bookID)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT COUNT(*) FROM Reservations WHERE BookID = @bookId", connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookID);
                    var result = command.ExecuteScalar();
                    var count = Convert.ToInt32(result);
                    return count > 0;
                }
            }
        }

        public List<BookReservation> FindReservationsOf(string bookID)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT ReservationId, UserID, BookID, ReservationDate FROM Reservations WHERE BookID = @bookId", connection))
                {
                    command.Parameters.AddWithValue("@bookId", bookID);
                    using var reader = command.ExecuteReader();
                    var reservations = new List<BookReservation>();
                    while (reader.Read())
                    {
                        var reservation = new BookReservation(
                            reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3)
                        );
                        reservations.Add(reservation);
                    }
                    return reservations;
                }
            }
        }

        public List<BookReservation> FindReservationsBy(string userID)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqliteCommand("SELECT ReservationId, UserID, BookID, ReservationDate FROM Reservations WHERE UserID = @userId",connection))
                {
                    command.Parameters.AddWithValue("@userId", userID);
                    using var reader = command.ExecuteReader();
                    var reservations = new List<BookReservation>();
                    while (reader.Read())
                    {
                        var reservation = new BookReservation(
                            reader.GetString(0),
                            reader.GetString(1),
                            reader.GetString(2),
                            reader.GetDateTime(3)
                        );
                        reservations.Add(reservation);
                    }
                    return reservations;
                }
            }
        }
        public List<BookReservation> GetOlderReservations(int limit)
        {
            List<BookReservation> reservations = new List<BookReservation>();

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Reservations ORDER BY ReservationDate LIMIT @Limit";
                using (SqliteCommand command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Limit", limit);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            BookReservation reservation = new BookReservation(
                                reader.GetString(reader.GetOrdinal("ReservationId")),
                                reader.GetString(reader.GetOrdinal("UserID")),
                                reader.GetString(reader.GetOrdinal("BookID")),
                                reader.GetDateTime(reader.GetOrdinal("ReservationDate"))
                            );

                            reservations.Add(reservation);
                        }
                    }
                }
            }

            return reservations;
        }

        public BookReservation Find(string reserveID)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var command = new SqliteCommand("SELECT * FROM Reservations WHERE ReservationId = @ReservationId", connection);
                command.Parameters.AddWithValue("@ReservationId", reserveID);

                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                reader.Read();

                return new BookReservation(
                    reader["ReservationId"].ToString(),
                    reader["UserID"].ToString(),
                    reader["BookID"].ToString(),
                    DateTime.Parse(reader["ReservationDate"].ToString()));
            }
        }
    }
}
