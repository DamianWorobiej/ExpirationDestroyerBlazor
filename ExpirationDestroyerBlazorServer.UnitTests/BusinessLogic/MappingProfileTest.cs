using AutoMapper;
using ExpirationDestroyerBlazorServer.BusinessLogic.DTOs;
using ExpirationDestroyerBlazorServer.BusinessLogic.Mappers;
using ExpirationDestroyerBlazorServer.DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpirationDestroyerBlazorServer.UnitTests.BusinessLogic
{
    [TestClass]
    public class MappingProfileTest
    {
        private readonly IMapper _mapper;

        public MappingProfileTest()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();
        }

        [TestMethod]
        public void Mapper_MapsModelToDTO()
        {
            var model = new Product()
            {
                ID = 15,
                Name = "Test name",
                Consumed = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddDays(1),
                ExpirationDate = DateTime.Now.AddDays(2),
                DeletedAt = DateTime.Now.AddDays(3)
            };

            var dto = _mapper.Map<ProductDTO>(model);

            Assert.AreEqual(model.ID, dto.ID);
            Assert.AreEqual(model.Name, dto.Name);
            Assert.AreEqual(model.Consumed, dto.Consumed);
            Assert.AreEqual(model.CreatedAt, dto.CreatedAt);
            Assert.AreEqual(model.UpdatedAt, dto.UpdatedAt);
            Assert.AreEqual(model.ExpirationDate, dto.ExpirationDate);
            Assert.AreEqual(model.DeletedAt, dto.DeletedAt);
            Assert.AreEqual(model.Expired, dto.Expired);
        }

        [TestMethod]
        public void Mapper_MapsDTOToModel()
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

            var model = _mapper.Map<Product>(dto);

            Assert.AreEqual(dto.ID, model.ID);
            Assert.AreEqual(dto.Name, model.Name);
            Assert.AreEqual(dto.Consumed, model.Consumed);
            Assert.AreEqual(dto.CreatedAt, model.CreatedAt);
            Assert.AreEqual(dto.UpdatedAt, model.UpdatedAt);
            Assert.AreEqual(dto.ExpirationDate, model.ExpirationDate);
            Assert.AreEqual(dto.DeletedAt, model.DeletedAt);
            Assert.AreEqual(dto.Expired, model.Expired);
        }
    }
}
