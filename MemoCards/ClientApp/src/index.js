"use strict";
import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import store from "./store";
import {Provider} from "react-redux";
import {Router} from "react-router";
import {createBrowserHistory} from "history";
export const history = createBrowserHistory();

const rootElement = document.getElementById('root');

ReactDOM.render(<Provider store={store}><App/></Provider>, rootElement);
