using ExpirationDestroyerBlazorServer.BusinessLogic.DTOs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpirationDestroyerBlazorServer.UnitTests.BusinessLogic
{
    [TestClass]
    public class ProductDTOTest
    {
        [TestMethod]
        public void Clone_CreatesObjectWithTheSameDataAndDifferentReference()
        {
            var dto = new ProductDTO()
            {
                ID = 15,
                Name = "Test name",
                Consumed = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddDays(1),
                ExpirationDate = DateTime.Now.AddDays(2),
                DeletedAt = DateTime.Now.AddDays(3)
            };

            var clonedDto = dto.Clone();

            Assert.IsFalse(ReferenceEquals(dto, clonedDto));
            Assert.AreEqual(dto.ID, clonedDto.ID);
            Assert.AreEqual(dto.Name, clonedDto.Name);
            Assert.AreEqual(dto.Consumed, clonedDto.Consumed);
            Assert.AreEqual(dto.CreatedAt, clonedDto.CreatedAt);
            Assert.AreEqual(dto.UpdatedAt, clonedDto.UpdatedAt);
            Assert.AreEqual(dto.ExpirationDate, clonedDto.ExpirationDate);
            Assert.AreEqual(dto.DeletedAt, clonedDto.DeletedAt);
            Assert.AreEqual(dto.Expired, clonedDto.Expired);
        }
    }
}
