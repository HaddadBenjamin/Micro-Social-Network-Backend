﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Items;
using DiabloII.Application.Tests.Startup;

namespace DiabloII.Application.Tests.Domains.Items
{
    public class ItemsApi
    {
        private readonly MyHttpClient _httpClient;

        private static readonly string BaseUrl = "items";

        public ItemsApi(MyHttpClient httpClient) => _httpClient = httpClient;

        public async Task<IReadOnlyCollection<ItemDto>> GetAll() =>
            await _httpClient.GetAsync<IReadOnlyCollection<ItemDto>>(BaseUrl);

        public async Task<IReadOnlyCollection<ItemDto>> Search(SearchUniquesDto dto) =>
            await _httpClient.GetAsync<IReadOnlyCollection<ItemDto>>($"{BaseUrl}/search");
    }
}
