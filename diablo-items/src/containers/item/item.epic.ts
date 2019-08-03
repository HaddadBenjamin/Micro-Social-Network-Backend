import { ItemActionName } from "./item.action";
import { ofType } from "redux-observable";
import { ajax } from 'rxjs/ajax';
import { mergeMap, map } from 'rxjs/operators';

const searchItems = searchQueryParameters => ({ type: ItemActionName.ITEM_SEARCH, payload: searchQueryParameters });
const searchItemsCompleted = items => ({ type: ItemActionName.ITEM_SEARCH_COMPLETED, payload: items });

const searchItemsEpic = action$ => action$.pipe(
    ofType(ItemActionName.ITEM_SEARCH),
    mergeMap(action =>
        ajax.getJSON(`http://localhost:56205/api/v1/Items/searchuniques/${action$.payload}`).pipe(
            map(response => searchItemsCompleted(response))
        )
    )
);
