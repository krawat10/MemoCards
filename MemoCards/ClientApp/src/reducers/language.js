import {actions} from '../actions';

const en = {
    home: 'Home',
    login: 'Login',
    register: 'Register',
    pleaseLogin: 'Please login',
    typeEmailAndPassword: 'Type your email and password:',
    createNewAccount: 'Create a new account!',
    wait: 'Please wait...'
};

const pl = {
    home: 'Strona Główna',
    login: 'Zaloguj',
    register: 'Rejestracja',
    pleaseLogin: 'Proszę się zalogować',
    typeEmailAndPassword: 'Wprowadź email i hasło:',
    createNewAccount: 'Stwórz nowe konto!',
    wait: 'Proszę czekać...'
};

export const languages = {
    'en': en,
    'en-US': en,
    'en-GB': en,
    'pl': pl,
};

const language = (state = languages['en'], action) => {
    switch (action.type) {
        case actions.SET_LANGUAGE:
            if(languages.hasOwnProperty(action.language)){
                return languages[action.language];
            }
            return state;
        default:
            return state;
    }
};


export default language;