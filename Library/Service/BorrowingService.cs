using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Service
{
    public class LendingException:Exception
    {
        public LendingException()
        {

        }

        public LendingException(string message) : base(message)
        {

        }

        public LendingException(string message, Exception inner):base(message,inner)
        {

        }
    }

    public class BorrowingService
    {
        public static TimeSpan RentalTime { get; } = new TimeSpan(14, 0, 0, 0, 0);
        private IBooks Books { get; }
        private ITransactions Transactions { get; }

        public BorrowingService(IBooks library, ITransactions transactions)
        {
            Books = library;
            Transactions = transactions;
        }

        public void Borrow(string userID, string bookID, DateTime dateTime)
        {
            if (Books.QueryStatus(bookID)!=Status.Shelf && Books.QueryStatus(bookID) != Status.Backyard) throw new LendingException("本が貸出可能な状態ではありません。");
            if (Transactions.FindOverduesBy(userID,dateTime).Count()!=0) throw new LendingException("返却遅延している本があります");
            Transactions.Add(new Transaction(userID, bookID, dateTime, dateTime+RentalTime));
            Books.UpdateStatus(bookID, Status.Rentaled);
        }



        public List<Transaction> GetTransactionsBy(string userID)
        {
            return Transactions.GetTransactionsBy(userID);
        }
    }
}
