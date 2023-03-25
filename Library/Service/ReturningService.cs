using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Service
{
    public class ReturningService
    {
        private IBooks Books { get; }
        private ITransactions Transactions { get; }
        private IHistories Histories { get; }
        public ReturningService(IBooks library, ITransactions transactions, IHistories history)
        {
            Books = library;
            Transactions = transactions;
            Histories = history;
        }

        public void Return(string bookID)
        {
            var transaction = Transactions.Get(bookID);
            Transactions.Delete(transaction.ID);
            Books.UpdateStatus(transaction.BookID, Status.Backyard);
            Histories.Add(new History(transaction.UserID,transaction.BookID,transaction.CheckOutDate,DateTime.Now));
        }
    }
}
