using Library;
using Library.Model;
using Library.Repositories.Dummy;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTest
{
    [TestClass]
    public class TestArrangingService
    {

        public DummyBooks Library { get; set; }
        public DummyBookReservations Reserves { get; set; }
        public DummyCheckoutHistories History { get; set; }
        public DummyBookReturnAgreements Transactions { get; set; }

        public ArrangingService RackingService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyBookReservations();
            History = new DummyCheckoutHistories();
            Transactions = new DummyBookReturnAgreements();

            //サービスの初期化
            RackingService = new ArrangingService(Library,Reserves);


            //データの作成
            Library.Add(new Book("a", BookStatus.OnShelf));//本aを本棚
            Library.Add(new Book("b", BookStatus.Rented));//本bを貸出済み
            Library.Add(new Book("c", BookStatus.InStorage));//本dをバックヤードに
            Library.Add(new Book("d", BookStatus.InStorage));//本dをバックヤードに
            Library.Add(new Book("e", BookStatus.OnShelf));//本eを本棚

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new BookReturnAgreement("userX", "b", time, time + BorrowingService.DefaultLoanSpan));//本bの貸出情報

            Reserves.Add(new BookReservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
            Reserves.Add(new BookReservation("user2", "e", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestBackToShelve()
        {
            RackingService.PutToShelf("d");
            Assert.AreEqual(BookStatus.OnShelf, Library.Books.Where(book => book.Id == "d").First().BookStatus);
           
        }

        [TestMethod]
        public void TestPick()
        {
            RackingService.PickToStorage("a");
            Assert.AreEqual(BookStatus.InStorage, Library.Books.Where(book => book.Id == "a").First().BookStatus);
        }
        [TestMethod]

        public void TestGetBooksToTakeToBackyard()
        {
            Assert.AreEqual("e",
                RackingService.FindBooksToPickToStorage().First());
        }
        [TestMethod]

        public void TestBooksToTakeToShelves()
        {
            Assert.AreEqual("d",
               RackingService.FindBooksToPutToShelf().First());
        }


        [TestMethod]
        public void TestBackToShelve_Exception()
        {
            Assert.ThrowsException<ArrangingException>(
                () => RackingService.PutToShelf("c")
                );
        }

           [TestMethod]
        public void TestBackToShelve_Exception2()
        {
            Assert.ThrowsException<ArrangingException>(
                () => RackingService.PutToShelf("b")
                );
        }



        [TestMethod]
        public void TestPick_Exception2()
        {
            Assert.ThrowsException<ArrangingException>(
                () => RackingService.PickToStorage("b")
                );
        }

    }
}
