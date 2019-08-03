import {ItemActionName} from "../item.action";

interface ItemSearchAction
{
    type: typeof ItemActionName.ITEM_SEARCH
    payload: string // search query string parameters
}

interface ItemSearchCompletedAction {
    type: typeof ItemActionName.ITEM_SEARCH_COMPLETED
    payload: string // Item[] json
}

export type SearchActionType = ItemSearchAction | ItemSearchCompletedAction;