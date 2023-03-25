using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Service
{
    public class ReservingException : Exception
    {
        public ReservingException()
        {

        }

        public ReservingException(string message) : base(message)
        {

        }

        public ReservingException(string message, Exception inner) : base(message, inner)
        {


        }
    }

    public class ReservingService
    {
        private IReservations Reservations;
        private ITransactions Transactions;

        public ReservingService(IReservations reserves,ITransactions transactions)
        {
            Reservations = reserves;
            Transactions = transactions;
        }

        public void Reserve(string bookID, string userID,DateTime dateTime)
        {
            if (Transactions.FindOverduesBy(userID,dateTime).Count() != 0) throw new ReservingException("延滞本があります。");
            Reservations.Add(new Reservation(userID, bookID, dateTime));
        }

        public List<Reservation> GetReservesBy(string userID)
        {
            return Reservations.GetReservesBy(userID);
        }

       

        public void Cancel(string id)
        {
            var reserve = Reservations.Get(id);
            Reservations.Delete(id);
        }

    }
}
