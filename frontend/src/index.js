import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import axios from 'axios';

axios.defaults.baseURL = window.REACT_APP_BaseURL;
//process.env.REACT_APP_BaseURL;

axios.defaults.withCredentials = true;
axios.defaults.headers.post['Content-Type'] = 'application/json';
axios.interceptors.request.use(
  request=>{
    console.log("Request happening");
    console.log(axios.defaults.baseURL);
    return request;
  },
  error =>{
    console.log("Request Error");
    console.log(error);
    return Promise.reject(error)
  }
)

axios.interceptors.response.use(
  response=>{
    if(response.config.parse){
      console.log("Parsing Normal Response");
    }
    return response
  },
  error =>{
      console.log("app.js error thrown")
      console.log(error.response)
      if(error.response && error.response.status && error.response.status == 401){
        console.log(`error 401 unauthorized`)
        window.location.replace('/main/Signin');
      }
      return Promise.reject(error.response);
  }
)

ReactDOM.render(
  <React.StrictMode>
    <BrowserRouter>
    <App />
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
