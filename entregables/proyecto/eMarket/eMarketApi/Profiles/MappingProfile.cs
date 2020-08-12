using AutoMapper;
using eMarketDB.Infrastructure.Data;
using eMarketDomain.Models;

namespace eMarketApi.Profiles
{
    public class MappingProfile : Profile 
    {
        /// <summary>
        /// Automapper profile
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Categories, Category>();
            CreateMap<Category, Categories>();
            CreateMap<Products, Product>();
            CreateMap<Product, Products>();
            CreateMap<Review, Reviews>();
            CreateMap<Reviews, Review>();
        }
    }
}
