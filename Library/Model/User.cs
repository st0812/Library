using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model.Model
{
    public class User
    {
        public string ID { get; }=Guid.NewGuid().ToString();

        public User()
        {
        }
    }
}
