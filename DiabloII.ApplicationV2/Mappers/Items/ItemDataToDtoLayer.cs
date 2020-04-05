using System.Linq;
using AutoMapper;
using DiabloII.Application.Responses.Items;
using DiabloII.Domain.Models.Items;

namespace DiabloII.Application.Mappers.Items
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