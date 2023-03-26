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

        public BookReturnService ReturnAcceptanceService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyReservations();
            History = new DummyHistories();
            Transactions = new DummyTransactions();

            //サービスの初期化
            ReturnAcceptanceService = new BookReturnService(Library, Transactions, History);


            //データの作成
            Library.Add(new Book("a", BookStatus.OnShelf));//本aを本棚
            Library.Add(new Book("b", BookStatus.Rented));//本bを貸出済み
            Library.Add(new Book("c", BookStatus.InStorage));//本cをバックヤード

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new BookReturnAgreement("userX", "b", time, time + BorrowingService.DefaultLoanSpan));//本bの貸出情報

            Reserves.Add(new BookReservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestReturn()
        {
            ReturnAcceptanceService.ReturnBook("b", DateTime.Now);

            Assert.AreEqual(0, Transactions.Transactions.Where(tran => tran.BookID == "b").Count());
            Assert.AreEqual(BookStatus.InStorage,Library.Books.Where(book=>book.Id=="b").First().BookStatus);
            var history = History.Items.Where(item => item.BookID == "b").First();
            Assert.AreEqual("userX", history.UserID);
            Assert.AreEqual(DateTime.Parse("2020/03/10"), history.CheckoutDate);
        }
       
    }
}
