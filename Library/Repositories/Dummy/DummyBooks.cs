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

        public List<Book> FindBooksInStorage()
        {
            return Books.Where(book => book.BookStatus == BookStatus.InStorage).ToList();
        }

        public BookStatus QueryStatus(string id)
        {
            return Books.First(book => book.Id == id).BookStatus;
        }

        public void UpdateStatus(string id, BookStatus status)
        {
            var book= Books.First(book => book.Id == id);
            var books2 = new Book(book.Id, status);
            Books.Remove(book);
            Books.Add(books2);
        }
    }
}
