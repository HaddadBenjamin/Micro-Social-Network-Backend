using AutoMapper;
using DiabloII.Items.Api.Application.Responses.Items;
using DiabloII.Items.Api.Domain.Models.Items;

namespace DiabloII.Items.Api.Application.MappingConfigurations
{
    public class ItemMappingConfiguration : Profile
    {
        public ItemMappingConfiguration()
        {
            CreateMap<Item, ItemDto>()
                .AfterMap((dataModel, dto) =>
                {
                    dto.Quality = dataModel.Quality.ToString();
                    dto.Category = dataModel.Category.ToString();
                });
          
            CreateMap<ItemProperty, ItemPropertyDto>();
        }
    }
}