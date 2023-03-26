using Library;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DummyReservations : IBookReservations
    {
        public List<BookReservation> Reserves { get; set; } = new List<BookReservation>();
      
        public void Add(BookReservation reserve)
        {
            Reserves.Add(reserve);
        }

        public void Delete(string reserveID)
        {
            Reserves.Remove(Get(reserveID));
        }

        public bool Exists(string bookID)
        {
            return Reserves.Where(reserve => reserve.BookID == bookID).Count() != 0;
        }

        public BookReservation Get(string reserveID)
        {
            return Reserves.Where(reserve => reserve.ReservationId == reserveID).First();
        }

        public List<BookReservation> GetPrimeReserves()
        {
            return new List<BookReservation>(Reserves);
        }

        public List<BookReservation> GetReserves(string bookID)
        {
            return Reserves.Where(reserve => reserve.BookID == bookID).ToList();
        }

        public List<BookReservation> FindReservationsBy(string userID)
        {
            return Reserves.Where(reserve => reserve.UserID==userID).ToList();

        }

    }
}
