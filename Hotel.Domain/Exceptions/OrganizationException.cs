using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class OrganizationException : Exception
    {
        public OrganizationException(string? message) : base(message)
        {
        }

        public OrganizationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
