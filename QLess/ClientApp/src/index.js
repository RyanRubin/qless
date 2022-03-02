import React from 'react';
import ReactDom from 'react-dom';
import App from './App'

import 'bootstrap/dist/css/bootstrap.css';
import './index.css';

ReactDom.render(<App />, document.getElementById('root'));

window.appFetch = async (url, httpVerb) => {
    const response = await fetch(url, { method: httpVerb });
    const { returnValue, errorMessage } = await response.json();
    if (errorMessage) {
        alert(errorMessage);
        throw errorMessage;
    }
    return returnValue;
};
