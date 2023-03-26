using Library;
using Library.Model;
using Library.Repositories;
using Library.Repositories.Dummy;
using Library.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTest;

namespace LibraryTest
{
    [TestClass]
    public class DummyTest
    {
        public IBooks Library { get; set; }
        public IBookReservations Reserves { get; set; }
        public ICheckoutHistories History { get; set; }
        public IBookReturnAgreements Transactions { get; set; }

        public Scenerio Scenerio { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            //データの初期化
            Library = new DummyBooks();
            Reserves = new DummyBookReservations();
            History = new DummyCheckoutHistories();
            Transactions = new DummyBookReturnAgreements();

            Scenerio = new Scenerio(Library, Reserves, History, Transactions);
          
        }


        [TestMethod]
        public void Test()
        {
            Scenerio.RunSchenerio();
        }

    }
}
