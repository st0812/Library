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
    public class TestReservingService
    {
        public DummyBooks Library { get; set; }
        public DummyBookReservations Reserves { get; set; }
        public DummyCheckoutHistories History { get; set; }
        public DummyBookReturnAgreements Transactions { get; set; }

        public ReservationService ReservingService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyBookReservations();
            History = new DummyCheckoutHistories();
            Transactions = new DummyBookReturnAgreements();

            //サービスの初期化
            ReservingService = new ReservationService(Reserves,Transactions);


            //データの作成
            Library.Add(new Book("a", BookStatus.OnShelf));//本aを本棚
            Library.Add(new Book("b", BookStatus.Rented));//本bを貸出済み
            Library.Add(new Book("c", BookStatus.InStorage));//本cをバックヤード

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new BookReturnAgreement("userX", "b", time, time + BorrowingService.DefaultLoanSpan));//本bの貸出情報

            Reserves.Add(new BookReservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestReserve()
        {
            ReservingService.ReserveBook("a", "user", DateTime.Parse("2020/03/14"));
            Assert.AreEqual("user", Reserves.Reserves.First(reserve => reserve.BookID == "a").UserID);
        }

        [TestMethod]
        public void TestReserve_Exception()
        {
            Assert.ThrowsException<ReservingException>(
                () => ReservingService.ReserveBook("a", "userX", DateTime.Parse("2020/03/25")));
        }
        [TestMethod]

        public void TestGetResevesBy()
        {
            Assert.AreEqual("c", ReservingService.FindReservationsBy("user2").First().BookID);
            Assert.AreEqual(0, ReservingService.FindReservationsBy("userX").Count());
        }

        [TestMethod]
        public void TestCancel()
        {
            var reserve = Reserves.FindReservationsBy("user2").First();
            ReservingService.CancelReservation(reserve.ReservationId);
            Assert.AreEqual(0, Reserves.FindReservationsOf("c").Count());
        }
       
    }
}
