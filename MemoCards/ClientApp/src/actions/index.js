const ADD_MEMO_CARD = 'ADD_MEMO_CARD';
export const addMemoCard = memoCard => ({
    type: ADD_MEMO_CARD,
    memoCard
});

const DELETE_MEMO_CARD = 'DELETE_MEMO_CARD';
export const deleteMemoCard = id => ({
    type: DELETE_MEMO_CARD,
    id
});

const INITIALIZE = 'INITIALIZE';
export const initialize = memoCards => ({
    type: INITIALIZE,
    memoCards
});

const LOGIN = 'LOGIN';
export const login = user => ({
    type: LOGIN,
    user
});

const LOGOUT = 'LOGOUT';
export const logout = () => ({
    type: LOGOUT
});

const SET_LANGUAGE = 'SET_LANGUAGE';
export const setLanguage = language => ({
    type: SET_LANGUAGE,
    language
});


export const actions = {
    ADD_MEMO_CARD, DELETE_MEMO_CARD, INITIALIZE, LOGIN, LOGOUT, SET_LANGUAGE
};