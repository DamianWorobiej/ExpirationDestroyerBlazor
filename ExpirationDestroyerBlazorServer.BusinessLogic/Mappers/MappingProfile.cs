using AutoMapper;
using ExpirationDestroyerBlazorServer.BusinessLogic.DTOs;
using ExpirationDestroyerBlazorServer.DataAccess.Models;

namespace ExpirationDestroyerBlazorServer.BusinessLogic.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDTO>();

            CreateMap<ProductDTO, Product>();
        }
    }
}
