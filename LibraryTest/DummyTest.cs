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

        [TestMethod]
        public void Test()
        {
            var Library = new DummyBooks();
            var Reserves = new DummyBookReservations();
            var History = new DummyCheckoutHistories();
            var Transactions = new DummyBookReturnAgreements();

            var Scenerio = new Scenerio(Library, Reserves, History, Transactions);

            Scenerio.RunSchenerio();
        }

    }
}
