import React, {useState, useEffect} from 'react';
import {Router, Route} from 'react-router-dom';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import './custom.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import SignIn from "./components/Auth/SignIn";
import SignUp from "./components/Auth/SignUp";
import {connect} from "react-redux";
import {login, setLanguage} from "./actions";
import history from './history';
import {get} from './services/apiRequest';
import {languagesFilename} from "./reducers/language";
import {getCookie, setCookie} from "./services/cookies";
import {isEmpty} from 'lodash'

let isLanguageInitialized = false;
const langHeader = navigator.language;
const langParam = new URLSearchParams(window.location.search).get('lang');
const langCookie = getCookie('lang');
let language = 'en';

const App = ({user, setDictionary, login}) => {

    if (!isLanguageInitialized) {
        if (!isEmpty(langHeader)) language = langHeader;
        if (!isEmpty(langCookie)) language = langCookie;
        if (!isEmpty(langParam)) language = langParam;
        console.log(language);
        setCookie('lang', language, 7); // Set language cookie
        get('lang').then(value => setDictionary(value.root)); // Initialize translation
        isLanguageInitialized = true;
    }

    if (!user.isValid && localStorage.getItem('user')) {
        const user = JSON.parse(localStorage.getItem('user'));
        login(user);
    }

    return (
        <Router history={history}>
            <Layout>
                <Route exact path='/' component={Home}/>
                <Route path='/login' render={() => <SignIn/>}/>
                <Route path='/register' component={() => <SignUp/>}/>
            </Layout>
        </Router>
    );
};

const mapStateToProps = state => ({
    user: state.user,
    memoCardsState: state.memoCardsState,
    language: state.language
});

const mapDispatchToProps = dispatch => ({
    setDictionary: (language) => dispatch(setLanguage(language)),
    login: (user) => dispatch(login(user))
});


export default connect(mapStateToProps, mapDispatchToProps)(App);
