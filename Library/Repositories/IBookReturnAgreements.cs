using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library
{
    public interface IBookReturnAgreements
    {
        void Add(BookReturnAgreement transaction);
        void Delete(string transactionID);

        BookReturnAgreement Get(string bookID);

        List<BookReturnAgreement> GetTransactionsBy(string userID);

        List<BookReturnAgreement> FindOverdues(DateTime dateTime);

        List<BookReturnAgreement> FindOverduesBy(string userID,DateTime dateTime);

    }
}
