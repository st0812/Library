using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class Transaction
    {
        public string ID { get; } = Guid.NewGuid().ToString();
        public string UserID { get;  }
        public string BookID { get; }
        public DateTime CheckOutDate { get;  }
        public DateTime DueDate { get;  }


        public Transaction(string userID, string bookID,DateTime checkOutDate, DateTime dueDate)
        {
            UserID = userID;
            BookID = bookID;
            CheckOutDate = checkOutDate;
            DueDate = dueDate;
        }
        public Transaction(string iD, string userID, string bookID, DateTime checkOutDate, DateTime dueDate)
        {
            ID = iD;
            UserID = userID;
            BookID = bookID;
            CheckOutDate = checkOutDate;
            DueDate = dueDate;
        }

        public bool IsDelayed(DateTime dateTime)
        {
            return DueDate < dateTime;
        }
    }
}
