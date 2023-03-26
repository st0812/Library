using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IBookReservations
    {
        void Add(BookReservation reserve);
        void Delete(string reserveID);
        bool Exists(string bookID);
        List<BookReservation> GetReserves(string bookID);

        List<BookReservation> FindReservationsBy(string userID);
        List<BookReservation> GetPrimeReserves();
        BookReservation Get(string reserveID);
    }
}
