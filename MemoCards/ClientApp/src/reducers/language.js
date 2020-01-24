import {actions} from '../actions';

const defaultLang = {
    home: 'Home',
    login: 'Login',
    logout: 'Logout',
    register: 'Register',
    pleaseLogin: 'Please login',
    typeEmailAndPassword: 'Type your email and password:',
    createNewAccount: 'Create a new account!',
    wait: 'Please wait...'
};

export const languagesFilename = {
    'en': 'en.xml',
    'en-US': 'en.xml',
    'en-GB': 'en.xml',
    'pl': 'pl.xml',
};

const language = (state = defaultLang, action) => {
    switch (action.type) {
        case actions.SET_LANGUAGE:
            return action.language;
        default:
            return state; //return dictionary
    }
};


export default language;