using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class OrganizationRepositoryException : Exception
    {
        public OrganizationRepositoryException(string? message) : base(message)
        {
        }

        public OrganizationRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
