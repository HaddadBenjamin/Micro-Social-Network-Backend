using System.Linq;
using AutoMapper;
using DiabloII.Items.Api.Application.Requests.Items;
using DiabloII.Items.Api.Domain.Queries.Items;

namespace DiabloII.Items.Api.Application.Mappers.Items
{
    public class ItemDtoToQueryLayer : Profile
    {
        public ItemDtoToQueryLayer()
        {
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