using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;
using Library.Repositories;

namespace Library.Service
{
    public class ArrangingException:Exception
    {
        public ArrangingException()
        {

        }

        public ArrangingException(string message) : base(message)
        {

        }

        public ArrangingException(string message, Exception inner) : base(message, inner)
        {


        }
    }
    public class ArrangingService
    {
        private IBooks Books { get; }
        private IBookReservations Reservations { get; }

        public ArrangingService(IBooks books,IBookReservations reservations)
        {
            Books = books;
            Reservations = reservations;
        }

        public void PutToShelf(string bookID)
        {
            if (Reservations.Exists(bookID)) throw new ArrangingException("予約中の本です");
            if (Books.QueryStatus(bookID) == BookStatus.Rented) throw new ArrangingException("貸出中となっています。");
            Books.UpdateStatus(bookID, BookStatus.OnShelf);

        }
        public void PickToStorage(string bookID)
        {
            if (Books.QueryStatus(bookID) == BookStatus.Rented)
            {
                throw new ArrangingException("貸出中となっています。");
            }

            Books.UpdateStatus(bookID, BookStatus.InStorage);
        }


        public List<string> FindBooksToPickToStorage()
        {
            return Reservations.GetOlderReservations(10)
                .Where(reserve => Books.QueryStatus(reserve.BookID) == BookStatus.OnShelf)
                .Select(reserve=>reserve.BookID)
                .ToList();
        }


        public List<string> FindBooksToPutToShelf()
        {
            return Books.FindBooksInStorage()
                .Where(book => !Reservations.Exists(book.Id))
                .Select(book=>book.Id)
                .ToList();
        }

    }
}
