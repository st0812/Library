﻿using Library.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;
using Library;

namespace LibraryTest
{
    [TestClass]
    public class TestBorrowingService
    {
        public DummyBooks Library { get; set; }
        public DummyReservations Reserves { get; set; }
        public DummyHistories History { get; set; }
        public DummyTransactions Transactions { get; set; }

        public BorrowingService LendingService { get; set; }
       
        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyReservations();
            History = new DummyHistories();
            Transactions = new DummyTransactions();

            //サービスの初期化
            LendingService = new BorrowingService(Library,Transactions);


            //データの作成
            Library.Add(new Book("a", Status.Shelf));//本aを本棚
            Library.Add(new Book("b", Status.Rentaled));//本bを貸出済み
            Library.Add(new Book("c", Status.Backyard));//本bを貸出済み

            var time = DateTime.Parse("2020/03/10");
            Transactions.Add(new Transaction("userX", "b", time, time + BorrowingService.RentalTime));//本bの貸出情報

            Reserves.Add(new Reservation("user2", "c", DateTime.Parse("2020/03/20")));//本cの予約
        }

        [TestMethod]
        public void TestLend()
        {
            LendingService.Borrow("user", "a", DateTime.Parse("2020/04/05"));
            
            
            Assert.AreEqual(Status.Rentaled, Library.Books.Where(book => book.ID == "a").First().Status);
            
            var tran = Transactions.Transactions.Where(transaction=>transaction.BookID=="a").First();
            Assert.AreEqual("user", tran.UserID);
            Assert.AreEqual("a", tran.BookID);
            Assert.AreEqual(DateTime.Parse("2020/04/05"), tran.CheckOutDate);
            Assert.AreEqual(DateTime.Parse("2020/04/05")+BorrowingService.RentalTime, tran.DueDate);
        }

        [TestMethod]
        public void TestGetTransactionsBy()
        {
            var transaction = LendingService.GetTransactionsBy("userX").First();
            Assert.AreEqual("b", transaction.BookID);
        }

        [TestMethod]
        public void TestLend_Exception()
        {
            Assert.ThrowsException<LendingException>(()=>LendingService.Borrow("user", "b", DateTime.Parse("2020/04/05")));
        }

        [TestMethod]
        public void TestLend_Exception2()
        {
            Assert.ThrowsException<LendingException>(() => LendingService.Borrow("userX", "a", DateTime.Parse("2020/04/05")));
        }

    }
}