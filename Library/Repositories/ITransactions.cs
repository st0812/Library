using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library
{
    public interface ITransactions
    {
        void Add(Transaction transaction);
        void Delete(string transactionID);

        Transaction Get(string bookID);

        List<Transaction> GetTransactionsBy(string userID);

        List<Transaction> FindOverdues(DateTime dateTime);

        List<Transaction> FindOverduesBy(string userID,DateTime dateTime);

    }
}
