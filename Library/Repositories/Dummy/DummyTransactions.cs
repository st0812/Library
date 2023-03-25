using Library;
using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library
{
    public class DummyTransactions : ITransactions
    {
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public void Add(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void Delete(string transactionID)
        {
            var transaction = Transactions.Where(tran => tran.ID == transactionID).First();
            Transactions.Remove(transaction);
        }


        public List<Transaction> FindOverdues(DateTime dateTime)
        {
            return Transactions.Where(tran => tran.DueDate < dateTime).ToList();
        }

        public List<Transaction> FindOverduesBy(string userID,DateTime dateTime)
        {
            return Transactions
                .Where(tran=>tran.UserID==userID)
                .Where(tran => tran.IsDelayed(dateTime))
                .ToList();
        }

        public Transaction Get(string bookID)
        {
            return Transactions.Where(tran => tran.BookID == bookID).First();
        }

        public List<Transaction> GetTransactionsBy(string userID)
        {
            return Transactions.Where(tran => tran.UserID == userID).ToList();
        }
    }
}
