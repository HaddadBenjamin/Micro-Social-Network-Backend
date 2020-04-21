using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Items;
using DiabloII.Application.Tests.Apis.Bases;
using DiabloII.Application.Tests.Services.Http;

namespace DiabloII.Application.Tests.Apis.Domains.Items
{
    public class ItemsApi : BaseApi, IItemsApi
    {
        protected override string BaseUrl { get; } = "items";

        public ItemsApi(IHttpService httpService) : base(httpService) { }

        public async Task<IReadOnlyCollection<ItemDto>> GetAll() =>
            await _httpService.GetAsync<IReadOnlyCollection<ItemDto>>(BaseUrl);

        public async Task<IReadOnlyCollection<ItemDto>> Search(SearchUniquesDto dto) =>
            await _httpService.GetAsync<IReadOnlyCollection<ItemDto>>($"{BaseUrl}/search");
    }
}
