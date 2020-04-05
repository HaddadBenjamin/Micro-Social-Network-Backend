using AutoMapper;
using DiabloII.Items.Api.Application.Responses.Items;
using DiabloII.Items.Api.Domain.Models.Items;

namespace DiabloII.Items.Api.Application.Mappers.Items
{
    public class ItemDataToDtoLayer : Profile
    {
        public ItemDataToDtoLayer()
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