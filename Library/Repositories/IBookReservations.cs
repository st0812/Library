using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories
{
    public interface IBookReservations
    {
        void Add(BookReservation reserve);
        void Delete(string reserveID);
        bool Exists(string bookID);
        List<BookReservation> FindReservationsOf(string bookID);

        List<BookReservation> FindReservationsBy(string userID);
        List<BookReservation> GetOlderReservations(int limit);
        BookReservation Find(string reserveID);
    }
}
