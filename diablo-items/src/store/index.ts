import { itemReducer } from './item/reducers'
import {combineReducers} from 'redux'

const rootReducer = combineReducers({
    search: itemReducer
})

export type GlobalState = ReturnType<typeof rootReducer>