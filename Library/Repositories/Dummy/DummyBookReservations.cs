using Library;
using Library.Model;
using Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Repositories.Dummy
{
    public class DummyBookReservations : IBookReservations
    {
        public List<BookReservation> Reserves { get; set; } = new List<BookReservation>();
      
        public void Add(BookReservation reserve)
        {
            Reserves.Add(reserve);
        }

        public void Delete(string reserveID)
        {
            Reserves.Remove(Find(reserveID));
        }

        public bool Exists(string bookID)
        {
            return Reserves.Where(reserve => reserve.BookID == bookID).Count() != 0;
        }

        public BookReservation Find(string reserveID)
        {
            return Reserves.Where(reserve => reserve.ReservationId == reserveID).First();
        }

        public List<BookReservation> GetOlderReservations(int limit)
        {
            return Reserves.GetRange(0,Math.Min(limit,Reserves.Count));
        }

        public List<BookReservation> FindReservationsOf(string bookID)
        {
            return Reserves.Where(reserve => reserve.BookID == bookID).ToList();
        }

        public List<BookReservation> FindReservationsBy(string userID)
        {
            return Reserves.Where(reserve => reserve.UserID==userID).ToList();

        }

    }
}
