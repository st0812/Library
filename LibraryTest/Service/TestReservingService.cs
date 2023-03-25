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
    public class TestReservingService
    {
        public DummyBooks Library { get; set; }
        public DummyReservations Reserves { get; set; }
        public DummyHistories History { get; set; }
        public DummyTransactions Transactions { get; set; }

        public ReservingService ReservingService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyReservations();
            History = new DummyHistories();
            Transactions = new DummyTransactions();

            //サービスの初期化
            ReservingService = new ReservingService(Reserves,Transactions);


            //データの作成
            Library.Add(new Book("a", Status.Shelf));//本aを本棚
            Library.Add(new Book("b", Status.Rentaled));//本bを貸出済み
            Library.Add(new Book("c", Status.Backyard));//本cをバックヤード

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new Transaction("userX", "b", time, time + BorrowingService.RentalTime));//本bの貸出情報

            Reserves.Add(new Reservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestReserve()
        {
            ReservingService.Reserve("a", "user", DateTime.Parse("2020/03/14"));
            Assert.AreEqual("user", Reserves.Reserves.First(reserve => reserve.BookID == "a").UserID);
        }

        [TestMethod]
        public void TestReserve_Exception()
        {
            Assert.ThrowsException<ReservingException>(
                () => ReservingService.Reserve("a", "userX", DateTime.Parse("2020/03/25")));
        }
        [TestMethod]

        public void TestGetResevesBy()
        {
            Assert.AreEqual("c", ReservingService.GetReservesBy("user2").First().BookID);
            Assert.AreEqual(0, ReservingService.GetReservesBy("userX").Count());
        }

        [TestMethod]
        public void TestCancel()
        {
            var reserve = Reserves.GetReservesBy("user2").First();
            ReservingService.Cancel(reserve.ID);
            Assert.AreEqual(0, Reserves.GetReserves("c").Count());
        }
       
    }
}
