using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class History
    {
        public string ID= Guid.NewGuid().ToString();
        public string UserID { get;  }
        public string BookID { get; }
        public DateTime CheckOutDate { get;  }
        public DateTime ReturnDate { get;  }

        public History(string userID, string bookID, DateTime checkOutDate,DateTime returnDate)
        {
            UserID = userID;
            BookID = bookID;
            CheckOutDate = checkOutDate;
            ReturnDate = returnDate;
        }
        public History(string id, string userID, string bookID, DateTime checkOutDate, DateTime returnDate)
        {
            ID = id;
            UserID = userID;
            BookID = bookID;
            CheckOutDate = checkOutDate;
            ReturnDate = returnDate;
        }
    }
}
