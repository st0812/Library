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
    public class DummyHistories : ICheckoutHistories
    {
        public List<CheckoutHistory> Items = new List<CheckoutHistory>();
        public void Add(CheckoutHistory item)
        {
            Items.Add(item);
        }
    }
}
