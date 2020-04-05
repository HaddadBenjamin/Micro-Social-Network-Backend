using AutoMapper;
using DiabloII.Items.Api.Application.Responses.Items;
using DiabloII.Items.Api.Domain.Models.Items;

namespace DiabloII.Items.Api.Application.Mappers
{
    public class ItemMapper : Profile
    {
        public ItemMapper()
        {
            // Data layer to DTO layer.
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