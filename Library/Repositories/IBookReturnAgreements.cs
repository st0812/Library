using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Repositories
{
    public interface IBookReturnAgreements
    {
        void Add(BookReturnAgreement transaction);
        void Delete(string transactionID);

        BookReturnAgreement Get(string bookID);

        List<BookReturnAgreement> GetAgreementsBy(string userID);

        List<BookReturnAgreement> FindOverdues(DateTime dateTime);

        List<BookReturnAgreement> FindOverduesBy(string userID,DateTime dateTime);

    }
}
