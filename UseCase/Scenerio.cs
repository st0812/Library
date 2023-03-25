using Library;
using Library.Model;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCase
{
    [TestClass]
    public class Scenerio
    {
        public TestLibrary Library { get; set; }
        public TestReserves Reserves { get; set; }
        public TestTransactionHistory History { get; set; }
        public TestTransactions Transactions { get; set; }

        public RegisterBookService RegisterService { get; set; }
        public LendingService LendingService { get; set; }
        public RackingService RackingService { get; set; }
        public ReservingService ReservingService { get; set; }
        public ReturnAcceptanceService ReturnAcceptanceService { get; set; }



        public List<Book> Books { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            //データの初期化
            Library = new TestLibrary();
            Reserves = new TestReserves();
            History = new TestTransactionHistory();
            Transactions = new TestTransactions();

            //サービスの初期化
            RegisterService = new RegisterBookService(Library);
            LendingService = new LendingService(Library, Transactions);
            RackingService = new RackingService(Library, Reserves);
            ReservingService = new ReservingService(Reserves, Transactions);
            ReturnAcceptanceService = new ReturnAcceptanceService(Library, Transactions, History);


            //データの作成
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();
            RegisterService.RegisterBook();

            Books = Library.Books;;
        }


        [TestMethod]
        public void Test()
        {
            //現在の貸出数が0
            Assert.AreEqual(0, LendingService.GetTransactionsBy("user").Count());

            var book1 = Books[0];
            var book2 = Books[1];
            var book3 = Books[2];
            var book4 = Books[3];
            var book5 = Books[4];

            LendingService.Lend("user", book1.ID, DateTime.Parse("2020/04/05"));
            LendingService.Lend("user", book2.ID, DateTime.Parse("2020/04/05"));
            //貸出数が2
            Assert.AreEqual(2, LendingService.GetTransactionsBy("user").Count());


            //ユーザがBook3を予約
            ReservingService.Reserve(book3.ID, "user",DateTime.Parse("2020/04/05"));

            //職員がBook3を確保
            var book=RackingService.GetBooksToTakeToBackyard().First();
            RackingService.Pick(book);

            //ユーザがBook3を借りる
            LendingService.Lend("user", book3.ID, DateTime.Parse("2020/04/10"));
            Assert.AreEqual(3, LendingService.GetTransactionsBy("user").Count());


            //ユーザ2がBook4,5を借りる

            LendingService.Lend("user2", book4.ID, DateTime.Parse("2020/04/15"));
            LendingService.Lend("user2", book5.ID, DateTime.Parse("2020/04/15"));
            Assert.AreEqual(2, LendingService.GetTransactionsBy("user2").Count());


            //ユーザがBook4を予約しようとするが、Book1とBook2を延滞している
            Assert.ThrowsException<ReservingException>(()=>ReservingService.Reserve(book4.ID, "user", DateTime.Parse("2020/04/20")));



            //ユーザがBook1,2を返却
            ReturnAcceptanceService.Return(book1.ID);
            ReturnAcceptanceService.Return(book2.ID);
            Assert.AreEqual(1, LendingService.GetTransactionsBy("user").Count());

            //職員がBook1,2を本棚に返却
            Assert.AreEqual(2,RackingService.GetBooksToTakeToShelves().Count());
            foreach (var id in RackingService.GetBooksToTakeToShelves()) RackingService.BackToShelve(id);
            Assert.AreEqual(0, RackingService.GetBooksToTakeToShelves().Count());


            //ユーザがBook4を予約
            ReservingService.Reserve(book4.ID, "user", DateTime.Parse("2020/04/21"));

            //ユーザ2がBook4,5を返却
            ReturnAcceptanceService.Return(book4.ID);
            ReturnAcceptanceService.Return(book5.ID);


            //職員がBook5を棚に戻す
            foreach (var id in RackingService.GetBooksToTakeToShelves()) RackingService.BackToShelve(id);
            Assert.AreEqual(0, RackingService.GetBooksToTakeToShelves().Count());

            //ユーザがBook4を借りる
            LendingService.Lend("user", book4.ID, DateTime.Parse("2020/04/22"));

            //ユーザ2がBook2,3を予約
            ReservingService.Reserve(book2.ID, "user2", DateTime.Parse("2020/04/22"));
            ReservingService.Reserve(book3.ID, "user3", DateTime.Parse("2020/04/22"));

            //職員がBook2を確保
            foreach (var id in RackingService.GetBooksToTakeToBackyard()) RackingService.Pick(id);


            //ユーザが持っている本は1つ Book3, Book4
            Assert.AreEqual(2, LendingService.GetTransactionsBy("user").Count());

            //ユーザ2が持っている本は0つ
            Assert.AreEqual(0, LendingService.GetTransactionsBy("user2").Count());

            //5冊の本のうち、ユーザ2, 本棚2, バックヤード 1
            Assert.AreEqual(1,Library.Books.Where(book=>book.Status==Status.Backyard).Count());
            Assert.AreEqual(2,Library.Books.Where(book=>book.Status==Status.Shelf).Count());



        }

    }
}
