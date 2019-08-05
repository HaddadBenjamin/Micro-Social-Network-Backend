import Item from "../components/Items/Item";

export interface ItemState
{
    items : Item[]
}

export const searchItemsReducer = (state : ItemState = { items : []}, action : any) => {
    switch (action.type)
    {
        case "SEARCH_ITEMS":
            return {
                ...state,
                items : action.payload
            };

        default:
            return state;
    }
};

