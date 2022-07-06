using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardApp.BLL.Exceptions
{
    public class CardNotBelongToUserException : Exception
    {
        public CardNotBelongToUserException() { }
        public CardNotBelongToUserException(string message) : base(message) { }
        public CardNotBelongToUserException(string message, Exception innerException) : base(message, innerException) { }
    }
}
