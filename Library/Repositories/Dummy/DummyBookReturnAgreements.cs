using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Repositories.Dummy
{
    public class DummyBookReturnAgreements : IBookReturnAgreements
    {
        public List<BookReturnAgreement> Transactions { get; set; } = new List<BookReturnAgreement>();
        public void Add(BookReturnAgreement transaction)
        {
            Transactions.Add(transaction);
        }

        public void Delete(string transactionID)
        {
            var transaction = Transactions.Where(tran => tran.ID == transactionID).First();
            Transactions.Remove(transaction);
        }


        public List<BookReturnAgreement> FindOverdues(DateTime dateTime)
        {
            return Transactions.Where(tran => tran.DueDate < dateTime).ToList();
        }

        public List<BookReturnAgreement> FindOverduesBy(string userID,DateTime dateTime)
        {
            return Transactions
                .Where(tran=>tran.UserID==userID)
                .Where(tran => tran.IsOverdue(dateTime))
                .ToList();
        }

        public BookReturnAgreement Get(string bookID)
        {
            return Transactions.Where(tran => tran.BookID == bookID).First();
        }

        public List<BookReturnAgreement> GetAgreementsBy(string userID)
        {
            return Transactions.Where(tran => tran.UserID == userID).ToList();
        }
    }
}
