using Library;
using Library.Model;
using Library.Repositories;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace UnitTest
{
    public class Scenerio
    {
        public IBooks Library { get; }
        public RegisteringService RegisterService { get; set; }
        public BorrowingService LendingService { get; set; }
        public ArrangingService RackingService { get; set; }
        public ReservationService ReservingService { get; set; }
        public BookReturnService ReturnAcceptanceService { get; set; }



        public Scenerio(IBooks library, IBookReservations reserves, ICheckoutHistories histories, IBookReturnAgreements agreements)
        {
            //サービスの初期化
            Library = library;
            RegisterService = new RegisteringService(library);
            LendingService = new BorrowingService(library, agreements);
            RackingService = new ArrangingService(library, reserves);
            ReservingService = new ReservationService(reserves, agreements);
            ReturnAcceptanceService = new BookReturnService(library, agreements, histories);
        }

        public void RunSchenerio()
        {
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();

            var Books = Library.FindBooks();

            //現在の貸出数が0
            Assert.AreEqual(0, LendingService.GetReturnAgreementsBy("user").Count());

            var book1 = Books[0];
            var book2 = Books[1];
            var book3 = Books[2];
            var book4 = Books[3];
            var book5 = Books[4];

            LendingService.Borrow("user", book1.Id, DateTime.Parse("2020/04/05"));
            LendingService.Borrow("user", book2.Id, DateTime.Parse("2020/04/05"));
            //貸出数が2
            Assert.AreEqual(2, LendingService.GetReturnAgreementsBy("user").Count());


            //ユーザがBook3を予約
            ReservingService.ReserveBook(book3.Id, "user", DateTime.Parse("2020/04/05"));

            //職員がBook3を確保
            var book = RackingService.FindBooksToPickToStorage().First();
            RackingService.PickToStorage(book);

            //ユーザがBook3を借りる
            LendingService.Borrow("user", book3.Id, DateTime.Parse("2020/04/10"));
            Assert.AreEqual(3, LendingService.GetReturnAgreementsBy("user").Count());


            //ユーザ2がBook4,5を借りる

            LendingService.Borrow("user2", book4.Id, DateTime.Parse("2020/04/15"));
            LendingService.Borrow("user2", book5.Id, DateTime.Parse("2020/04/15"));
            Assert.AreEqual(2, LendingService.GetReturnAgreementsBy("user2").Count());


            //ユーザがBook4を予約しようとするが、Book1とBook2を延滞している
            Assert.ThrowsException<ReservingException>(() => ReservingService.ReserveBook(book4.Id, "user", DateTime.Parse("2020/04/20")));



            //ユーザがBook1,2を返却
            ReturnAcceptanceService.ReturnBook(book1.Id, DateTime.Parse("2020/04/15"));
            ReturnAcceptanceService.ReturnBook(book2.Id, DateTime.Parse("2020/04/15"));
            Assert.AreEqual(1, LendingService.GetReturnAgreementsBy("user").Count());

            //職員がBook1,2を本棚に返却
            Assert.AreEqual(2, RackingService.FindBooksToPutToShelf().Count());
            foreach (var id in RackingService.FindBooksToPutToShelf()) RackingService.PutToShelf(id);
            Assert.AreEqual(0, RackingService.FindBooksToPutToShelf().Count());


            //ユーザがBook4を予約
            ReservingService.ReserveBook(book4.Id, "user", DateTime.Parse("2020/04/21"));

            //ユーザ2がBook4,5を返却
            ReturnAcceptanceService.ReturnBook(book4.Id, DateTime.Parse("2020/04/21"));
            ReturnAcceptanceService.ReturnBook(book5.Id, DateTime.Parse("2020/04/21"));


            //職員がBook5を棚に戻す
            foreach (var id in RackingService.FindBooksToPutToShelf()) RackingService.PutToShelf(id);
            Assert.AreEqual(0, RackingService.FindBooksToPutToShelf().Count());

            //ユーザがBook4を借りる
            LendingService.Borrow("user", book4.Id, DateTime.Parse("2020/04/22"));

            //ユーザ2がBook2,3を予約
            ReservingService.ReserveBook(book2.Id, "user2", DateTime.Parse("2020/04/22"));
            ReservingService.ReserveBook(book3.Id, "user3", DateTime.Parse("2020/04/22"));

            //職員がBook2を確保
            foreach (var id in RackingService.FindBooksToPickToStorage()) RackingService.PickToStorage(id);


            //ユーザが持っている本は1つ Book3, Book4
            Assert.AreEqual(2, LendingService.GetReturnAgreementsBy("user").Count());

            //ユーザ2が持っている本は0つ
            Assert.AreEqual(0, LendingService.GetReturnAgreementsBy("user2").Count());

            //5冊の本のうち、ユーザ2, 本棚2, バックヤード 1
            Assert.AreEqual(1, Library.FindBooks().Where(book => book.BookStatus == BookStatus.InStorage).Count());
            Assert.AreEqual(2, Library.FindBooks().Where(book => book.BookStatus == BookStatus.OnShelf).Count());

            //ユーザ3がBook3の予約をキャンセル
            var reservations=ReservingService.FindReservationsBy("user3");
            Assert.AreEqual(1, reservations.Count());
            ReservingService.CancelReservation(reservations.First().ReservationId);
             reservations = ReservingService.FindReservationsBy("user3");
            Assert.AreEqual(0, reservations.Count());


        }


    }
}
