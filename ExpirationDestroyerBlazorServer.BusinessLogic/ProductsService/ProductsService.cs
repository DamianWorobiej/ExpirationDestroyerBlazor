using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ExpirationDestroyerBlazorServer.BusinessLogic.DTOs;
using ExpirationDestroyerBlazorServer.DataAccess;
using ExpirationDestroyerBlazorServer.DataAccess.Exceptions;
using ExpirationDestroyerBlazorServer.DataAccess.Models;

namespace ExpirationDestroyerBlazorServer.BusinessLogic.ProductsService
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository repository, IMapper mapper)
        {
            this._productsRepository = repository;
            this._mapper = mapper;
        }

        public int Add(ProductDTO product)
        {
            var productModel = this._mapper.Map<Product>(product);
            return _productsRepository.Add(productModel);
        }

        public async Task<int> AddAsync(ProductDTO product)
        {
            var productModel = this._mapper.Map<Product>(product);
            int id = await _productsRepository.AddAsync(productModel);
            return id;
        }

        public void Delete(ProductDTO product)
        {
            var productModel = this._mapper.Map<Product>(product);
            _productsRepository.Delete(productModel);
        }

        public void Delete(int productId)
        {
            _productsRepository.Delete(productId);
        }

        public async Task DeleteAsync(int productId)
        {
            await _productsRepository.DeleteAsync(productId);
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            var modelsCollection = _productsRepository.GetAll();
            return _mapper.Map<IEnumerable<ProductDTO>>(modelsCollection);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var modelsCollection = await _productsRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(modelsCollection);
        }

        public ProductDTO GetById(int id)
        {
            var productModel = _productsRepository.GetById(id);

            if (productModel == null)
            {
                throw new ProductNotFoundException("Product you were trying to get was not found.");
            }

            return _mapper.Map<ProductDTO>(productModel);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var productModel = await _productsRepository.GetByIdAsync(id);

            if (productModel == null)
            {
                throw new ProductNotFoundException("Product you were trying to get was not found.");
            }

            return _mapper.Map<ProductDTO>(productModel);
        }

        public void SetAsConsumed(int id)
        {
            var model = _productsRepository.GetById(id);

            if (model == null)
            {
                throw new ProductNotFoundException("Product you were trying to set as consumed was not found");
            }

            var dto = _mapper.Map<ProductDTO>(model);
            dto.Consumed = true;
            this.Update(dto);
        }

        public async Task SetAsConsumedAsync(int id)
        {
            var model = await _productsRepository.GetByIdAsync(id);

            if (model == null)
            {
                throw new ProductNotFoundException("Product you were trying to set as consumed was not found");
            }

            var productModel = _mapper.Map<ProductDTO>(model);
            productModel.Consumed = true;
            await this.UpdateAsync(productModel);
        }

        public void Update(ProductDTO product)
        {
            var productModel = this._mapper.Map<Product>(product);
            _productsRepository.Update(productModel);
        }

        public async Task UpdateAsync(ProductDTO product)
        {
            var productModel = this._mapper.Map<Product>(product);
            await _productsRepository.UpdateAsync(productModel);
        }
    }
}
