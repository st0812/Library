using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Repositories
{
    public interface ICheckoutHistories
    {
        void Add(CheckoutHistory history);
        List<CheckoutHistory> FindCheckoutHistoriesBy(string userID);

    }
}
