﻿using DiabloII.Application.Tests.Models.Hals.Domains.Suggestions;

namespace DiabloII.Application.Tests.Contexts.Domains.Suggestions
{
    public class HalSuggestionsTestContext : IHalSuggestionsTestContext
    {
        public HalSuggestionsDto HalResources { get; set; }
    }
}