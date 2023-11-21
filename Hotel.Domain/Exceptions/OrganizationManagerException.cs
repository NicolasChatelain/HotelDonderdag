using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class OrganizationManagerException : Exception
    {
        public OrganizationManagerException(string? message) : base(message)
        {
        }

        public OrganizationManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
