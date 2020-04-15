﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DiabloII.Application.Requests.Items;
using DiabloII.Application.Responses.Items;

namespace DiabloII.Application.Tests.Apis
{
    public class ItemsApi
    {
        private readonly HttpContext _httpContext;

        private static readonly string BaseUrl = "items";

        public ItemsApi(HttpContext httpContext) => _httpContext = httpContext;

        public async Task GetAll() => await _httpContext.GetAsync<IReadOnlyCollection<ItemDto>>(BaseUrl);

        public async Task Search(SearchUniquesDto dto) => await _httpContext.GetAsync<IReadOnlyCollection<ItemDto>>($"{BaseUrl}/search");
    }
}