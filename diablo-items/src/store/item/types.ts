import Item from "../../components/Items/Item";

export const ITEM_SEARCH = "ITEM_SEARCH";
export const ITEM_SEARCH_COMPLETED = "ITEM_SEARCH_COMPLETED";

export interface ItemSearchState
{
    items : Item[],
    searchQueryParameter : string,
}

interface ItemSearchAction
{
    type: typeof ITEM_SEARCH
    payload: string // search query string parameters
}

interface ItemSearchCompletedAction {
    type: typeof ITEM_SEARCH_COMPLETED
    payload: Item[]
}

export type SearchActionTypes = ItemSearchAction | ItemSearchCompletedAction;