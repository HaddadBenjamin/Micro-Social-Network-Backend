import {ITEM_SEARCH, ITEM_SEARCH_COMPLETED, SearchActionTypes, ItemSearchState} from "./types";

const initialState: ItemSearchState =
{
    items: [],
    searchQueryParameter : "",
}

export function searchReducer(state = initialState, action: SearchActionTypes) : ItemSearchState
{
    switch (action.type) {
        case ITEM_SEARCH:
            return { ...state }
        case ITEM_SEARCH_COMPLETED:
            return { ...state }
        default:
            return state
    }
}