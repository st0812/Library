using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Service
{
    public class RackingException:Exception
    {
        public RackingException()
        {

        }

        public RackingException(string message) : base(message)
        {

        }

        public RackingException(string message, Exception inner) : base(message, inner)
        {


        }
    }
    public class ArrangingService
    {
        private IBooks Books { get; }
        private IReservations Reservations { get; }

        public ArrangingService(IBooks books,IReservations reservations)
        {
            Books = books;
            Reservations = reservations;
        }

        public void PutToShelf(string bookID)
        {
            if (Reservations.Exists(bookID)) throw new RackingException("予約中の本です");
            if (Books.QueryStatus(bookID) == Status.Rentaled) throw new RackingException("貸出中となっています。");
            Books.UpdateStatus(bookID, Status.Shelf);

        }
        public void PickToBackyard(string bookID)
        {
            if (Books.QueryStatus(bookID) == Status.Rentaled) throw new RackingException("貸出中となっています。");
            Books.UpdateStatus(bookID, Status.Backyard);
        }


        public List<string> GetBooksToPickToBackyard()
        {
            return Reservations.GetPrimeReserves()
                .Where(reserve => Books.QueryStatus(reserve.BookID) == Status.Shelf)
                .Select(reserve=>reserve.BookID)
                .ToList();
        }


        public List<string> GetBooksToPutToShelf()
        {
            return Books.GetBooksInBackYard()
                .Where(book => !Reservations.Exists(book.ID))
                .Select(book=>book.ID)
                .ToList();
        }

    }
}
