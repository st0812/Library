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
    public class DummyReservations : IReservations
    {
        public List<Reservation> Reserves { get; set; } = new List<Reservation>();
      
        public void Add(Reservation reserve)
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

        public Reservation Get(string reserveID)
        {
            return Reserves.Where(reserve => reserve.ID == reserveID).First();
        }

        public List<Reservation> GetPrimeReserves()
        {
            return new List<Reservation>(Reserves);
        }

        public List<Reservation> GetReserves(string bookID)
        {
            return Reserves.Where(reserve => reserve.BookID == bookID).ToList();
        }

        public List<Reservation> GetReservesBy(string userID)
        {
            return Reserves.Where(reserve => reserve.UserID==userID).ToList();

        }

    }
}
