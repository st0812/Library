using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;

namespace Library.Repositories
{
    public interface IBooks
    {
        void Add(Book book);
        void UpdateStatus(string id, BookStatus status);
        BookStatus QueryStatus(string id);

        List<Book> FindBooks();

        List<Book> FindBooksInStorage();
    }
}
