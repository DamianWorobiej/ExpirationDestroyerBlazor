using AutoMapper;
using ExpirationDestroyerBlazorServer.BusinessLogic.DTOs;
using ExpirationDestroyerBlazorServer.BusinessLogic.Mappers;
using ExpirationDestroyerBlazorServer.BusinessLogic.ProductsService;
using ExpirationDestroyerBlazorServer.DataAccess;
using ExpirationDestroyerBlazorServer.DataAccess.Exceptions;
using ExpirationDestroyerBlazorServer.DataAccess.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpirationDestroyerBlazorServer.UnitTests.BusinessLogic
{
    [TestClass]
    public class ProductsServiceTests
    {
        private ProductsService _service;
        private readonly Mock<IProductsRepository> _mock;

        public ProductsServiceTests()
        {
            _mock = new Mock<IProductsRepository>();

        }

        #region Setup

        private void CreateService()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            _service = new ProductsService(_mock.Object, mapper);
        }

        #endregion

        #region Add

        [TestMethod]
        public void Add_CallsRepoAddOnce()
        {
            this.CreateService();

            _service.Add(new ProductDTO());

            _mock.Verify(repo => repo.Add(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public async Task AddAsync_CallsRepoAddOnce()
        {
            this.CreateService();

            await _service.AddAsync(new ProductDTO());

            _mock.Verify(repo => repo.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void Add_ReturnsID()
        {
            _mock.Setup(repo => repo.Add(It.IsAny<Product>())).Returns(1);
            this.CreateService();

            var id = _service.Add(new ProductDTO());

            Assert.AreEqual(1, id);
        }

        [TestMethod]
        public async Task AddAsync_ReturnsID()
        {
            _mock.Setup(repo => repo.AddAsync(It.IsAny<Product>())).Returns(async () => 1);
            this.CreateService();

            var id = await _service.AddAsync(new ProductDTO());

            Assert.AreEqual(1, id);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_ByModel_CallsRepoDeleteOnce()
        {
            this.CreateService();

            _service.Delete(new ProductDTO());

            _mock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public void Delete_ByModel_DoesNotCallById()
        {
            this.CreateService();

            _service.Delete(new ProductDTO());

            _mock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void Delete_ById_CallsRepoDeleteOnce()
        {
            this.CreateService();

            _service.Delete(0);

            _mock.Verify(repo => repo.Delete(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Delete_ByModel_DoesNotCallByModel()
        {
            this.CreateService();

            _service.Delete(0);

            _mock.Verify(repo => repo.Delete(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public async Task DeleteAsync_ById_CallsRepoDeleteOnce()
        {
            this.CreateService();

            await _service.DeleteAsync(0);

            _mock.Verify(repo => repo.DeleteAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteAsync_ById_DoesNotCallByModel()
        {
            this.CreateService();

            await _service.DeleteAsync(0);

            _mock.Verify(repo => repo.DeleteAsync(It.IsAny<Product>()), Times.Never);
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAll_CallsRepoGetAllOnce()
        {
            this.CreateService();

            _service.GetAll();

            _mock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyCollectionOnEmpty()
        {
            _mock.Setup(repo => repo.GetAll()).Returns(new List<Product>());
            this.CreateService();

            var all = _service.GetAll();

            Assert.AreEqual(0, all.Count());
        }

        [TestMethod]
        public async Task GetAllAsync_CallsRepoGetAllOnce()
        {
            this.CreateService();

            await _service.GetAllAsync();

            _mock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsEmptyCollectionOnEmpty()
        {
            _mock.Setup(repo => repo.GetAllAsync()).Returns(async () => new List<Product>());
            this.CreateService();

            var all = await _service.GetAllAsync();

            Assert.AreEqual(0, all.Count());
        }

        #endregion

        #region GetById

        [TestMethod]
        public void GetById_CallsRepoGetByIdOnce()
        {
            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Product());
            this.CreateService();

            _service.GetById(0);

            _mock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void GetById_ThrowsProductNotFoundOnMissingModel()
        {
            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);
            this.CreateService();

            _service.GetById(0);
        }

        [TestMethod]
        public async Task GetByIdAsync_CallsRepoGetByIdOnce()
        {
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(async () => new Product());
            this.CreateService();

            await _service.GetByIdAsync(0);

            _mock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public async Task GetByIdAsync_ThrowsProductNotFoundOnMissingModel()
        {
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(async () => (Product)null);
            this.CreateService();

            await _service.GetByIdAsync(0);
        }

        #endregion

        #region Update

        [TestMethod]
        public void Update_CallsRepoUpdateOnce()
        {
            this.CreateService();

            _service.Update(new ProductDTO());

            _mock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public async Task UpdateAsync_CallsRepoUpdateOnce()
        {
            this.CreateService();

            await _service.UpdateAsync(new ProductDTO());

            _mock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        #endregion

        #region SetAsConsumed

        [TestMethod]
        public void SetAsConsumed_SetsConsumedProperty()
        {
            Product model = new Product()
            {
                Consumed = false
            };
            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Product());
            _mock.Setup(repo => repo.Update(It.IsAny<Product>()))
                    .Callback<Product>((obj) => model = obj);
            this.CreateService();

            _service.SetAsConsumed(0);

            Assert.IsTrue(model.Consumed);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void SetAsConsumed_ThrowsProductNotFoundOnMissingModel()
        {
            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);
            this.CreateService();

            _service.SetAsConsumed(0);
        }

        [TestMethod]
        public void SetAsConsumed_CallsRepoGetByIdAndUpdateOnce()
        {
            _mock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(new Product());
            this.CreateService();

            _service.SetAsConsumed(0);

            _mock.Verify(repo => repo.GetById(It.IsAny<int>()), Times.Once);
            _mock.Verify(repo => repo.Update(It.IsAny<Product>()), Times.Once);
        }

        [TestMethod]
        public async Task SetAsConsumedAsync_SetsConsumedProperty()
        {
            Product model = new Product()
            {
                Consumed = false
            };
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(async () => new Product());
            _mock.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                    .Callback<Product>((obj) => model = obj);
            this.CreateService();

            await _service.SetAsConsumedAsync(0);

            Assert.IsTrue(model.Consumed);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public async Task SetAsConsumedAsync_ThrowsProductNotFoundOnMissingModel()
        {
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(async () => (Product)null);
            this.CreateService();

            await _service.SetAsConsumedAsync(0);
        }


        [TestMethod]
        public async Task SetAsConsumedAsync_CallsRepoGetByIdAndUpdateOnce()
        {
            _mock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).Returns(async () => new Product());
            this.CreateService();

            await _service.SetAsConsumedAsync(0);

            _mock.Verify(repo => repo.GetByIdAsync(It.IsAny<int>()), Times.Once);
            _mock.Verify(repo => repo.UpdateAsync(It.IsAny<Product>()), Times.Once);
        }

        #endregion
    }
}
