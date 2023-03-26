using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    public enum BookStatus
    {
        OnShelf,
        InStorage,
        Rented,
        Lost
    }
    public class Book
    {
        public string Id { get;  }= Guid.NewGuid().ToString();
        public BookStatus BookStatus { get;}= BookStatus.OnShelf;

        public Book(string id, BookStatus status)
        {
            Id = id;
            BookStatus = status;
        }
        public Book()
        {

        }
    }
}
