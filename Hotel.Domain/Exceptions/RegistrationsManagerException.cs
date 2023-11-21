﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class RegistrationsManagerException : Exception
    {
        public RegistrationsManagerException(string? message) : base(message)
        {
        }

        public RegistrationsManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
