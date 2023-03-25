using Library;
using Library.Model;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTest
{
    [TestClass]
    public class TestReturningService
    {
        public DummyBooks Library { get; set; }
        public DummyReservations Reserves { get; set; }
        public DummyHistories History { get; set; }
        public DummyTransactions Transactions { get; set; }

        public ReturningService ReturnAcceptanceService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyReservations();
            History = new DummyHistories();
            Transactions = new DummyTransactions();

            //サービスの初期化
            ReturnAcceptanceService = new ReturningService(Library, Transactions, History);


            //データの作成
            Library.Add(new Book("a", Status.Shelf));//本aを本棚
            Library.Add(new Book("b", Status.Rentaled));//本bを貸出済み
            Library.Add(new Book("c", Status.Backyard));//本cをバックヤード

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new Transaction("userX", "b", time, time + BorrowingService.RentalTime));//本bの貸出情報

            Reserves.Add(new Reservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestReturn()
        {
            ReturnAcceptanceService.Return("b");

            Assert.AreEqual(0, Transactions.Transactions.Where(tran => tran.BookID == "b").Count());
            Assert.AreEqual(Status.Backyard,Library.Books.Where(book=>book.ID=="b").First().Status);
            var history = History.Items.Where(item => item.BookID == "b").First();
            Assert.AreEqual("userX", history.UserID);
            Assert.AreEqual(DateTime.Parse("2020/03/10"), history.CheckOutDate);
        }
       
    }
}
