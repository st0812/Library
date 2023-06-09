﻿using Library.Model;
using Library.Service;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Repositories.Dummy;

namespace UnitTest.ServiceUnitTest
{
    [TestClass]
    public class TestRegisteringService
    {
        public DummyBooks Library { get; set; }
        public DummyBookReservations Reserves { get; set; }
        public DummyCheckoutHistories History { get; set; }
        public DummyBookReturnAgreements Transactions { get; set; }

        public RegisteringService RegisterBookService { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyBookReservations();
            History = new DummyCheckoutHistories();
            Transactions = new DummyBookReturnAgreements();

            //サービスの初期化
            RegisterBookService = new RegisteringService(Library);


            //データの作成
            Library.Add(new Book("a", BookStatus.OnShelf));//本aを本棚
            Library.Add(new Book("b", BookStatus.Rented));//本bを貸出済み
            Library.Add(new Book("c", BookStatus.InStorage));//本bを貸出済み

        }

        [TestMethod]
        public void TestLend()
        {
            RegisterBookService.RegisterBook();
            Assert.AreEqual(4, Library.Books.Count());

        }

    }
}
