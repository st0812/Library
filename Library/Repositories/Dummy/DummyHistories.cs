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
    public class DummyHistories : IHistories
    {
        public List<History> Items = new List<History>();
        public void Add(History item)
        {
            Items.Add(item);
        }
    }
}
