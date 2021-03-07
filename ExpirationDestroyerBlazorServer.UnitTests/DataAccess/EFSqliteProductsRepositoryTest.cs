using ExpirationDestroyerBlazorServer.DataAccess.DBContexts;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpirationDestroyerBlazorServer.DataAccess.Models;
using ExpirationDestroyerBlazorServer.DataAccess.ProductsRepository;
using System.Linq;
using System.Threading.Tasks;
using ExpirationDestroyerBlazorServer.DataAccess.Exceptions;
using System.Collections.Generic;

namespace ExpirationDestroyerBlazorServer.UnitTests.DataAccess
{
    [TestClass]
    public class EFSqliteProductsRepositoryTest
    {
        private readonly EFSQLiteDBContext _context;
        private readonly EFSQLiteProductsRepository _repo;

        public EFSqliteProductsRepositoryTest()
        {
            _context = this.CreateContextForInMemory();
            _repo = new EFSQLiteProductsRepository(_context);
        }

        #region Setup

        private EFSQLiteDBContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<EFSQLiteDBContext>().UseInMemoryDatabase(databaseName: "Repository_Tests").Options;
            //var option = new DbContextOptionsBuilder<EFSQLiteDBContext>().UseSqlite("DataSource=:memory:").Options;

            var context = new EFSQLiteDBContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        #endregion

        #region Add

        [TestMethod]
        public void Add_AddsEntry()
        {
            var product = new Product()
            {
                Name = "Name",
                ExpirationDate = DateTime.Now
            };

            _repo.Add(product);

            var all = _repo.GetAll();

            Assert.AreEqual(1, all.Count());

            var insertedProduct = all.First();
            Assert.AreEqual(product.Name, insertedProduct.Name);
            Assert.AreEqual(product.ExpirationDate, insertedProduct.ExpirationDate);
        }

        [TestMethod]
        public async Task AddAsync_AddsEntry()
        {
            var product = new Product()
            {
                Name = "Name",
                ExpirationDate = DateTime.Now
            };

            await _repo.AddAsync(product);

            var all = _repo.GetAll();

            Assert.AreEqual(1, all.Count());

            var insertedProduct = all.First();
            Assert.AreEqual(product.Name, insertedProduct.Name);
            Assert.AreEqual(product.ExpirationDate, insertedProduct.ExpirationDate);
        }

        [TestMethod]
        public void Add_ReturnsProperID()
        {
            var product = new Product()
            {
                Name = "Name",
                ExpirationDate = DateTime.Now
            };

            var id =_repo.Add(product);
            var model = _repo.GetAll().First();

            Assert.AreEqual(model.ID, id);
        }

        [TestMethod]
        public async Task AddAsync_ReturnsProperID()
        {
            var product = new Product()
            {
                Name = "Name",
                ExpirationDate = DateTime.Now
            };

            var id = await _repo.AddAsync(product);
            var model = _repo.GetAll().First();

            Assert.AreEqual(model.ID, id);
        }

        #endregion

        #region GetByID

