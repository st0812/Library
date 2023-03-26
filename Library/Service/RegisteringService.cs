using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Model;
using Library.Repositories;

namespace Library
{
    public class RegisteringService
    {
        private IBooks Books { get; set; }

        public RegisteringService(IBooks library)
        {
            Books = library;
        }
        public void RegisterBook()
        {
            Books.Add(new Book());
        }
    }
}
