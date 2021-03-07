using ExpirationDestroyerBlazorServer.DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpirationDestroyerBlazorServer.DataAccess
{
    public interface IProductsRepository
    {
        int Add(Product product);

        Task<int> AddAsync(Product product);

        void Update(Product product);

        Task UpdateAsync(Product product);

        void Delete(Product product);

        Task DeleteAsync(Product product);

        void Delete(int productId);

        Task DeleteAsync(int productId);


        IEnumerable<Product> GetAll();

        Task<IEnumerable<Product>> GetAllAsync();

        IEnumerable<Product> GetAllNotExpired();

        Task<IEnumerable<Product>> GetAllNotExpiredAsync();

        Product GetById(int id);

        Task<Product> GetByIdAsync(int id);
    }
}
