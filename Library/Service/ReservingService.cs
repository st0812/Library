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

    public class ReservationService
    {
        private readonly IBookReservations Reservations;
        private readonly IBookReturnAgreements Agreements;

        public ReservationService(IBookReservations reserves,IBookReturnAgreements agreements)
        {
            Reservations = reserves;
            Agreements = agreements;
        }

        public void ReserveBook(string bookID, string userID,DateTime reserveDate)
        {
            if (Agreements.FindOverduesBy(userID,reserveDate).Any()) throw new ReservingException("延滞本があります。");
            Reservations.Add(new BookReservation(userID, bookID, reserveDate));
        }

        public List<BookReservation> FindReservationsBy(string userID)
        {
            return Reservations.FindReservationsBy(userID);
        }
       

        public void CancelReservation(string reservationID)
        {
            Reservations.Delete(reservationID);
        }

    }


}
