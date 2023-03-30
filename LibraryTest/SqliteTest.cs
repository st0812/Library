using Library;
using Library.Model;
using Library.Repositories;
using Library.Repositories.Sqlite;
using Library.Service;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest;

namespace LibraryTest
{
    [TestClass]
    public class SqliteTest
    {

        const string connectionString = "Data Source=InMemory;Mode=Memory;Cache=Shared";
       
        [TestMethod]
        public void Test()
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            var createCommand = connection.CreateCommand();
            createCommand.CommandText = ""
    ;
            createCommand.ExecuteNonQuery();

            var Library = new SqliteBooks(connectionString);
            var Reserves = new SqliteBookReservations(connectionString);
            var History = new SqliteCheckoutHistories(connectionString);
            var Transactions = new SqliteBookReturnAgreements(connectionString);

            var Scenerio = new Scenerio(Library, Reserves, History, Transactions);

            Scenerio.RunSchenerio();

        }

    }
}
