using ExpirationDestroyerBlazorServer.DataAccess.DBContexts;
using ExpirationDestroyerBlazorServer.DataAccess.Exceptions;
using ExpirationDestroyerBlazorServer.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpirationDestroyerBlazorServer.DataAccess.ProductsRepository
{
    public class EFSQLiteProductsRepository : IProductsRepository
    {
        private readonly EFSQLiteDBContext _dbContext;

        public EFSQLiteProductsRepository(EFSQLiteDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Add(Product product)
        {
            this.CommonAddProcess(product);
            _dbContext.SaveChanges();
            return product.ID;
        }

        public async Task<int> AddAsync(Product product)
        {
            this.CommonAddProcess(product);
            await _dbContext.SaveChangesAsync();
            return product.ID;
        }

        private void CommonAddProcess(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _dbContext.Products.Add(product);
        }

        public void Delete(Product product)
        {
            this.CommonDeleteByModelProcess(product);
            _dbContext.SaveChanges();
        }

        public async Task DeleteAsync(Product product)
        {
            this.CommonDeleteByModelProcess(product);
            await _dbContext.SaveChangesAsync();
        }

        private void CommonDeleteByModelProcess(Product product)
        {
            product.DeletedAt = DateTime.Now;
            _dbContext.Products.Update(product);
        }

        public void Delete(int productId)
        {
            var productToDelete = this.CommonDeleteByIdProcess(productId);

            if (productToDelete == null)
            {
                throw new ProductNotFoundException("Product you were trying to remove was not found. Perhaps is was removed earlier.");
            }

            this.Delete(productToDelete);
        }

        public async Task DeleteAsync(int productId)
        {
            var productToDelete = this.CommonDeleteByIdProcess(productId);

            if (productToDelete == null)
            {
                throw new ProductNotFoundException("Product you were trying to remove was not found. Perhaps is was removed earlier.");
            }

            await this.DeleteAsync(productToDelete);
        }

        private Product CommonDeleteByIdProcess(int productId)
        {
            return this.GetById(productId);
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Products.Where(p => p.DeletedAt == DateTime.MinValue).AsEnumerable();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _dbContext.Products.Where(p => p.DeletedAt == DateTime.MinValue).ToListAsync();
        }

        public IEnumerable<Product> GetAllNotExpired()
        {
            var notRemoved = _dbContext.Products.Where(p => p.DeletedAt == DateTime.MinValue).AsEnumerable();
            return notRemoved.Where(p => p.Expired == false).AsEnumerable();
        }

        public async Task<IEnumerable<Product>> GetAllNotExpiredAsync()
        {
            var notRemoved = await _dbContext.Products.Where(p => p.DeletedAt == DateTime.MinValue).ToListAsync();
            return notRemoved.Where(p => p.Expired == false);
        }

        public Product GetById(int id)
        {
            return this.GetByIdCommonProcess(id).SingleOrDefault();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var foo = await this.GetByIdCommonProcess(id).ToListAsync();
            return foo.SingleOrDefault();
        }

        private IQueryable<Product> GetByIdCommonProcess(int id)
        {
            return _dbContext.Products.Where(p => p.ID == id && p.DeletedAt == DateTime.MinValue);
        }

        public void Update(Product product)
        {
            var productToUpdate = this.GetById(product.ID);

            if (productToUpdate == null)
            {
                throw new ProductNotFoundException("Product you were trying to remove was not found. Perhaps is was removed earlier.");
            }

            this.UpdateCommonProcess(productToUpdate, product);

            _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(Product product)
        {
            var productToUpdate = this.GetById(product.ID);

            if (productToUpdate == null)
            {
                throw new ProductNotFoundException("Product you were trying to remove was not found. Perhaps is was removed earlier.");
            }

            this.UpdateCommonProcess(productToUpdate, product);

            await _dbContext.SaveChangesAsync();
        }

        private void UpdateCommonProcess(Product dbModel, Product userData)
        {
            dbModel.Name = userData.Name;
            dbModel.ExpirationDate = userData.ExpirationDate;
            dbModel.Consumed = userData.Consumed;
            dbModel.DeletedAt = userData.DeletedAt;
            dbModel.UpdatedAt = DateTime.Now;
        }
    }
}
