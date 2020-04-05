using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Items;
using DiabloII.Items.Api.Application.Responses.Items;
using DiabloII.Items.Api.Domain.Models.Items;
using DiabloII.Items.Api.Domain.Queries.Items;

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

            // DTO layer to query layer.
            CreateMap<SearchUniquesDto, SearchUniquesQuery>()
                .AfterMap((dto, query) =>
                {
                    query.SubCategories = dto.SubCategories?.Split(", ")
                        .Select(_ => _
                            .Replace("Two_Handed_Sword", "Two-Handed Sword")
                            .Replace("Wirt_s_Leg", "Wirt's Leg")
                            .Replace("Poorman_s_Head", "Poorman`s Head")
                            .Replace("Hunter_s_Bow", "Hunter’s Bow")
                            .Replace("Chu_Ko_Nu", "Chu-Ko-Nu")
                            .Replace("Bec_De_Corbin", "Bec-De-Corbin")
                            .Replace("Silver_Edged_Axe", "Silver-Edged Axe")
                            .Replace("_", " "));
                });
        }
    }
}