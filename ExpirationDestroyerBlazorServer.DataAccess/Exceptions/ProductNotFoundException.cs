using System;
using System.Collections.Generic;
using System.Text;

namespace ExpirationDestroyerBlazorServer.DataAccess.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(string message) : base(message)
        {

        }
    }
}
