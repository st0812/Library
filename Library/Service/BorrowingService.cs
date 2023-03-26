using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;
using Library.Repositories;

namespace Library.Service
{
    public class BorrowingException:Exception
    {
        public BorrowingException()
        {

        }

        public BorrowingException(string message) : base(message)
        {

        }

        public BorrowingException(string message, Exception inner):base(message,inner)
        {

        }
    }

    public class BorrowingService
    {
        public static TimeSpan DefaultLoanSpan { get; } = new TimeSpan(14, 0, 0, 0, 0);
        private IBooks Books { get; }
        private IBookReturnAgreements Transactions { get; }

        public BorrowingService(IBooks library, IBookReturnAgreements transactions)
        {
            Books = library;
            Transactions = transactions;
        }

        public void Borrow(string userID, string bookID, DateTime dateTime,TimeSpan loadSpan)
        {
            if (Books.QueryStatus(bookID)!=BookStatus.OnShelf && Books.QueryStatus(bookID) != BookStatus.InStorage) throw new BorrowingException("本が貸出可能な状態ではありません。");
            if (Transactions.FindOverduesBy(userID,dateTime).Any()) throw new BorrowingException("返却遅延している本があります");
            Transactions.Add(new BookReturnAgreement(userID, bookID, dateTime, dateTime+loadSpan));
            Books.UpdateStatus(bookID, BookStatus.Rented);
        }

        public void Borrow(string userID, string bookID, DateTime dateTime)
        {
            Borrow(userID, bookID, dateTime, DefaultLoanSpan);
        }




        public List<BookReturnAgreement> GetReturnAgreementsBy(string userID)
        {
            return Transactions.GetAgreementsBy(userID);
        }
    }
}
