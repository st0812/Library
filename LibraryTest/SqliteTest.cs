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
        public IBooks Library { get; set; }
        public IBookReservations Reserves { get; set; }
        public ICheckoutHistories History { get; set; }
        public IBookReturnAgreements Transactions { get; set; }

        public Scenerio Scenerio { get; set; }


        private static string DateBasePath { get; set; }

        [TestInitialize]

        public void Initialize()
        {
            //データの初期化
            DateBasePath = Guid.NewGuid().ToString() + ".dB";

            Library = new SqliteBooks(DateBasePath);
            Reserves = new SqliteBookReservations(DateBasePath);
            History = new SqliteCheckoutHistories(DateBasePath);
            Transactions = new SqliteBookReturnAgreements(DateBasePath);

            Scenerio = new Scenerio(Library, Reserves, History, Transactions);

        
        }


        [TestMethod]
        public void Test()
        {
            Scenerio.RunSchenerio();

        }

        [ClassCleanup]
        public static void Cleanup()
        {
            File.Delete(DateBasePath);
        }

    }
}
