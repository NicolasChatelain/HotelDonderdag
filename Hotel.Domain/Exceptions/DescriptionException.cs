using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class DescriptionException : Exception
    {
        public DescriptionException(string? message) : base(message)
        {
        }

        public DescriptionException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
