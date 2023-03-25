using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public enum Status
    {
        Shelf,
        Backyard,
        Rentaled,
        Missing
    }
    public class Book
    {
        public string ID { get;  }= Guid.NewGuid().ToString();
        public Status Status { get;}= Status.Shelf;

        public Book(string id, Status status)
        {
            ID = id;
            Status = status;
        }
        public Book()
        {

        }
    }
}
