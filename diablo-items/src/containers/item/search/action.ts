import { ItemActionName } from "../item.action";
import { SearchActionTypes } from "./types"

export function searchItem(searchQueryStringParameter: string) : SearchActionTypes
{
    return {
        type: ItemActionName.ITEM_SEARCH,
        payload: searchQueryStringParameter
    }
}

export function searchItemCompleted(itemsResponse : string): SearchActionTypes
{
    return {
        type: ItemActionName.ITEM_SEARCH_COMPLETED,
        payload : itemsResponse
    }
}