        [TestMethod]
        public void GetById_ReturnsProperModel()
        {
            var id = this.AddDummyProduct();

            var model = _repo.GetById(id);

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsNullOnMissingModel()
        {
            var model = await _repo.GetByIdAsync(0);
            Assert.IsNull(model);
        }

        [TestMethod]
        public async Task GetByIdAsync_ReturnsProperModel()
        {
            var id = this.AddDummyProduct();

            var model = await _repo.GetByIdAsync(id);

            Assert.IsNotNull(model);
        }

        [TestMethod]
        public void GetById_ReturnsNullOnMissingModel()
        {
            var model = _repo.GetById(0);
            Assert.IsNull(model);
        }

        #endregion

        #region Delete

        [TestMethod]
        public void Delete_ByModel_RemovesEntry()
        {
            var id = this.AddDummyProduct();

            var model = _repo.GetById(id);

            Assert.IsNotNull(model);

            _repo.Delete(model);

            Assert.IsNull(_repo.GetById(id));
        }

        [TestMethod]
        public void Delete_ByModel_SetsDeletedAt()
        {
            var id = this.AddDummyProduct();

            var model = _repo.GetById(id);

            Assert.IsNotNull(model);

            _repo.Delete(model);

            var removedEntry = _context.Products.First(p => p.ID == id);

            Assert.IsTrue(removedEntry.DeletedAt > DateTime.MinValue);
        }

        [TestMethod]
        public async Task DeleteAsync_ByModel_RemovesEntry()
        {
            var id = this.AddDummyProduct();

            var model = _repo.GetById(id);

            Assert.IsNotNull(model);

            await _repo.DeleteAsync(model);

            Assert.IsNull(_repo.GetById(id));
        }

        [TestMethod]
        public void Delete_ById_RemoveEntry()
        {
            var id = this.AddDummyProduct();

            _repo.Delete(id);

            Assert.IsNull(_repo.GetById(id));
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void Delete_ById_ThrowsProductNotFoundOnRemovingMissingModel()
        {
            _repo.Delete(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public async Task DeleteAsync_ById_ThrowsProductNotFoundOnRemovingMissingModel()
        {
            await _repo.DeleteAsync(0);
        }

        [TestMethod]
        public async Task DeleteAsync_ById_RemoveEntry()
        {
            var id = this.AddDummyProduct();

            await _repo.DeleteAsync(id);

            Assert.IsNull(_repo.GetById(id));
        }

        #endregion

        #region GetAll

        [TestMethod]
        public void GetAll_ReturnsAll()
        {
            var amountTested = 5;
            var products = new List<Product>();

            for (int i = 0; i < amountTested; i++)
            {
                products.Add(this.AddDummyProduct(i));
            }

            var all = _repo.GetAll().ToList();

            Assert.AreEqual(all.Count, products.Count);

            for (int i = 0; i < amountTested; i++)
            {
                Assert.AreEqual(products[i].ID, all[i].ID);
                Assert.AreEqual(products[i].Name, all[i].Name);
                Assert.AreEqual(products[i].ExpirationDate, all[i].ExpirationDate);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAll()
        {
            var amountTested = 5;
            var products = new List<Product>();

            for (int i = 0; i < amountTested; i++)
            {
                products.Add(this.AddDummyProduct(i));
            }

            var resp = await _repo.GetAllAsync();
            var all = resp.ToList();

            Assert.AreEqual(all.Count, products.Count);

            for (int i = 0; i < amountTested; i++)
            {
                Assert.AreEqual(products[i].ID, all[i].ID);
                Assert.AreEqual(products[i].Name, all[i].Name);
                Assert.AreEqual(products[i].ExpirationDate, all[i].ExpirationDate);
            }
        }

        [TestMethod]
        public void GetAll_EmptyReturnsEmpty()
        {
            Assert.AreEqual(0, _repo.GetAll().Count());
        }

        [TestMethod]
        public async Task GetAllAsync_EmptyReturnsEmpty()
        {
            var list = await _repo.GetAllAsync();
            Assert.AreEqual(0, list.Count());
        }

        [TestMethod]
        public void GetAll_DoNotReturnDeletedElements()
        {
            var existingProduct = this.AddDummyProduct(0);
            var idToDelete = this.AddDummyProduct();
            _repo.Delete(idToDelete);

            var all = _repo.GetAll();

            Assert.AreEqual(1, all.Count());
            var addedModel = all.First();
            Assert.AreEqual(existingProduct.ID, addedModel.ID);
            Assert.AreEqual(existingProduct.Name, addedModel.Name);
            Assert.AreEqual(existingProduct.ExpirationDate, addedModel.ExpirationDate);
        }

        [TestMethod]
        public async Task GetAllAsync_DoNotReturnDeletedElements()
        {
            var existingProduct = this.AddDummyProduct(0);
            var idToDelete = this.AddDummyProduct();
            _repo.Delete(idToDelete);

            var all = await _repo.GetAllAsync();

            Assert.AreEqual(1, all.Count());
            var addedModel = all.First();
            Assert.AreEqual(existingProduct.ID, addedModel.ID);
            Assert.AreEqual(existingProduct.Name, addedModel.Name);
            Assert.AreEqual(existingProduct.ExpirationDate, addedModel.ExpirationDate);
        }

        #endregion

        #region Update

        [TestMethod]
        public void Update_UpdatesModelInDB()
        {
            var product = this.AddDummyProduct(0);
            product.Name = "Edited name";

            _repo.Update(product);

            var updatedProduct =_repo.GetById(product.ID);
            Assert.AreEqual(product.ID, updatedProduct.ID);
            Assert.AreEqual(product.Name, updatedProduct.Name);
            Assert.AreEqual(product.ExpirationDate, updatedProduct.ExpirationDate);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesModelInDB()
        {
            var product = this.AddDummyProduct(0);
            product.Name = "Edited name";

            await _repo.UpdateAsync(product);

            var updatedProduct = _repo.GetById(product.ID);
            Assert.AreEqual(product.ID, updatedProduct.ID);
            Assert.AreEqual(product.Name, updatedProduct.Name);
            Assert.AreEqual(product.ExpirationDate, updatedProduct.ExpirationDate);
        }

        [TestMethod]
        [ExpectedException(typeof(ProductNotFoundException))]
        public void Update_ThrowsProductNotFoundExceptionOnNotFoundElement()
        {
            var product = this.AddDummyProduct(0);
            _repo.Delete(product.ID);
            product.Name = "Edited name";

            _repo.Update(product);
        }

        [TestMethod]
        public async Task UpdateAsync_ThrowsProductNotFoundExceptionOnNotFoundElement()
        {
            var product = this.AddDummyProduct(0);
            product.Name = "Edited name";

            await _repo.UpdateAsync(product);
        }

        #endregion

        #region GetNotAllExpired

        [TestMethod]
        public void GetAllNotExpired_DoNotReturnExpired()
        {
            var product1 = this.AddDummyProduct(0, DateTime.Now.AddDays(2));
            this.AddDummyProduct(1, DateTime.Now.Subtract(TimeSpan.FromDays(2)));

            var all = _repo.GetAllNotExpired();

            Assert.AreEqual(1, all.Count());

            var model = all.First();

            Assert.AreEqual(product1.ID, model.ID);
        }

        [TestMethod]
        public async Task GetAllNotExpiredAsync_DoNotReturnExpired()
        {
            var product1 = this.AddDummyProduct(0, DateTime.Now.AddDays(2));
            this.AddDummyProduct(1, DateTime.Now.Subtract(TimeSpan.FromDays(2)));

            var all = await _repo.GetAllNotExpiredAsync();

            Assert.AreEqual(1, all.Count());

            var model = all.First();

            Assert.AreEqual(product1.ID, model.ID);
        }

        [TestMethod]
        public void GetAllNotExpired_DoNotReturnDeleted()
        {
            var product1 = this.AddDummyProduct(0, DateTime.Now.AddDays(2));
            var product2 = this.AddDummyProduct(1, DateTime.Now.AddDays(2));
            _repo.Delete(product2);

            var all = _repo.GetAllNotExpired();

            Assert.AreEqual(1, all.Count());

            var model = all.First();

            Assert.AreEqual(product1.ID, model.ID);
        }

        [TestMethod]
        public async Task GetAllNotExpiredAsync_DoNotReturnDeleted()
        {
            var product1 = this.AddDummyProduct(0, DateTime.Now.AddDays(2));
            var product2 = this.AddDummyProduct(1, DateTime.Now.AddDays(2));
            _repo.Delete(product2);

            var all = await _repo.GetAllNotExpiredAsync();

            Assert.AreEqual(1, all.Count());

            var model = all.First();

            Assert.AreEqual(product1.ID, model.ID);
        }

        #endregion

        #region Helpers

        private int AddDummyProduct()
        {
            var product = new Product()
            {
                Name = "Name",
                ExpirationDate = DateTime.Now
            };

            return _repo.Add(product);
        }

        private Product AddDummyProduct(int number)
        {
            var product = new Product()
            {
                Name = "Name" + number,
                ExpirationDate = DateTime.Now
            };

            _repo.Add(product);

            return product;
        }

        private Product AddDummyProduct(int number, DateTime expirationDate)
        {
            var product = new Product()
            {
                Name = "Name" + number,
                ExpirationDate = expirationDate
            };

            _repo.Add(product);

            return product;
        }

        #endregion
    }

}
