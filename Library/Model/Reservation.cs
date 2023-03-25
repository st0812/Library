using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Reservation
    {
        public string ID { get; }=Guid.NewGuid().ToString();
        public string UserID { get;  }
        public string BookID { get;  }

        public DateTime Date { get;  }

        public Reservation(string userID, string bookID, DateTime date)
        {
            UserID = userID;
            BookID = bookID;
            Date = date;
        }

        public Reservation(string iD, string userID, string bookID, DateTime date)
        {
            ID = iD;
            UserID = userID;
            BookID = bookID;
            Date = date;
        }
    }
}
