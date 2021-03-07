using ExpirationDestroyerBlazorServer.DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ExpirationDestroyerBlazorServer.UnitTests.DataAccess
{
    [TestClass]
    public class ProductTest
    {
        [TestMethod]
        public void Expired_TrueIfPastExpirationDate()
        {
            var model = new Product()
            {
                ExpirationDate = DateTime.Now.Subtract(TimeSpan.FromDays(1))
            };

            Assert.IsTrue(model.Expired);
        }

        [TestMethod]
        public void Expired_FalseIfNotPastExpirationDate()
        {
            var model = new Product()
            {
                ExpirationDate = DateTime.Now.AddDays(1)
            };

            Assert.IsFalse(model.Expired);
        }
    }
}
