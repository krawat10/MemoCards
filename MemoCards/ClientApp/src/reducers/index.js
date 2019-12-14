"use strict";

import {combineReducers} from 'redux'
import memoCardsState from "./memoCardsStatus";
import user from "./user";
import language from "./language";

export default combineReducers({
    memoCardsState, user, language
});