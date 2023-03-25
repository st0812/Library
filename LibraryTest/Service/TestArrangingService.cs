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
    public class TestArrangingService
    {

        public DummyBooks Library { get; set; }
        public DummyReservations Reserves { get; set; }
        public DummyHistories History { get; set; }
        public DummyTransactions Transactions { get; set; }

        public ArrangingService RackingService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyReservations();
            History = new DummyHistories();
            Transactions = new DummyTransactions();

            //サービスの初期化
            RackingService = new ArrangingService(Library,Reserves);


            //データの作成
            Library.Add(new Book("a", Status.Shelf));//本aを本棚
            Library.Add(new Book("b", Status.Rentaled));//本bを貸出済み
            Library.Add(new Book("c", Status.Backyard));//本dをバックヤードに
            Library.Add(new Book("d", Status.Backyard));//本dをバックヤードに
            Library.Add(new Book("e", Status.Shelf));//本eを本棚

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new Transaction("userX", "b", time, time + BorrowingService.RentalTime));//本bの貸出情報

            Reserves.Add(new Reservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
            Reserves.Add(new Reservation("user2", "e", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestBackToShelve()
        {
            RackingService.PutToShelf("d");
            Assert.AreEqual(Status.Shelf, Library.Books.Where(book => book.ID == "d").First().Status);
           
        }

        [TestMethod]
        public void TestPick()
        {
            RackingService.PickToBackyard("a");
            Assert.AreEqual(Status.Backyard, Library.Books.Where(book => book.ID == "a").First().Status);
        }
        [TestMethod]

        public void TestGetBooksToTakeToBackyard()
        {
            Assert.AreEqual("e",
                RackingService.GetBooksToPickToBackyard().First());
        }
        [TestMethod]

        public void TestBooksToTakeToShelves()
        {
            Assert.AreEqual("d",
               RackingService.GetBooksToPutToShelf().First());
        }


        [TestMethod]
        public void TestBackToShelve_Exception()
        {
            Assert.ThrowsException<RackingException>(
                () => RackingService.PutToShelf("c")
                );
        }

           [TestMethod]
        public void TestBackToShelve_Exception2()
        {
            Assert.ThrowsException<RackingException>(
                () => RackingService.PutToShelf("b")
                );
        }



        [TestMethod]
        public void TestPick_Exception2()
        {
            Assert.ThrowsException<RackingException>(
                () => RackingService.PickToBackyard("b")
                );
        }

    }
}
