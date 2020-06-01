﻿using DiabloII.Domain.Models.Suggestions;
using DiabloII.Domain.Queries.Domains.Suggestions;
using DiabloII.Domain.Readers.Bases;

namespace DiabloII.Domain.Readers
{
    public interface ISuggestionReader :
        IReaderGetAll<Suggestion>,
        IReaderGet<Suggestion, GetASuggestionQuery>
    {
    }
}