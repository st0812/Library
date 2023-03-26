using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Service
{
    public class BookReturnService
    {
        private IBooks Books { get; }
        private IBookReturnAgreements Agreements { get; }
        private ICheckoutHistories CheckoutHistories { get; }
        public BookReturnService(IBooks library, IBookReturnAgreements transactions, ICheckoutHistories history)
        {
            Books = library;
            Agreements = transactions;
            CheckoutHistories = history;
        }

        public void ReturnBook(string bookID,DateTime returndate)
        {
            var transaction = Agreements.Get(bookID);
            Agreements.Delete(transaction.ID);
            Books.UpdateStatus(transaction.BookID, BookStatus.InStorage);
            CheckoutHistories.Add(new CheckoutHistory(
                transaction.UserID,
                transaction.BookID,
                transaction.CheckoutDate,returndate));
        }
    }
}
