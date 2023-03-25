using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public interface IReservations
    {
        void Add(Reservation reserve);
        void Delete(string reserveID);
        bool Exists(string bookID);
        List<Reservation> GetReserves(string bookID);

        List<Reservation> GetReservesBy(string userID);
        List<Reservation> GetPrimeReserves();
        Reservation Get(string reserveID);
    }
}
