import { combineReducers } from "redux";
import SearchItemDto from "../../components/Items/SearchItemDto";
import searchItemReducer from "item.search.reducer";

export interface ItemState
{
    itemSearch : SearchItemDto
}

export const itemReducer = combineReducers<ItemState>({
    itemSearch : searchItemReducer
});