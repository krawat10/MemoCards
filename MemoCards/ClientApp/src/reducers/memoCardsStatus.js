import {actions} from '../actions';

export const MemoCardsStatus = {
    NOT_INITIALIZED: 'NOT_INITIALIZED',
    INITIALIZED: 'INITIALIZED',
    ERROR: 'ERROR'
};

const memoCardsState = (state = {status: MemoCardsStatus.NOT_INITIALIZED, memoCards: []}, action) => {
    switch (action.type) {
        case actions.ADD_MEMO_CARD:
            return {status: state.status, memoCards: [...state.memoCards, action.memoCard]};
        case actions.INITIALIZE:
            return {status: MemoCardsStatus.INITIALIZED, memoCards: action.memoCards};
        case actions.DELETE_MEMO_CARD:
            return {status: state.status, memoCards: state.memoCards.filter((card) => (card.id !== action.id))}
        default:
            return state;
    }
};

export default memoCardsState;