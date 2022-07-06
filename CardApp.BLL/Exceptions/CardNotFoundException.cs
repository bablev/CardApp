using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public CardNotFoundException() { }
        public CardNotFoundException(string message) : base(message) { }
        public CardNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
