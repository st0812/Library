using Library;
using Library.Model;
using Library.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class DummyBooks : IBooks
    {
        public List<Book> Books { get; set; } = new List<Book>();

        public void Add(Book book)
        {
            Books.Add(book);
        }

        public List<Book> GetBooksInBackYard()
        {
            return Books.Where(book => book.Status == Status.Backyard).ToList();
        }

        public Status QueryStatus(string id)
        {
            return Books.First(book => book.ID == id).Status;
        }

        public void UpdateStatus(string id, Status status)
        {
            var book= Books.First(book => book.ID == id);
            var books2 = new Book(book.ID, status);
            Books.Remove(book);
            Books.Add(books2);
        }
    }
}
