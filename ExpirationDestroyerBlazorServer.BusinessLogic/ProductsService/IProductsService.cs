using ExpirationDestroyerBlazorServer.BusinessLogic.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpirationDestroyerBlazorServer.BusinessLogic.ProductsService
{
    public interface IProductsService
    {
        int Add(ProductDTO product);

        Task<int> AddAsync(ProductDTO product);

        void Update(ProductDTO product);

        Task UpdateAsync(ProductDTO product);

        void Delete(ProductDTO product);

        void Delete(int productId);

        Task DeleteAsync(int productId);

        IEnumerable<ProductDTO> GetAll();

        Task<IEnumerable<ProductDTO>> GetAllAsync();

        ProductDTO GetById(int id);

        Task<ProductDTO> GetByIdAsync(int id);

        void SetAsConsumed(int id);

        Task SetAsConsumedAsync(int id);
    }
}
