import React from 'react';
import {Route} from 'react-router';
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import './custom.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import {BrowserRouter, Router} from "react-router-dom";
import {createBrowserHistory} from 'history';
import SignIn from "./components/Auth/SignIn";
import SignUp from "./components/Auth/SignUp";
import {connect} from "react-redux";
import {login, setLanguage} from "./actions";


const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
export const history = createBrowserHistory();

const langHeader = navigator.language;
const params = new URLSearchParams(window.location.search);
const langParam = params.get('lang');

const App = ({user, setLanguage, login}) => {
    if(langParam) setLanguage(langParam);
    else setLanguage(langHeader);

    if(!user.isValid && localStorage.getItem('user')){
        const user = JSON.parse(localStorage.getItem('user'));
        login(user);
    }

    return (
            <BrowserRouter basename={baseUrl}>
                <Router history={history}>
                    <Layout>
                        <Route exact path='/' component={Home}/>
                        <Route path='/login' render={() => <SignIn/>}/>
                        <Route path='/register' component={() => <SignUp/>}/>
                    </Layout>
                </Router>
            </BrowserRouter>
    );
};

const mapStateToProps = state => ({
    user: state.user,
    memoCardsState: state.memoCardsState,
    language: state.language
});

const mapDispatchToProps = dispatch => ({
    setLanguage: (language) => dispatch(setLanguage(language)),
    login: (user) => dispatch(login(user))
});


export default connect(mapStateToProps, mapDispatchToProps)(App);
