import {actions} from '../actions';

const user = (state = {isValid: false}, action) => {
    switch (action.type) {
        case actions.LOGIN:
            const {user: {id, email, token: {value, expires}}} = action;
            localStorage.setItem('user', JSON.stringify(action.user));
            return { id, email, tokenValue: value, expires: new Date(expires), isValid: true };;
        default:
            return state;
    }
};

export default user;