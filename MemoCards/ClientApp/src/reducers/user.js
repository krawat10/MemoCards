import {actions} from '../actions';

const user = (state = {isValid: false}, action) => {
    switch (action.type) {
        case actions.LOGOUT:
            localStorage.removeItem('user');
            return {isValid: false};
        case actions.LOGIN:
            const {user: {id, email, token: {value, expires}}} = action;
            localStorage.setItem('user', JSON.stringify(action.user)); // save token
            return {id, email, tokenValue: value, expires: new Date(expires), isValid: true};
        default:
            return state;
    }
};

export default user;