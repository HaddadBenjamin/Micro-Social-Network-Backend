import {ITEM_SEARCH, ITEM_SEARCH_COMPLETED, SearchActionTypes} from "./types";
import Item from "../../components/Items/Item";

export function searchItem(searchQueryStringParameter: string) : SearchActionTypes
{
    return {
        type: ITEM_SEARCH,
        payload: searchQueryStringParameter
    }
}

export function searchItemCompleted(items : Item[]): SearchActionTypes
{
    return {
        type: ITEM_SEARCH_COMPLETED,
        payload : items
    }
}