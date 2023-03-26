using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class CheckoutHistory
    {
        public string HistoryId= Guid.NewGuid().ToString();
        public string UserID { get;  }
        public string BookID { get; }
        public DateTime CheckoutDate { get;  }
        public DateTime ReturnDate { get;  }

        public CheckoutHistory(string userID, string bookID, DateTime checkOutDate,DateTime returnDate)
        {
            UserID = userID;
            BookID = bookID;
            CheckoutDate = checkOutDate;
            ReturnDate = returnDate;
        }
        public CheckoutHistory(string id, string userID, string bookID, DateTime checkOutDate, DateTime returnDate)
        {
            HistoryId = id;
            UserID = userID;
            BookID = bookID;
            CheckoutDate = checkOutDate;
            ReturnDate = returnDate;
        }
    }
}
