using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Repositories.Dummy
{
    public class DummyCheckoutHistories : ICheckoutHistories
    {
        public List<CheckoutHistory> Items = new List<CheckoutHistory>();
        public void Add(CheckoutHistory item)
        {
            Items.Add(item);
        }

        public List<CheckoutHistory> FindCheckoutHistoriesBy(string userID)
        {
            return Items.Where(item => item.UserID == userID).ToList();
        }
    }
}
