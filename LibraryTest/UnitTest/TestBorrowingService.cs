using Library.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;
using Library;
using Library.Repositories.Dummy;

namespace LibraryTest
{
    [TestClass]
    public class TestBorrowingService
    {
        public DummyBooks Library { get; set; }
        public DummyBookReservations Reserves { get; set; }
        public DummyCheckoutHistories History { get; set; }
        public DummyBookReturnAgreements Transactions { get; set; }

        public BorrowingService LendingService { get; set; }
       
        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyBookReservations();
            History = new DummyCheckoutHistories();
            Transactions = new DummyBookReturnAgreements();

            //サービスの初期化
            LendingService = new BorrowingService(Library,Transactions);


            //データの作成
            Library.Add(new Book("a", BookStatus.OnShelf));//本aを本棚
            Library.Add(new Book("b", BookStatus.Rented));//本bを貸出済み
            Library.Add(new Book("c", BookStatus.InStorage));//本bを貸出済み

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new BookReturnAgreement("userX", "b", time, time + BorrowingService.DefaultLoanSpan));//本bの貸出情報

            Reserves.Add(new BookReservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestLend()
        {
            LendingService.Borrow("user", "a", DateTime.Parse("2020/04/05"));
            
            
            Assert.AreEqual(BookStatus.Rented, Library.Books.Where(book => book.Id == "a").First().BookStatus);
            
            var tran = Transactions.Transactions.Where(transaction=>transaction.BookID=="a").First();
            Assert.AreEqual("user", tran.UserID);
            Assert.AreEqual("a", tran.BookID);
            Assert.AreEqual(DateTime.Parse("2020/04/05"), tran.CheckoutDate);
            Assert.AreEqual(DateTime.Parse("2020/04/05")+BorrowingService.DefaultLoanSpan, tran.DueDate);
        }

        [TestMethod]
        public void TestGetTransactionsBy()
        {
            var transaction = LendingService.GetReturnAgreementsBy("userX").First();
            Assert.AreEqual("b", transaction.BookID);
        }

        [TestMethod]
        public void TestLend_Exception()
        {
            Assert.ThrowsException<BorrowingException>(()=>LendingService.Borrow("user", "b", DateTime.Parse("2020/04/05")));
        }

        [TestMethod]
        public void TestLend_Exception2()
        {
            Assert.ThrowsException<BorrowingException>(() => LendingService.Borrow("userX", "a", DateTime.Parse("2020/04/05")));
        }

    }
}
