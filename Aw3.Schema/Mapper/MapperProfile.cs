using AutoMapper;
using Aw3.Schema.Category;
using Aw3.Schema.Product;

namespace Aw3.Schema.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Aw3.Data.Model.Category, CategoryResponse>();
            CreateMap<CategoryRequest, Aw3.Data.Model.Category>();

            CreateMap<Aw3.Data.Model.Product, ProductResponse>();
            CreateMap<ProductRequest, Aw3.Data.Model.Product>();
        }


    }
}
