using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class BookReservation
    {
        public string ReservationId { get; }=Guid.NewGuid().ToString();
        public string UserID { get;  }
        public string BookID { get;  }

        public DateTime ReservationDate { get;  }

        public BookReservation(string userID, string bookID, DateTime date)
        {
            UserID = userID;
            BookID = bookID;
            ReservationDate = date;
        }

        public BookReservation(string iD, string userID, string bookID, DateTime date)
        {
            ReservationId = iD;
            UserID = userID;
            BookID = bookID;
            ReservationDate = date;
        }
    }
}
