import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { Auth0Provider } from "@auth0/auth0-react";

ReactDOM.render(
  <React.StrictMode>
    <Auth0Provider
       domain={process.env.REACT_APP_AUTH0_DOMAIN}
       clientId={process.env.REACT_APP_AUTH0_CLIENTID}
       redirectUri={window.location.origin}
       audience={process.env.REACT_APP_API_AUDIENCE}
       scope="open idread:notes"
    >
    <App domain={process.env.REACT_APP_AUTH0_DOMAIN} />
    </Auth0Provider>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
