using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public class BookReturnAgreement
    {
        public string ID { get; } = Guid.NewGuid().ToString();
        public string UserID { get;  }
        public string BookID { get; }
        public DateTime CheckoutDate { get;  }
        public DateTime DueDate { get;  }


        public BookReturnAgreement(string userID, string bookID,DateTime checkoutDate, DateTime dueDate)
        {
            UserID = userID;
            BookID = bookID;
            CheckoutDate = checkoutDate;
            DueDate = dueDate;
        }
        public BookReturnAgreement(string iD, string userID, string bookID, DateTime checkOutDate, DateTime dueDate)
        {
            ID = iD;
            UserID = userID;
            BookID = bookID;
            CheckoutDate = checkOutDate;
            DueDate = dueDate;
        }

        public bool IsOverdue(DateTime currentDateTime)
        {
            return DueDate < currentDateTime;
        }
    }
}
