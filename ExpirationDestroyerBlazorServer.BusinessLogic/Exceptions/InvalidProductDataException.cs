using System;
using System.Collections.Generic;
using System.Text;

namespace ExpirationDestroyerBlazorServer.BusinessLogic.Exceptions
{
    public class InvalidProductDataException : Exception
    {
        public InvalidProductDataException(string message) : base(message)
        {
        }
    }
}
